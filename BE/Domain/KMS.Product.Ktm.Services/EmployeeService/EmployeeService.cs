using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Dto.KmsEmployee;
using KMS.Product.Ktm.Services.AuthenticateService;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private IConfiguration Configuration { get; }
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<EmployeeService> _logger;

        /// <summary>
        /// Inject Employee repository, AutoMapper, Authenticate service, Team service
        /// </summary>
        /// <returns></returns>
        public EmployeeService(
            IConfiguration configuration,
            IEmployeeRepository employeeRepository, 
            IMapper mapper, 
            IAuthenticateService authenticateService, 
            ITeamRepository teamRepository,
            ILogger<EmployeeService> logger)
        {
            Configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException($"{nameof(employeeRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _authenticateService = authenticateService ?? throw new ArgumentNullException($"{nameof(authenticateService)}");
            _teamRepository = teamRepository ?? throw new ArgumentNullException($"{nameof(teamRepository)}");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)}");
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <returns>Employee by id</returns>
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <returns></returns>
        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.InsertAsync(employee);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <returns></returns>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        /// <summary>
        /// Delete an existing employee
        /// </summary>
        /// <returns></returns>
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await _employeeRepository.DeleteAsync(employee);
        }

        /// <summary>
        /// There are 3 cases when syncing:
        /// 1. New employees
        ///     Add new employee to database
        /// 2. Active employees
        ///     If join new team
        ///         Update released date of the current team to now
        ///         Add new join team
        /// 3. Quit employees
        ///     Update release date of the current team to now
        /// </summary>
        /// <returns></returns>
        public async Task SyncEmployeeDatabaseWithKms(DateTime now)
        {
            _logger.LogInformation("Start sync employee");
            // Fetch employees from KMS and map from DTO to Employee
            var fetchedEmployeeDTOs = await GetEmployeesFromKmsAsync();
            var fetchedEmployees = 
                _mapper.Map<IEnumerable<KmsEmployeeDto>, 
                IEnumerable<Employee>>(fetchedEmployeeDTOs.Where(e => int.TryParse(e.EmployeeBadgeId, out _) && e.Email.Contains("@")));

            // Get employees from database
            var databaseEmployees = await GetAllEmployeesAsync();

            // Get employee badge ids
            var fetchedEmployeeBadgeIds = fetchedEmployees.Select(e => e.EmployeeBadgeId).ToList();
            var databaseEmployeeBadgeIds = databaseEmployees.Select(e => e.EmployeeBadgeId).ToList();


            // Sync new employees
            var teams = await _teamRepository.GetTeamsAsync();
            var newEmployees = fetchedEmployees.Where(e => !databaseEmployeeBadgeIds.Contains(e.EmployeeBadgeId)).ToList();
            if(newEmployees.Count() > 0)
            {
                await SyncNewEmployees(newEmployees, teams);
            }

            // Sync (update) active employees change team
            var updateEmployees = new List<Employee>();
            foreach(var databaseEmployee in databaseEmployees)
            {
                var fetch = fetchedEmployees
                    .Where(e => e.EmployeeBadgeId == databaseEmployee.EmployeeBadgeId && e.CurrentTeam != databaseEmployee.CurrentTeam)
                    .FirstOrDefault();

                if(fetch != null)
                {
                    databaseEmployee.CurrentTeam = fetch.CurrentTeam;
                    updateEmployees.Add(databaseEmployee);
                }
            }

            if (updateEmployees.Count() > 0)
            {
                await SyncActiveEmployees(updateEmployees, teams);
            }

            // Sync quit employees
            var quitEmployees = databaseEmployees.Where(e => !fetchedEmployeeBadgeIds.Contains(e.EmployeeBadgeId)).ToList();
            if (quitEmployees.Count() > 0)
            {
                await SyncQuitEmployees(quitEmployees);
            }
        }

        /// <summary>
        /// Get all teams from KMS by KMS API
        /// API: https://hr.kms-technology.com/api/contact/ReturnContactList/{pageIndex}/{pageSize}
        ///     Default page index: 1
        ///     Default page size: 10
        /// </summary>
        /// <returns>A collection of all KMS employee DTOs</returns>
        private async Task<IEnumerable<KmsEmployeeDto>> GetEmployeesFromKmsAsync()
        {
            var KmsEmployeeDTOs = new List<KmsEmployeeDto>();
            // Initialize query string for request to KMS HRM API
            var pageIndex = 1;
            var pageSize = 100;
            var totalCount = 0;
            // Initialize httpclient with token to send request to KMS HRM 
            var  bearerToken = await _authenticateService.AuthenticateUsingConfiguration();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            do
            {
                var response = await client
                    .GetAsync($"{Configuration.GetValue<string>("KmsInfo:KmsEmployeeRequestUrl")}/{pageIndex}/{pageSize}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Convert response JSON to object and get total count
                    var contentString = await response.Content.ReadAsStringAsync();
                    var kmsEmployeeResponse = JsonConvert.DeserializeObject<KmsEmployeeResponseDto>(contentString);
                    totalCount = kmsEmployeeResponse.TotalCount;
                    // Add KMS employee DTOs to list
                    KmsEmployeeDTOs.AddRange(kmsEmployeeResponse.KmsEmployees);
                }
                // Prepare next request
                pageIndex += 1;

            } while (totalCount >= (pageSize * (pageIndex - 1)));

            return KmsEmployeeDTOs;
        }

        /// <summary>
        /// Add new employees from KMS 
        /// </summary>
        /// <param name="newEmployees"></param>
        /// <param name="teams"></param>
        /// <returns></returns>
        private async Task SyncNewEmployees(IEnumerable<Employee> newEmployees, IEnumerable<Team> teams)
        {
            foreach (var newEmployee in newEmployees)
            {
                //default role
                newEmployee.EmployeeRoleId = 1;

                if (teams.Where(t => t.TeamName == newEmployee.CurrentTeam).Any())
                {
                    newEmployee.EmployeeTeams = new List<EmployeeTeam>
                    {
                        new EmployeeTeam()
                        {
                            TeamID = teams.Where(t => t.TeamName == newEmployee.CurrentTeam).Select(t => t.Id).First(),
                            JoinedDate = DateTime.Now,
                            ReleseadDate = null
                        }
                    };
                }
            }

            await _employeeRepository.CreateEmployees(newEmployees);
        }

        /// <summary>
        /// Update active employees in database 
        /// </summary>
        /// <param name="activeEmployees"></param>
        /// <param name="teams"></param>
        /// <returns></returns>
        private async Task SyncActiveEmployees(IEnumerable<Employee> activeEmployees, IEnumerable<Team> teams)
        {
            foreach (var activeEmployee in activeEmployees)
            {
                // Update released date of current team to now
                if (activeEmployee.EmployeeTeams.Where(e => e.ReleseadDate == null).Any())
                {
                    activeEmployee.EmployeeTeams.Where(e => e.ReleseadDate == null).First()
                       .ReleseadDate = DateTime.Now;
                }

                // Add new join team
                if (teams.Where(t => t.TeamName == activeEmployee.CurrentTeam).Any())
                {
                    activeEmployee.EmployeeTeams.Add(new EmployeeTeam()
                    {
                        TeamID = teams.Where(t => t.TeamName == activeEmployee.CurrentTeam).Select(t => t.Id).First(),
                        JoinedDate = DateTime.Now,
                        ReleseadDate = null
                    });
                }
            }

            await _employeeRepository.UpdateEmployees(activeEmployees);
        }

        /// <summary>
        /// Update quit employee in database by setting released date to now
        /// </summary>
        /// <param name="quitEmployees"></param>
        /// <returns></returns>
        private async Task SyncQuitEmployees(IEnumerable<Employee> quitEmployees)
        {
            foreach (var oldEmployee in quitEmployees)
            {
                if (oldEmployee.EmployeeTeams.Where(e => e.ReleseadDate == null).Any())
                {
                    oldEmployee.EmployeeTeams.Where(e => e.ReleseadDate == null).First()
                       .ReleseadDate = DateTime.Now;
                }
            }

            await _employeeRepository.UpdateEmployees(quitEmployees);
        }
    }
}

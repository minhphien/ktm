using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.LoginService;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Services.TeamService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        private readonly ITeamService _teamService;

        /// <summary>
        /// Inject Employee repository
        /// </summary>
        /// <returns></returns>
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILoginService loginService, ITeamService teamService)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException($"{nameof(employeeRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _loginService = loginService ?? throw new ArgumentNullException($"{nameof(loginService)}");
            _teamService = teamService ?? throw new ArgumentNullException($"{nameof(teamService)}");
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
        /// Get an employee by id
        /// </summary>
        /// <returns>An employee by id</returns>
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetByIdAsync(employeeId);
        }

        /// <summary>
        /// Create an employee
        /// </summary>
        /// <returns></returns>
        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.InsertAsync(employee);
        }

        /// <summary>
        /// Update an employee
        /// </summary>
        /// <returns></returns>
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        /// <summary>
        /// Delete an employee
        /// </summary>
        /// <returns></returns>
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await _employeeRepository.DeleteAsync(employee);
        }

        /// <summary>
        /// Get all employees from KMS HRM system
        /// https://hr.kms-technology.com/api/contact/ReturnContactList/{pageIndex}/{pageSize}
        /// Index: 1
        /// Page size: 10
        /// </summary>
        /// <returns>A collection of all employess from KMS HRM system</returns>
        public async Task<IEnumerable<EmployeeFromKmsDto>> GetEmployeesFromKmsAsync()
        {
            var employeesFromKms = new List<EmployeeFromKmsDto>();
            // Initialize query string for request to KMS HRM API
            var pageIndex = 1;
            var pageSize = 100;
            var totalCount = 0;
            // Initialize httpclient with token to send request to KMS HRM 
            //var bearerToken = await _loginService.LoginWithConfiguration();
            var bearerToken = _loginService.LoginWithTokens();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            do
            {
                var requestUrl = $"https://hr.kms-technology.com/api/contact/ReturnContactList/{pageIndex}/{pageSize}";
                var response = await client.GetAsync(requestUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Convert response JSON to object and get total count
                    var contentString = await response.Content.ReadAsStringAsync();
                    var kmsEmployeeResponse = JsonConvert.DeserializeObject<EmployeeFromKmsResponse>(contentString);
                    totalCount = kmsEmployeeResponse.TotalCount;
                    // Add KMS employees to list
                    var kmsEmployeeDtos = kmsEmployeeResponse.KmsEmployeeDtos;
                    employeesFromKms.AddRange(kmsEmployeeDtos);
                    // Prepare next request
                    pageIndex += 1;
                }
            } while (totalCount >= (pageSize * (pageIndex - 1)));
            return employeesFromKms;
        }

        /// <summary>
        /// Add new employees from KMS 
        /// </summary>
        /// <param name="newEmployees"></param>
        /// <returns></returns>
        public async Task SyncNewEmployees(IEnumerable<Employee> newEmployees)
        {
            foreach (var newEmployee in newEmployees)
            {
                var currentTeam = newEmployee.CurrentTeam;
                var currentTeamId = await _teamService.GetTeamIdByTeamNameAsync(currentTeam);
                newEmployee.EmployeeTeams.Add(new EmployeeTeam()
                {
                    TeamID = currentTeamId,
                    JoinedDate = DateTime.Now,
                    ReleseadDate = null
                });
                await CreateEmployeeAsync(newEmployee);
            }
        }

        /// <summary>
        /// Update employees in database from KMS
        /// </summary>
        /// <param name="currentEmployees"></param>
        /// <returns></returns>
        public async Task SyncCurrentEmployees(IEnumerable<Employee> currentEmployees)
        {
            foreach (var currentEmployee in currentEmployees)
            {            
                var currentTeam = currentEmployee.CurrentTeam;
                var currentTeamId = await _teamService.GetTeamIdByTeamNameAsync(currentTeam);
                // Employee joins new team
                if (! currentEmployee.EmployeeTeams.Select(e => e.TeamID).Contains(currentTeamId))
                {
                    // Update released date of current team
                    currentEmployee.EmployeeTeams.OrderByDescending(e => e.JoinedDate)
                       .FirstOrDefault()
                       .ReleseadDate = DateTime.Now;
                    // Add new team
                    currentEmployee.EmployeeTeams.Add(new EmployeeTeam()
                    {
                        TeamID = currentTeamId,
                        JoinedDate = DateTime.Now,
                        ReleseadDate = null
                    });
                }
                await UpdateEmployeeAsync(currentEmployee);
            }
        }

        /// <summary>
        /// Update quit employee in database by setting released date to current
        /// </summary>
        /// <param name="quitEmployees"></param>
        /// <returns></returns>
        public async Task SyncQuitEmployees(IEnumerable<Employee> quitEmployees)
        {
            foreach (var oldEmployee in quitEmployees)
            {
                oldEmployee.EmployeeTeams.OrderByDescending(e => e.JoinedDate)
                      .FirstOrDefault()
                      .ReleseadDate = DateTime.Now;
                await UpdateEmployeeAsync(oldEmployee);
            }
        }

        /// <summary>
        /// There are 3 cases when syncing:
        /// 1. New employees
        ///     Add new employee to database
        /// 2. Current employees
        ///     If join new team
        ///         Update released date of the current team 
        ///         Add new team
        /// 3. Quit employees
        ///     Update release date
        /// </summary>
        /// <returns></returns>
        public async Task SyncEmployeeDatabaseWithKms()
        {
            // Fetch employees from KMS and map from DTO to Employee
            var fetchedEmployeesDto = await GetEmployeesFromKmsAsync();
            var fetchedEmployees = _mapper.Map<IEnumerable<EmployeeFromKmsDto>, IEnumerable<Employee>>(fetchedEmployeesDto);
            // Get employees from database
            var databaseEmployees = await GetAllEmployeesAsync();
            // Get employee badge ids
            var fetchedEmployeeBadgeIds = fetchedEmployees.Select(e => e.EmployeeBadgeId);
            var databaseEmployeeBadgeIds = databaseEmployees.Select(e => e.EmployeeBadgeId);
            // Sync new employees
            var newEmployees = fetchedEmployees.Where(e => !databaseEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncNewEmployees(newEmployees);
            // Sync current employees 
            var currentEmployees = fetchedEmployees.Where(e => databaseEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncCurrentEmployees(currentEmployees);
            // Sync quit employees
            var quitEmployees = databaseEmployees.Where(e => !fetchedEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncQuitEmployees(quitEmployees);
        }
    }
}

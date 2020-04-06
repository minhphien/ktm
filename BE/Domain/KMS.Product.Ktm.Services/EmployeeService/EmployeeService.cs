using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.EntitiesServices.DTOs;
using KMS.Product.Ktm.EntitiesServices.Responses;
using KMS.Product.Ktm.Services.AuthenticateService;
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
        private readonly IAuthenticateService _authenticateService;
        private readonly ITeamService _teamService;

        public const string KmsEmployeeRequestUrl = "https://hr.kms-technology.com/api/contact/ReturnContactList";

        /// <summary>
        /// Inject Employee repository, AutoMapper, Authenticate service, Team service
        /// </summary>
        /// <returns></returns>
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IAuthenticateService authenticateService, ITeamService teamService)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException($"{nameof(employeeRepository)}");
            _mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
            _authenticateService = authenticateService ?? throw new ArgumentNullException($"{nameof(authenticateService)}");
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
        public async Task SyncEmployeeDatabaseWithKms()
        {
            // Fetch employees from KMS and map from DTO to Employee
            var fetchedEmployeeDTOs = await GetEmployeesFromKmsAsync();
            var fetchedEmployees = _mapper.Map<IEnumerable<KmsEmployeeDTO>, IEnumerable<Employee>>(fetchedEmployeeDTOs);
            // Get employees from database
            var databaseEmployees = await GetAllEmployeesAsync();
            // Get employee badge ids
            var fetchedEmployeeBadgeIds = fetchedEmployees.Select(e => e.EmployeeBadgeId);
            var databaseEmployeeBadgeIds = databaseEmployees.Select(e => e.EmployeeBadgeId);
            // Sync new employees
            var newEmployees = fetchedEmployees.Where(e => !databaseEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncNewEmployees(newEmployees);
            // Sync active employees 
            var activeEmployees = fetchedEmployees.Where(e => databaseEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncActiveEmployees(activeEmployees);
            // Sync quit employees
            var quitEmployees = databaseEmployees.Where(e => !fetchedEmployeeBadgeIds.Contains(e.EmployeeBadgeId));
            await SyncQuitEmployees(quitEmployees);
        }

        /// <summary>
        /// Get all teams from KMS by KMS API
        /// API: https://hr.kms-technology.com/api/contact/ReturnContactList/{pageIndex}/{pageSize}
        ///     Default page index: 1
        ///     Default page size: 10
        /// </summary>
        /// <returns>A collection of all KMS employee DTOs</returns>
        private async Task<IEnumerable<KmsEmployeeDTO>> GetEmployeesFromKmsAsync()
        {
            var KmsEmployeeDTOs = new List<KmsEmployeeDTO>();
            // Initialize query string for request to KMS HRM API
            var pageIndex = 1;
            var pageSize = 100;
            var totalCount = 0;
            // Initialize httpclient with token to send request to KMS HRM 
            var bearerToken = _authenticateService.AuthenticateUsingToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            do
            {
                var response = await client.GetAsync($"{KmsEmployeeRequestUrl}/{pageIndex}/{pageSize}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Convert response JSON to object and get total count
                    var contentString = await response.Content.ReadAsStringAsync();
                    var kmsEmployeeResponse = JsonConvert.DeserializeObject<KmsEmployeeResponse>(contentString);
                    totalCount = kmsEmployeeResponse.TotalCount;
                    // Add KMS employee DTOs to list
                    KmsEmployeeDTOs.AddRange(kmsEmployeeResponse.KmsEmployeeDTOs);
                    // Prepare next request
                    pageIndex += 1;
                }

            } while (totalCount >= (pageSize * (pageIndex - 1)));

            return KmsEmployeeDTOs;
        }

        /// <summary>
        /// Add new employees from KMS 
        /// </summary>
        /// <param name="newEmployees"></param>
        /// <returns></returns>
        private async Task SyncNewEmployees(IEnumerable<Employee> newEmployees)
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
        /// Update active employees in database 
        /// If the employee joins new team
        ///     Update released date of curernt team to now
        ///     Add new join team
        /// </summary>
        /// <param name="activeEmployees"></param>
        /// <returns></returns>
        private async Task SyncActiveEmployees(IEnumerable<Employee> activeEmployees)
        {
            foreach (var activeEmployee in activeEmployees)
            {
                var currentTeam = activeEmployee.CurrentTeam;
                var currentTeamId = await _teamService.GetTeamIdByTeamNameAsync(currentTeam);

                // Employee joins new team
                if (!activeEmployee.EmployeeTeams.Select(e => e.TeamID).Contains(currentTeamId))
                {
                    // Update released date of current team to now
                    activeEmployee.EmployeeTeams.OrderByDescending(e => e.JoinedDate)
                       .FirstOrDefault()
                       .ReleseadDate = DateTime.Now;
                    // Add new join team
                    activeEmployee.EmployeeTeams.Add(new EmployeeTeam()
                    {
                        TeamID = currentTeamId,
                        JoinedDate = DateTime.Now,
                        ReleseadDate = null
                    });
                }

                await UpdateEmployeeAsync(activeEmployee);
            }
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
                oldEmployee.EmployeeTeams.OrderByDescending(e => e.JoinedDate)
                      .FirstOrDefault()
                      .ReleseadDate = DateTime.Now;
                await UpdateEmployeeAsync(oldEmployee);
            }
        }
    }
}

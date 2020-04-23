using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly KtmDbContext context;
        private readonly DbSet<Employee> employee;

        public EmployeeRepository(KtmDbContext context, ILogger<Employee> logger) : base(context, logger)
        {
            this.context = context;
            employee = context.Set<Employee>();
        }

        /// <summary>Gets the employee team by slack user ids.</summary>
        /// <param name="slackUserIds">The slack user IDs.</param>
        /// <returns></returns>
        public IQueryable<Employee> GetEmployeeTeamBySlackUserIds(IEnumerable<string> slackUserIds)
        {
            return employee.Where(e => slackUserIds.Contains(e.SlackUserId));
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await Task.FromResult(GetAll().Include(e => e.EmployeeTeams).ToList());
        }

        /// <summary>
        /// get employee by email
        /// </summary>
        /// <param name="emailaddresses"></param>
        /// <returns>employee</returns>
        public async Task<IEnumerable<Employee>> GetEmployeeByEmails(List<string> emailaddresses)
        {
            return await employee.Where(e => emailaddresses.Contains(e.Email)).ToListAsync();
        }

        /// <summary>
        /// create employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public async Task CreateEmployees(IEnumerable<Employee> employees)
        {
            employee.AddRange(employees);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        public async Task UpdateEmployees(IEnumerable<Employee> employees)
        {
            employee.UpdateRange(employees);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// get employee by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Employee GetEmployeeByUserName(string username)
        {
            return employee.Where(e => e.UserName == username).FirstOrDefault();
        }
    }
}

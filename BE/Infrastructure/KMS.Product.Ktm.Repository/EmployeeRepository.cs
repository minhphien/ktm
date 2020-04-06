using KMS.Product.Ktm.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly DbSet<Employee> employee;

        public EmployeeRepository(KtmDbContext context) : base(context)
        {
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
            return await Task.FromResult(GetAll().ToList());
        }
    }
}

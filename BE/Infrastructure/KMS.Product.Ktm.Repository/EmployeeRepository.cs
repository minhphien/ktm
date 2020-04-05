using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KMS.Product.Ktm.Entities.Models;

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
    }
}
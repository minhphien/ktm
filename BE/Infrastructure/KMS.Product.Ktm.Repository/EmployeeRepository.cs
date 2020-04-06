using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly DbSet<Employee> employee;

        public EmployeeRepository(KtmDbContext context) : base(context)
        {
            employee = context.Set<Employee>();
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await Task.FromResult(GetAll().ToList());
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
    }
}

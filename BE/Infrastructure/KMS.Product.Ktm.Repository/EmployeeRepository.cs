using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(KtmDbContext context) : base(context)
        {
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

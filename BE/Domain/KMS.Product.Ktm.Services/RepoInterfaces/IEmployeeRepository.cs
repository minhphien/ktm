using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{    
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}

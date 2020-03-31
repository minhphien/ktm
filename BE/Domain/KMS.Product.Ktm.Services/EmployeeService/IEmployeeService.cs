using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.EmployeeService
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Get an employee by id
        /// </summary>
        /// <returns>An employee by id</returns>
        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <returns></returns>
        Task CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <returns></returns>
        Task UpdateEmployeeAsync(Employee employee);

        /// <summary>
        /// Delete an existing employee
        /// </summary>
        /// <returns></returns>
        Task DeleteEmployeeAsync(Employee employee);

        /// <summary>
        /// Update existing employees' information in the database from list of all employees after GET request to KMS HRM API
        /// Only update if the information changes
        /// </summary>
        /// <returns></returns>
        Task SyncEmployeeDatabaseWithKms();
    }
}

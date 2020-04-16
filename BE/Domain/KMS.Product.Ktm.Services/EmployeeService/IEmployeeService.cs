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
        /// Get employee by id
        /// </summary>
        /// <returns>Employee by id</returns>
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
        Task SyncEmployeeDatabaseWithKms(DateTime now);
    }
}

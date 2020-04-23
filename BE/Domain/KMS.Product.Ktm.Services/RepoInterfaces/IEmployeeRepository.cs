using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>Gets the employee team by slack user ids.</summary>
        /// <param name="slackUserIds">The slack user IDs.</param>
        /// <returns></returns>
        IQueryable<Employee> GetEmployeeTeamBySlackUserIds(IEnumerable<string> slackUserIds);

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>A collection of all employees</returns>
        Task<IEnumerable<Employee>> GetEmployeesAsync();

        /// <summary>
        /// get employee by email
        /// </summary>
        /// <param name="emailaddresses"></param>
        /// <returns>employee</returns>
        Task<IEnumerable<Employee>> GetEmployeeByEmails(List<string> emailaddresses);

        /// <summary>
        /// create employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        Task CreateEmployees(IEnumerable<Employee> employees);

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        Task UpdateEmployees(IEnumerable<Employee> employees);

        /// <summary>
        /// get employee by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Employee GetEmployeeByUserName(string username);
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Repository
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
    }
}

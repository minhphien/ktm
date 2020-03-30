using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{
    public interface IEmployeeTeamRepository : IBaseRepository<EmployeeTeam>
    {

        /// <summary>
        /// get employee by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>employee</returns>
        Task<IEnumerable<EmployeeTeam>> GetEmployeeTeamByEmails(List<string> emailaddresses);
    }
}
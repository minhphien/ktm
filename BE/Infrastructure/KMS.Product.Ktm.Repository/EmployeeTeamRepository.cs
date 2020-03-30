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
    public class EmployeeTeamRepository : BaseRepository<EmployeeTeam>, IEmployeeTeamRepository
    {
        private readonly DbSet<EmployeeTeam> employeeTeam;

        public EmployeeTeamRepository(KtmDbContext context) : base(context)
        {
            employeeTeam = context.Set<EmployeeTeam>();
        }

        /// <summary>
        /// get employee by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>employee</returns>
        public async Task<IEnumerable<EmployeeTeam>> GetEmployeeTeamByEmails(List<string> emailaddresses)
        {
            return await employeeTeam.Where(et => emailaddresses.Contains(et.Employee.Email) && et.ReleseadDate == null).ToListAsync();
        }
    }
}
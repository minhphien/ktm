using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Repository
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(KtmDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>A collection of all teams</returns>
        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }

        /// <summary>
        /// Get all teams by condition
        /// </summary>
        /// <returns>A collection of all teams</returns>
        public async Task<IEnumerable<Team>> GetTeamsByConditionAsync(Expression<Func<Team, bool>> expression)
        {
            return await Task.FromResult(GetByCondition(expression).ToList());
        }
    }
}

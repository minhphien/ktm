using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{    
    public interface ITeamRepository : IBaseRepository<Team>
    {
        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>A collection of all teams</returns>
        Task<IEnumerable<Team>> GetTeamsAsync();

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>A collection of all teams</returns>
        Task<IEnumerable<Team>> GetTeamsByConditionAsync(Expression<Func<Team, bool>> expression);

        /// <summary>
        /// Create teams
        /// </summary>
        /// <returns></returns>
        Task CreateTeams(IEnumerable<Team> teams);

        /// <summary>
        /// Update teams
        /// </summary>
        /// <returns></returns>
        Task UpdateTeams(IEnumerable<Team> teams);
    }
}

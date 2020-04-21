using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KMS.Product.Ktm.Services.TeamService
{
    public interface ITeamService
    {
        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns>A collection of all teams</returns>
        Task<IEnumerable<Team>> GetAllTeamsAsync();

        /// <summary>
        /// Get team by id
        /// </summary>
        /// <returns>Team by id</returns>
        Task<Team> GetTeamByIdAsync(int teamId);

        /// <summary>
        /// Create a new team
        /// </summary>
        /// <returns></returns>
        Task CreateTeamAsync(Team team);

        /// <summary>
        /// Update an existing team
        /// </summary>
        /// <returns></returns>
        Task UpdateTeamAsync(Team team);

        /// <summary>
        /// Delete an existing team
        /// </summary>
        /// <returns></returns>
        Task DeleteTeamAsync(Team team);

        /// <summary>
        /// Get team id from team name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns>
        /// If team exists, return team id
        /// Else, create a new team and return that new team id
        /// </returns>
        Task<int> GetTeamIdByTeamNameAsync(string teamName);

        /// <summary>
        /// There are 3 cases when syncing:
        /// 1. New teams
        ///     Add new teams to database
        /// 2. Active teams
        /// 3. Disband teams
        /// </summary>
        /// <returns></returns>
        Task SyncTeamDatabaseWithKmsAsync(DateTime now);
    }
}

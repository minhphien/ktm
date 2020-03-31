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
        /// Get a team by id
        /// </summary>
        /// <returns>An team by id</returns>
        Task<Team> GetTeamByIdAsync(int teamId);

        /// <summary>
        /// Create a team
        /// </summary>
        /// <returns></returns>
        Task CreateTeamAsync(Team team);

        /// <summary>
        /// Update a team
        /// </summary>
        /// <returns></returns>
        Task UpdateTeamAsync(Team team);

        /// <summary>
        /// Delete a team
        /// </summary>
        /// <returns></returns>
        Task DeleteTeamAsync(Team team);

        /// <summary>
        /// Get team Id from team name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns>
        /// If team exists, return team id
        /// Else, create new team and return new team id
        /// </returns>
        Task<int> GetTeamIdByTeamNameAsync(string teamName);

        /// <summary>
        /// Add new teams to database from list of all teams after GET request to KMS HRM API
        /// If team's badge is not in database, it is a new team 
        /// </summary>
        /// <returns></returns>
        Task SyncTeamDatabaseWithKmsAsync();
    }
}

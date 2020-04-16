using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.RepoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly KtmDbContext context;
        private readonly DbSet<Team> team;

        public TeamRepository(KtmDbContext context, ILogger<Team> logger) : base(context, logger)
        {
            this.context = context;
            team = context.Set<Team>();
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
        /// <returns>A collection of all teams by condition</returns>
        public async Task<IEnumerable<Team>> GetTeamsByConditionAsync(Expression<Func<Team, bool>> expression)
        {
            return await Task.FromResult(GetByCondition(expression).ToList());
        }

        /// <summary>
        /// Create teams
        /// </summary>
        /// <returns></returns>
        public async Task CreateTeams(IEnumerable<Team> teams)
        {
            team.AddRange(teams);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update teams
        /// </summary>
        /// <returns></returns>
        public async Task UpdateTeams(IEnumerable<Team> teams)
        {
            team.UpdateRange(teams);
            await context.SaveChangesAsync();
        }
    }
}

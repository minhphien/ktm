using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Entities.DTO;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class KudoRepository : BaseRepository<Kudo>, IKudoRepository
    {
        private readonly KtmDbContext context;
        private readonly DbSet<Kudo> kudo;
        private readonly IMapper _mapper;

        public KudoRepository(KtmDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            kudo = context.Set<Kudo>();
            _mapper = mapper;
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<Kudo>> GetKudosAsync()
        {
            return await Task.FromResult(GetAll().ToList());
        }

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoReportDto>> GetKudosForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds, 
            List<int> kudoTypeIds,
            bool hasDateRange)
        {
            return await kudo
                .Where(k =>
                    (!hasDateRange || k.Created >= dateFrom && k.Created <= dateTo)
                    && (teamIds.Count() == 0 || teamIds.Contains(k.Sender.TeamID))
                    && (kudoTypeIds.Count() == 0 || kudoTypeIds.Contains(k.KudoDetail.KudoTypeId)))
                .ProjectTo<KudoReportDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Get kudos summary by employee for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoSumReportDto>> GetKudosummaryEmployee(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            bool hasDateRange)
        {
            return await kudo
                .Where(k =>
                (!hasDateRange || k.Created >= dateFrom && k.Created <= dateTo)
                && filterIds.Contains(k.Sender.Id))
                .GroupBy(g => new {g.Sender.EmployeeID, g.Sender.Employee.LastName, g.Sender.Employee.FirstMidName, g.Sender.Team.TeamName })
                .Select(k => new KudoSumReportDto
                {
                    FilterName = k.Key.FirstMidName + ' ' + k.Key.LastName,
                    TeamName = k.Key.TeamName,
                    CountNums = k.Count()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get kudos summary by team for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoSumReportDto>> GetKudosummaryTeam(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            bool hasDateRange)
        {
            return await kudo
                .Where(k =>
                (!hasDateRange || k.Created >= dateFrom && k.Created <= dateTo)
                && filterIds.Contains(k.Sender.TeamID))
                .GroupBy(g => new { g.Sender.TeamID, g.Sender.Team.TeamName })
                .Select(k => new KudoSumReportDto
                {
                    FilterName = k.Key.TeamName,
                    TeamName = k.Key.TeamName,
                    CountNums = k.Count()
                })
                .ToListAsync();
        }

        /// <summary>
        /// insert mutiply kudo
        /// </summary>
        /// <param name="kudos"></param>
        /// <returns></returns>
        public async Task InsertKudos(IEnumerable<Kudo> kudos)
        {
            kudo.AddRange(kudos);
            await context.SaveChangesAsync();
        }
    }
}

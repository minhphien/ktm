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
        public async Task<IEnumerable<KudoReportDto>> GetKudosForReport(List<int> teamIds, List<int> kudoTypeIds)
        {
            return await kudo
                .Where(k => 
                    (teamIds.Contains(k.Sender.TeamID) || teamIds.Count() == 0) 
                    && (kudoTypeIds.Contains(k.KudoDetail.KudoTypeId) || kudoTypeIds.Count() == 0))
                .ProjectTo<KudoReportDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Get kudos for report with date range
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoReportDto>> GetKudosForReportWithDateRange(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds, 
            List<int> kudoTypeIds)
        {
            return await kudo
                .Where(k =>
                    k.Created >= dateFrom && k.Created <= dateTo
                    && (teamIds.Contains(k.Sender.TeamID) || teamIds.Count() == 0)
                    && (kudoTypeIds.Contains(k.KudoDetail.KudoTypeId) || kudoTypeIds.Count() == 0))
                .ProjectTo<KudoReportDto>(_mapper.ConfigurationProvider)
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

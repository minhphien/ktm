using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.DTO;
using KMS.Product.Ktm.Services.RepoInterfaces;

namespace KMS.Product.Ktm.Repository
{
    public class KudoRepository : BaseRepository<Kudo>, IKudoRepository
    {
        private readonly KtmDbContext context;
        private readonly DbSet<Kudo> kudo;
        private readonly IMapper _mapper;

        public KudoRepository(KtmDbContext context, IMapper mapper, ILogger<Kudo> logger) : base(context, logger)
        {
            this.context = context;
            kudo = context.Set<Kudo>();
            _mapper = mapper;
        }

        /// <summary>
        /// Get all kudo types
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<KudoDetailDto>> GetKudosAsync()
        {
            return await kudo
                .ProjectTo<KudoDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoDetailDto>> GetKudosForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds, 
            List<int> kudoTypeIds,
            bool hasDateRange)
        {
            return await (from k in kudo
                          join es in context.Set<Employee>() on k.SenderId equals es.Id
                          join ets in context.Set<EmployeeTeam>() on es.Id equals ets.EmployeeID
                          join ts in context.Set<Team>() on ets.TeamID equals ts.Id
                          join er in context.Set<Employee>() on k.ReceiverId equals er.Id
                          join etr in context.Set<EmployeeTeam>() on er.Id equals etr.EmployeeID
                          join tr in context.Set<Team>() on etr.TeamID equals tr.Id
                          join kd in context.Set<KudoDetail>() on k.KudoDetailId equals kd.Id
                          join ty in context.Set<KudoType>() on  kd.KudoTypeId equals ty.Id
                          where (!hasDateRange || (k.Created >= dateFrom && k.Created <= dateTo))
                            && (teamIds.Count() == 0 || teamIds.Contains(ts.Id)) 
                            && k.Created >= ets.JoinedDate
                            && (ets.ReleseadDate == null || k.Created <= ets.ReleseadDate)
                            && k.Created >= etr.JoinedDate
                            && (etr.ReleseadDate == null || k.Created <= etr.ReleseadDate)
                          select new KudoDetailDto
                          {
                              Id = k.Id,
                              Created = k.Created,
                              Modified = k.Modified,
                              Content = kd.Content,
                              TypeName = ty.TypeName,
                              SenderBadgeId = es.EmployeeBadgeId,
                              SenderFirstMidName = es.FirstMidName,
                              SenderLastName = es.LastName,
                              SenderTeam = ts.TeamName,
                              ReceiverBadgeId = er.EmployeeBadgeId,
                              ReceiverFirstMidName = er.FirstMidName,
                              ReceiverLastName = er.LastName,
                              ReceiverTeam = tr.TeamName,
                              SenderEmployeeNumber = es.EmployeeNumber,
                              ReceiverEmployeeNumber = er.EmployeeNumber
                          }
                ).ToListAsync();
        }

        public async Task<IEnumerable<KudosByTeamDto>> GetKudosByEmployeeForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds)
        {
            var selectedTeams = from team in context.Set<Team>()
                                where teamIds.Contains(team.Id)
                                select team.TeamName;

            var result = from employee in context.Set<Employee>()
                         where selectedTeams.Contains(employee.CurrentTeam)
                         select new KudosByTeamDto
                         {
                             Employee = new EmployeeInfoDto
                             {
                                 BadgeId = employee.EmployeeBadgeId,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 FirstMidName = employee.FirstMidName,
                                 LastName = employee.LastName,
                                 TeamName = employee.CurrentTeam

                             },
                             ReceivedKudos = new KudosSummaryInfoDto { Total = 
                                employee.KudoReceives
                                    .Where(i=> kudoTypeIds.Contains(i.KudoDetail.KudoType.Id))
                                    .Where(i=> dateFrom== null || (i.KudoDetail.Created >= dateFrom))
                                    .Where(i => dateTo == null || (i.KudoDetail.Modified <= dateTo))
                                    .Count() },
                             SentKudos = new KudosSummaryInfoDto { Total = 
                                employee.KudoSends
                                    .Where(i => kudoTypeIds.Contains(i.KudoDetail.KudoType.Id))
                                    .Where(i => dateFrom == null || (i.KudoDetail.Created >= dateFrom))
                                    .Where(i => dateTo == null || (i.KudoDetail.Modified <= dateTo))
                                    .Count() }
                         };

            return await(result).ToListAsync();
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
            return await (from k in kudo
                          join e in context.Set<Employee>() on k.SenderId equals e.Id
                          join et in context.Set<EmployeeTeam>() on e.Id equals et.EmployeeID
                          join t in context.Set<Team>() on et.TeamID equals t.Id
                          where (!hasDateRange || k.Created >= dateFrom && k.Created <= dateTo)
                            && (filterIds.Count == 0 || filterIds.Contains(e.Id))
                            && k.Created >= et.JoinedDate
                            && (et.ReleseadDate == null || k.Created <= et.ReleseadDate)
                          group new { e, t } by new { e.Id, e.LastName, e.FirstMidName, t.TeamName } into Result
                          select new KudoSumReportDto
                          {
                              FilterName = Result.Key.FirstMidName + ' ' + Result.Key.LastName,
                              TeamName = Result.Key.TeamName,
                              CountNums = Result.Count()
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
            return await (from k in kudo
                          join et in context.Set<EmployeeTeam>() on k.SenderId equals et.EmployeeID
                          join t in context.Set<Team>() on et.TeamID equals t.Id
                          where (!hasDateRange || k.Created >= dateFrom && k.Created <= dateTo)
                            && (filterIds.Count == 0 || filterIds.Contains(t.Id))
                            && k.Created >= et.JoinedDate
                            && (et.ReleseadDate == null || k.Created <= et.ReleseadDate)
                          group t by new { t.Id, t.TeamName } into Result
                          select new KudoSumReportDto
                          {
                              FilterName = Result.Key.TeamName,
                              TeamName = Result.Key.TeamName,
                              CountNums = Result.Count()
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

        /// <summary>
        /// get user kudo send/receive by badge id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        public async Task<UserDataDto> GetUserKudosByBadgeId(string badgeId)
        {
            var dataReturn = new UserDataDto
            {
                KudoSends = await kudo
                .Where(k => k.Sender.EmployeeBadgeId == badgeId)
                .ProjectTo<KudoDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
                KudoReceives = await kudo
                .Where(k => k.Receiver.EmployeeBadgeId == badgeId)
                .ProjectTo<KudoDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync()
            };

            return dataReturn;
        }

        public async Task<IEnumerable<KudoDetailDto>> GetSentKudosByBadgeId(string badgeId, 
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> kudoTypeIds)
        {
            var result = await kudo
                .Where(k => k.Sender.EmployeeBadgeId == badgeId &&
                    (dateFrom == null || k.Created >= dateFrom) &&
                    (dateTo == null || k.Modified <= dateTo) &&
                    kudoTypeIds.Contains(k.KudoDetail.KudoTypeId)
                )
                .ProjectTo<KudoDetailDto>(_mapper.ConfigurationProvider).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KudoDetailDto>> GetReceivedKudosByBadgeId(
            string badgeId, 
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> kudoTypeIds)
        {
            var result = await kudo
                .Where(k => k.Receiver.EmployeeBadgeId == badgeId &&
                    (dateFrom == null || k.Created >= dateFrom) &&
                    (dateTo == null || k.Modified <= dateTo) &&
                    kudoTypeIds.Contains(k.KudoDetail.KudoTypeId))
                .ProjectTo<KudoDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.DTO;

namespace KMS.Product.Ktm.Services.RepoInterfaces
{    
    public interface IKudoRepository : IBaseRepository<Kudo>
    {
        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        Task<IEnumerable<KudoDetailDto>> GetKudosAsync();

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoDetailDto>> GetKudosForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds,
            bool hasDateRange);

        /// <summary>
        /// Get kudos summary by employee for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoSumReportDto>> GetKudosummaryEmployee(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            bool hasDateRange);

        /// <summary>
        /// Get kudos summary by team for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoSumReportDto>> GetKudosummaryTeam(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            bool hasDateRange);

        /// <summary>
        /// insert mutiply kudo
        /// </summary>
        /// <param name="kudos"></param>
        /// <returns></returns>
        Task InsertKudos(IEnumerable<Kudo> kudo);

        /// <summary>
        /// get user kudo send/receive by badge id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        Task<UserDataDto> GetUserKudosByBadgeId(string badgeId);

        /// <summary>
        /// Get kudos list by employee for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudosByTeamDto>> GetKudosByEmployeeForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds);

        /// <summary>Gets the sent kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="kudoTypeIds"></param>
        /// <returns></returns>
        Task<IEnumerable<KudoDetailDto>> GetSentKudosByBadgeId(string badgeId, DateTime? dateFrom, DateTime? dateTo, List<int> kudoTypeIds);

        /// <summary>Gets the received kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="kudoTypeIds"></param>
        /// <returns></returns>
        Task<IEnumerable<KudoDetailDto>> GetReceivedKudosByBadgeId(string badgeId, DateTime? dateFrom, DateTime? dateTo, List<int> kudoTypeIds);

        /// <summary>Gets the kudos across team for report.</summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="teamIds">The team ids.</param>
        /// <param name="kudoTypeIds">The kudo type ids.</param>
        /// <returns></returns>
        Task<IEnumerable<KudosAcrossTeamDto>> GetKudosAcrossTeamForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds);
    }
}

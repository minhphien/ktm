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
    }
}

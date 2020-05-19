using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Entities.DTO;

namespace KMS.Product.Ktm.Services.KudoService
{
    public interface IKudoService
    {
        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        Task<IEnumerable<KudoDetailDto>> GetAllKudosAsync();

        /// <summary>
        /// Get kudo by id
        /// </summary>
        /// <returns>Returns a kudo by id</returns>
        Task<Kudo> GetKudoByIdAsync(int kudoId);

        /// <summary>
        /// Create a new kudo
        /// </summary>
        /// <returns></returns>
        Task CreateKudoAsync(Kudo kudo);

        /// <summary>
        /// Update an existing kudo
        /// </summary>
        /// <returns></returns>
        Task UpdateKudoAsync(Kudo kudo);

        /// <summary>
        /// Delete an existing kudo
        /// </summary>
        /// <returns></returns>
        Task DeleteKudoAsync(Kudo kudo);

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudosByTeamDto>> GetKudosForReport(
            DateTime? dateFrom, 
            DateTime? dateTo, 
            List<int> teamIds, 
            List<int> kudoTypeIds);

        /// <summary>Gets the kudos across team report.</summary>
        /// <param name="dateFrom">The date from.</param>
        /// <param name="dateTo">The date to.</param>
        /// <param name="teamIds">The team ids.</param>
        /// <param name="kudoTypeIds">The kudo type ids.</param>
        /// <returns></returns>
        Task<IEnumerable<KudosAcrossTeamDto>> GetKudosAcrossTeamReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds);

        /// <summary>
        /// Get kudos summary for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoSumReportDto>> GetKudosummaryForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            int summaryReportType);

        /// <summary>
        /// add kudos from emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        Task InsertKudoFromEmails(List<EmailMessage> emails);

        /// <summary>
        /// get user kudo send/receive by badge id
        /// </summary>
        /// <param name="badgeId"></param>
        /// <returns></returns>
        Task<UserDataDto> GetUserKudosByBadgeId(string badgeId);

        /// <summary>
        /// create kudo with username
        /// </summary>
        /// <param name="kudo"></param>
        /// <returns></returns>
        Task CreateKudoByUserNameAsync(KudoDto kudo);

        /// <summary>Gets the received kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="kudoTypeIds"></param>
        /// <returns></returns>
        Task<IEnumerable<KudoDetailDto>> GetReceivedKudosByBadgeId(
            string badgeId, 
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> kudoTypeIds);

        /// <summary>Gets the sent kudos by badge identifier.</summary>
        /// <param name="badgeId">The badge ID.</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="kudoTypeIds"></param>
        /// <returns></returns>
        Task<IEnumerable<KudoDetailDto>> GetSentKudosByBadgeId(
            string badgeId,
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> kudoTypeIds);
    }
}

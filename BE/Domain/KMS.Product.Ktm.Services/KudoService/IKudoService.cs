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


    }
}

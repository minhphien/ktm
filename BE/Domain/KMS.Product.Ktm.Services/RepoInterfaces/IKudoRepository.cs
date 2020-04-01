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
        Task<IEnumerable<Kudo>> GetKudosAsync();

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoReportDto>> GetKudosForReport(List<int> teamIds, List<int> kudoTypeIds);

        /// <summary>
        /// Get kudos for report with date range
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        Task<IEnumerable<KudoReportDto>> GetKudosForReportWithDateRange(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> teamIds,
            List<int> kudoTypeIds);

        /// <summary>
        /// insert mutiply kudo
        /// </summary>
        /// <param name="kudos"></param>
        /// <returns></returns>
        Task InsertKudos(IEnumerable<Kudo> kudo);
    }
}

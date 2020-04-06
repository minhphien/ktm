using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Entities.Common;
using KMS.Product.Ktm.Entities.DTO;
using KMS.Product.Ktm.Services.AppConstants;

namespace KMS.Product.Ktm.Services.KudoService
{
    public class KudoService : IKudoService
    {
        private readonly IKudoRepository _kudoRepository;
        private readonly IEmployeeTeamRepository _employeeTeamRepository;

        /// <summary>
        /// Inject Kudo repository
        /// </summary>
        /// <returns></returns>
        public KudoService(IKudoRepository kudoRepository, IEmployeeTeamRepository employeeTeamRepository)
        {
            _kudoRepository = kudoRepository ?? throw new ArgumentNullException($"{nameof(kudoRepository)}");
            _employeeTeamRepository = employeeTeamRepository ?? throw new ArgumentNullException($"{nameof(employeeTeamRepository)}");
        }

        /// <summary>
        /// Get all kudos
        /// </summary>
        /// <returns>Returns a collection of all kudos</returns>
        public async Task<IEnumerable<Kudo>> GetAllKudosAsync()
        {
            return await _kudoRepository.GetKudosAsync();
        }

        /// <summary>
        /// Get kudo by id
        /// </summary>
        /// <returns>Returns a kudo by id</returns>
        public async Task<Kudo> GetKudoByIdAsync(int kudoId)
        {
            return await _kudoRepository.GetByIdAsync(kudoId);
        }

        /// <summary>
        /// Create a new kudo type
        /// </summary>
        /// <returns></returns>
        public async Task CreateKudoAsync(Kudo kudo)
        {
            await _kudoRepository.InsertAsync(kudo);
        }

        /// <summary>
        /// Update an existing kudo
        /// </summary>
        /// <returns></returns>
        public async Task UpdateKudoAsync(Kudo kudo)
        {
            await _kudoRepository.UpdateAsync(kudo);
        }

        /// <summary>
        /// Delete an existing kudo
        /// </summary>
        /// <returns></returns>
        public async Task DeleteKudoAsync(Kudo kudo)
        {
            await _kudoRepository.DeleteAsync(kudo);
        }

        /// <summary>
        /// Get kudos for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoReportDto>> GetKudosForReport(
            DateTime? dateFrom, 
            DateTime? dateTo, 
            List<int> teamIds, 
            List<int> kudoTypeIds)
        {
            if(dateFrom != null && dateTo != null)
            {
                return await _kudoRepository.GetKudosForReport(dateFrom, dateTo, teamIds, kudoTypeIds, true);
            }
            else
            {
                return await _kudoRepository.GetKudosForReport(dateFrom, dateTo, teamIds, kudoTypeIds, false);
            }
        }

        /// <summary>
        /// Get kudos summary for report
        /// </summary>
        /// <returns>Returns a collection of kudos</returns>
        public async Task<IEnumerable<KudoSumReportDto>> GetKudosummaryForReport(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int> filterIds,
            int summaryReportType)
        {
            bool hasDateRange = false;
            if (dateFrom != null && dateTo != null)
            {
                hasDateRange = true;
            }

            if(summaryReportType == KudoConstants.summaryReportType.TEAM)
            { 
                return await _kudoRepository.GetKudosummaryTeam(dateFrom, dateTo, filterIds, hasDateRange);
            }
            else
            {
                return await _kudoRepository.GetKudosummaryEmployee(dateFrom, dateTo, filterIds, hasDateRange);
            }
        }

        /// <summary>
        /// add kudos from emails
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public async Task InsertKudoFromEmails(List<EmailMessage> emails)
        {
            var kudos = new List<Kudo>();

            foreach(var email in emails)
            {
                var senders = await _employeeTeamRepository.GetEmployeeTeamByEmails(email.FromAddresses.Select(i => i.Address.ToString()).ToList());
                if(senders.Count() > 0)
                {
                    var senderInfo = senders.Single();
                    var receivers = await _employeeTeamRepository.GetEmployeeTeamByEmails(email.ToAddresses.Select(i => i.Address.ToString()).ToList());
                    if(receivers.Count() > 0)
                    {
                        foreach(var receiver in receivers)
                        {
                            var kudo = new Kudo
                            {
                                Sender = senderInfo,
                                Receiver = receiver,
                                KudoDetail = new KudoDetail
                                {
                                    Content = email.Content,
                                    //default kudo type
                                    KudoTypeId = 1
                                }
                            };

                            kudos.Add(kudo);
                        }
                    }
                }
            }

            if(kudos.Count > 0)
            {
                await _kudoRepository.InsertKudos(kudos);
            }
        }
    }
}

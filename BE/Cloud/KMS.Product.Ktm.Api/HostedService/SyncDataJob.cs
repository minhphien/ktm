using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using KMS.Product.Ktm.Services.EmployeeService;
using KMS.Product.Ktm.Services.TeamService;
using Microsoft.Extensions.DependencyInjection;

namespace KMS.Product.Ktm.Api.HostedService
{
    public class SyncDataJob : CronJobService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SyncDataJob> _logger;

        public SyncDataJob(IScheduleConfig<SyncDataJob> config, IServiceScopeFactory scopeFactory, ILogger<SyncDataJob> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        /// <summary>
        /// start job
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sync Data starts.");
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// working on the job
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            //sync team
            var teamService = scope.ServiceProvider.GetRequiredService<ITeamService>();
            await teamService.SyncTeamDatabaseWithKmsAsync(DateTime.Now);

            //sync employee
            var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
            await employeeService.SyncEmployeeDatabaseWithKms(DateTime.Now);
        }

        /// <summary>
        /// stop job
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sync Data is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
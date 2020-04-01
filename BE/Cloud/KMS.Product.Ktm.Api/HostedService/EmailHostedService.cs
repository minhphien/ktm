using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using KMS.Product.Ktm.Services.EmailService;

namespace KMS.Product.Ktm.Api.HostedService
{
    public class EmailHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EmailHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// start the background task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var iIdleEmailService = scope.ServiceProvider.GetRequiredService<IIdleEmailService>();
            await iIdleEmailService.RunAsync();
        }

        /// <summary>
        /// end the background task
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

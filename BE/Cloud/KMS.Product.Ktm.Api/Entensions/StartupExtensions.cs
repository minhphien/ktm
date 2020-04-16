using KMS.Product.Ktm.Repository;
using KMS.Product.Ktm.Services.AuthenticateService;
using KMS.Product.Ktm.Services.EmailService;
using KMS.Product.Ktm.Services.KudoService;
using KMS.Product.Ktm.Services.KudoTypeService;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Services.SlackService;
using KMS.Product.Ktm.Services.EmployeeService;
using KMS.Product.Ktm.Services.TeamService;
using Microsoft.Extensions.DependencyInjection;

namespace KMS.Product.Ktm.Api.Entensions
{
    internal static class StartupExtensions
    {
        internal static void AddKtmRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IKudoTypeRepository, KudoTypeRepository>();
            services.AddScoped<IKudoRepository, KudoRepository>();
            services.AddScoped<IKudosDetailRepository, KudosDetailRepository>();
            services.AddScoped<IEmployeeTeamRepository, EmployeeTeamRepository>();
            services.AddScoped<IKudosDetailRepository, KudosDetailRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
        }
        internal static void AddKtmServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IKudoTypeService, KudoTypeService>();
            services.AddScoped<IKudoService, KudoService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IIdleEmailService, IdleEmailService>();
            services.AddScoped<ISlackService, SlackService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ITeamService, TeamService>();
        }
    }
}

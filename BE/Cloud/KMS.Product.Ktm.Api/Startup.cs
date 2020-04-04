using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using KMS.Product.Ktm.Api.Authentication;
using KMS.Product.Ktm.Repository;
using KMS.Product.Ktm.Entities.Configurations;
using KMS.Product.Ktm.Entities.Profiles;
using KMS.Product.Ktm.Services.KudoTypeService;
using KMS.Product.Ktm.Services.KudoService;
using KMS.Product.Ktm.Services.EmailService;
using KMS.Product.Ktm.Services.RepoInterfaces;
using KMS.Product.Ktm.Services.SlackService;

namespace KMS.Product.Ktm.Api
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json",
                         optional: false,
                         reloadOnChange: true)
            .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                var settings = builder.Build();
                builder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(settings["ConnectionStrings:AppConfig"]);
                });
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 
            services.AddAuthentication("KmsTokenAuth")
                .AddScheme<KmsTokenAuthOptions, KmsTokenAuthHandler>("KmsTokenAuth", "KmsTokenAuth", opts => { });
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddDbContextPool<KtmDbContext>(
                options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("KMS.Product.Ktm.Repository"))
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(MyLoggerFactory));
            services.AddScoped<IKudoTypeService, KudoTypeService>();
            services.AddScoped<IKudoService, KudoService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IIdleEmailService, IdleEmailService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IKudoTypeRepository, KudoTypeRepository>();
            services.AddScoped<IKudoRepository, KudoRepository>();
            services.AddScoped<IEmployeeTeamRepository, EmployeeTeamRepository>();
            services.AddSingleton<ISlackService, SlackService>();
            // mapper
            services.AddAutoMapper(typeof(KudosProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

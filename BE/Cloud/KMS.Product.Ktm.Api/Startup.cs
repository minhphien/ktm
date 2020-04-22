using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using KMS.Product.Ktm.Repository;
using KMS.Product.Ktm.Api.Authentication;
using KMS.Product.Ktm.Entities.Configurations;
using KMS.Product.Ktm.Entities.Profiles;
using KMS.Product.Ktm.Services.AutoMapper;
using KMS.Product.Ktm.Services.SlackService;
using KMS.Product.Ktm.Api.Entensions;
using KMS.Product.Ktm.Api.HostedService;

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
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSlackClient(Configuration);
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
            services.AddKtmRepositories();
            services.AddKtmServices();
            // mapper
            services.AddAutoMapper(typeof(KudosProfile), typeof(AutoMapperProfile));
            // cache
            services.AddMemoryCache();
            // cron job
            services.AddCronJob<SyncDataJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = Configuration.GetValue<string>("CronTimmer"); //use @"*/5 * * * *" every five minutes for testing
            });
            //json
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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

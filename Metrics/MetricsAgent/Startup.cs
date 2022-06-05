using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;
using MetricsAgent.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(Configuration.GetSection("Settings:DatabaseOptions:ConnectionString").Value)
                .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(lb => lb
                .AddFluentMigratorConsole());

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(CpuMetricJob),
            cronExpression: "0/5 * * * * ?")); // Запускать каждые 5 секунд
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(RamMetricJob),
            cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(HddMetricJob),
            cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(NetworkMetricJob),
            cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<DotnetMetricJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(DotnetMetricJob),
            cronExpression: "0/5 * * * * ?"));
            services.AddHostedService<QuartzHostedService>();


            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>()
               .Configure<DatabaseOptions>(options =>
               {
                   Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
               });
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>()
               .Configure<DatabaseOptions>(options =>
               {
                   Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
               });
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>()
               .Configure<DatabaseOptions>(options =>
               {
                   Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
               });
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>()
               .Configure<DatabaseOptions>(options =>
               {
                   Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
               });
            services.AddSingleton<IDotNetMetricsRepository,DotNetMetricsRepository>()
               .Configure<DatabaseOptions>(options =>
               {
                   Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
               });

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}

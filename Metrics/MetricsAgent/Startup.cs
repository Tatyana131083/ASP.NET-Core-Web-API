using AutoMapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;
using System.Data.SQLite;

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
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();

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

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source = metrics.db; Version = 3; Pooling = true; Max Pool Size = 100; ";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }
        private void PrepareSchema(SQLiteConnection connection)
        {
            CreateTable(connection, "cpu_metrics");
            CreateTable(connection, "dotnet_metrics");
            CreateTable(connection, "hdd_metrics");
            CreateTable(connection, "network_metrics");
            CreateTable(connection, "ram_metrics");
        }

        private void CreateTable(SQLiteConnection connection, string table)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // Задаём новый текст команды для выполнения
                // Удаляем таблицу с метриками, если она есть в базе данных
                command.CommandText = $"DROP TABLE IF EXISTS {table}";

                // Отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText = $"CREATE TABLE {table} (id INTEGER PRIMARY KEY, value INT, time INT)";
                command.ExecuteNonQuery();
                //Наполняем таблицу
                command.CommandText = $"INSERT INTO {table} (value, time) VALUES(10, 1)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {table} (value, time) VALUES(20, 5)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {table} (value, time) VALUES(30, 10)";
                command.ExecuteNonQuery();
                command.CommandText = $"INSERT INTO {table} (value, time) VALUES(40, 15)";
                command.ExecuteNonQuery();

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}

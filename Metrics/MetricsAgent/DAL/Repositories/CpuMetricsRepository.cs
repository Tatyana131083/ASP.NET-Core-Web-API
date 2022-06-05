using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace MetricsAgent.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly string ConnectionString;

        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            ConnectionString = databaseOptions.Value.ConnectionString; 
        }
        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO cpu_metrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });

        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM cpu_metrics WHERE id=@id", new
            {
                id = id
            });
        }

        public void Update(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE cpu_metrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<CpuMetric> metrics = connection.Query<CpuMetric>("SELECT * FROM cpu_metrics").AsList();
            return metrics;
        }

        public CpuMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            CpuMetric metric = connection.QuerySingle<CpuMetric>("SELECT * FROM cpu_metrics WHERE id=@id", new
            {
                id = id
            });
            return metric;
        }

        /// <summary>
        /// Получить статистику по нагрузке на ЦП за период
        /// </summary>
        /// <param name="fromTime">Время начала периода</param>
        /// <param name="toTime">Время окончания периода</param>
        /// <returns></returns>
        public IList<CpuMetric> GetByTimePeriod(TimeSpan from, TimeSpan to)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<CpuMetric> metrics = connection.Query<CpuMetric>("SELECT * FROM cpu_metrics WHERE time >= @fromSec AND time <= @toSec", new
            {
                fromSec = from.TotalSeconds,
                toSec = to.TotalSeconds
            }).AsList();
            return metrics;
           
        }

    }
}

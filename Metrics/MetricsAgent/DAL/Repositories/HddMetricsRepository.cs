using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly string ConnectionString;

        public HddMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            ConnectionString = databaseOptions.Value.ConnectionString;
        }

        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO hdd_metrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });

        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM hdd_metrics WHERE id=@id", new
            {
                id = id
            });
        }

        public void Update(HddMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE hdd_metrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<HddMetric> metrics = connection.Query<HddMetric>("SELECT * FROM hdd_metrics").AsList();
            return metrics;
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            HddMetric metric = connection.QuerySingle<HddMetric>("SELECT * FROM hdd_metrics WHERE id=@id", new
            {
                id = id
            });
            return metric;
        }

        public IList<HddMetric> GetByTimePeriod(TimeSpan from, TimeSpan to)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<HddMetric> metrics = connection.Query<HddMetric>("SELECT * FROM hdd_metrics WHERE time >= @fromSec AND time <= @toSec", new
            {
                fromSec = from.TotalSeconds,
                toSec = to.TotalSeconds
            }).AsList();
            return metrics;
        }
    }
}

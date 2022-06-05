using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly string ConnectionString;

        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            ConnectionString = databaseOptions.Value.ConnectionString;
        }

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO ram_metrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });

        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM ram_metrics WHERE id=@id", new
            {
                id = id
            });
        }

        public void Update(RamMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE ram_metrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<RamMetric> metrics = connection.Query<RamMetric>("SELECT * FROM ram_metrics").AsList();
            return metrics;
        }

        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            RamMetric metric = connection.QuerySingle<RamMetric>("SELECT * FROM ram_metrics WHERE id=@id", new
            {
                id = id
            });
            return metric;
        }

        public IList<RamMetric> GetByTimePeriod(TimeSpan from, TimeSpan to)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<RamMetric> metrics = connection.Query<RamMetric>("SELECT * FROM ram_metrics WHERE time >= @fromSec AND time <= @toSec", new
            {
                fromSec = from.TotalSeconds,
                toSec = to.TotalSeconds
            }).AsList();
            return metrics;
        }
    }
}

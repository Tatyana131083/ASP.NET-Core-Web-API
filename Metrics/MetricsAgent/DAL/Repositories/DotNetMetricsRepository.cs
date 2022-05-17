using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO dotnet_metrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });

        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM dotnet_metrics WHERE id=@id", new
            {
                id = id
            });
        }

        public void Update(DotNetMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE dotnet_metrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<DotNetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<DotNetMetric> metrics = connection.Query<DotNetMetric>("SELECT * FROM dotnet_metrics").AsList();
            return metrics;
        }

        public DotNetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            DotNetMetric metric = connection.QuerySingle<DotNetMetric>("SELECT * FROM dotnet_metrics WHERE id=@id", new
            {
                id = id
            });
            return metric;
        }


        public IList<DotNetMetric> GetByTimePeriod(TimeSpan from, TimeSpan to)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<DotNetMetric> metrics = connection.Query<DotNetMetric>("SELECT * FROM dotnet_metrics WHERE time >= @fromSec AND time <= @toSec", new
            {
                fromSec = from.TotalSeconds,
                toSec = to.TotalSeconds
            }).AsList();
            return metrics;
        }
    }
}

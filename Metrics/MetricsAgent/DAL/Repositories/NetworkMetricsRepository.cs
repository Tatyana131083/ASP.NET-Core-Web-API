using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO network_metrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM network_metrics WHERE id=@id", new
            {
                id = id
            });
        }

        public void Update(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE network_metrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<NetworkMetric> metrics = connection.Query<NetworkMetric>("SELECT * FROM network_metrics").AsList();
            return metrics;
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            NetworkMetric metric = connection.QuerySingle<NetworkMetric>("SELECT * FROM network_metrics WHERE id=@id", new
            {
                id = id
            });
            return metric;
        }
        public IList<NetworkMetric> GetByTimePeriod(TimeSpan from, TimeSpan to)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<NetworkMetric> metrics = connection.Query<NetworkMetric>("SELECT * FROM network_metrics WHERE time >= @fromSec AND time <= @toSec", new
            {
                fromSec = from.TotalSeconds,
                toSec = to.TotalSeconds
            }).AsList();
            return metrics;
        }
    }
}

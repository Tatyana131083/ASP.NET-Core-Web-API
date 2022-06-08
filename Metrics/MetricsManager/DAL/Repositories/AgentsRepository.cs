using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class AgentsRepository : IAgentRepository
    {
        private readonly string ConnectionString;

        public AgentsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            ConnectionString = databaseOptions.Value.ConnectionString;
        }

        public void Create(AgentInfo item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO agents(Url) VALUES(@url)", new
            {
                url = item.Url
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("DELETE FROM agents WHERE Id=@id;", new
            {
                id = id
            });
        }

        public void Update(AgentInfo item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("UPDATE agents SET Url = @url WHERE Id=@id; ", new
            {
                url = item.Url,
                id = item.Id
            }) ;
        }

        public IList<AgentInfo> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            List<AgentInfo> metrics = connection.Query<AgentInfo>("SELECT * FROM agents;").ToList();
            return metrics;
        }

        public AgentInfo GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            try
            {
                AgentInfo metric = connection.QuerySingle<AgentInfo>("SELECT * FROM agents WHERE Id=@id;", new
                {
                    id = id
                });
                return metric;
            }
            catch (InvalidOperationException)
            {
                return null;
            }            
        }
    }
}

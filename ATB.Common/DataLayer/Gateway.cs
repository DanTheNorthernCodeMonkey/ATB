using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace ATB.Common.DataLayer
{
    public class Gateway : IGateway
    {
        private readonly ILogger<Gateway> logger;
        private readonly IConfiguration config;
        private const string AtbKey = "atb";

        public Gateway(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<T> Get<T>(string query, object parameters)
        {
            var result = await Execute(async connection => (await connection.QueryAsync<T>(query, parameters, null, 60, CommandType.Text)));
            return result.FirstOrDefault();
        }
        
        public async Task<IEnumerable<T>> GetCollection<T>(string query, object parameters)
        {
            var results = await Execute(async connection => (await connection.QueryAsync<T>(query, parameters, null, 60, CommandType.Text)));

            return results.ToArray();
        }
        
        public async Task<int> Execute(string sqlCommand, object parameters)
        {
            return await Execute(async connection => await connection.ExecuteAsync(sqlCommand, parameters, null, 60, CommandType.Text));
        }
        
        private async Task<T> Execute<T>(Func<IDbConnection, Task<T>> func)
        {
            try
            {
                var connectionString = config.GetConnectionString(AtbKey);
                using ( var connection = new NpgsqlConnection(connectionString))
                {
                    return await func.Invoke(connection);
                }
            }
            catch (Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex, "Data layer failure");
                return default;
            }
        }
    }
}
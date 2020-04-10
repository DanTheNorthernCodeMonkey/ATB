using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ATB.Infrastructure.DataLayer
{
    public class Gateway : IGateway
    {
        private readonly IConfiguration config;
        private const string AtbKey = "atb";

        public Gateway(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<ExecutionResult<T>> Get<T>(string query, object parameters)
        {
            return await Execute(async connection =>
            {
	            var result = (await connection.QueryAsync<T>(query, parameters, null, 60, CommandType.Text)).ToList();

                return new ExecutionResult<T> { DataSet = result, Status = result.Any() ? ExecutionStatus.Success : ExecutionStatus.NoData};
            });
        }
       
        public async Task<ExecutionResult<int>> ExecuteCommand(string sqlCommand, object parameters)
        {
            var result = await Execute(async connection =>
            {
	            var rowCount = await connection.ExecuteAsync(sqlCommand, parameters, null, 60, CommandType.Text);

                return new ExecutionResult<int> { DataSet = new[] { rowCount }, Status = rowCount > 0 ? ExecutionStatus.Success : ExecutionStatus.Fail};
            });

            return result;
        }
        
        private async Task<ExecutionResult<T>> Execute<T>(Func<IDbConnection, Task<ExecutionResult<T>>> func)
        {
            try
            {
                var connectionString = config.GetConnectionString(AtbKey);

                await using var connection = new NpgsqlConnection(connectionString);

                return await func.Invoke(connection);
            }
            catch (Exception ex)
            {
	            return new ExecutionResult<T>
	            {
		            Status = ExecutionStatus.Fail
	            };
            }
        }
    }
}
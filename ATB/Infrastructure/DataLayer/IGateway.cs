using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATB.Infrastructure.DataLayer
{
    public interface IGateway
    {
	    Task<ExecutionResult<T>> Get<T>(string query, object parameters);

		Task<ExecutionResult<int>> ExecuteCommand(string sqlCommand, object parameters);
    }
}
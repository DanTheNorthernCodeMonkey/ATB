using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATB.Common.DataLayer
{
    public interface IGateway
    {
        Task<T> Get<T>(string query, object parameters);
        Task<IEnumerable<T>> GetCollection<T>(string query, object parameters);
        Task<int> Execute(string sqlCommand, object parameters);
    }
}
using System.Threading.Tasks;
using ATB.Infrastructure;

namespace ATB.Data.Repository
{
	public interface IBeanAdvertRepository
	{
		Task<ExecutionResult<int>> Insert(BeanAdvertModel model);
	}
}
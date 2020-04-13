using System;
using System.Threading.Tasks;
using ATB.Infrastructure;

namespace ATB.Data.ReadModel
{
	public interface IBeanAdvertReadModel
	{
		Task<ExecutionResult<BeanAdvertProjection>> GetBeanFor(DateTime date);
		Task<ExecutionResult<BeanAdvertProjection>> GetCurrentBeans(DateTime date);
	}
}
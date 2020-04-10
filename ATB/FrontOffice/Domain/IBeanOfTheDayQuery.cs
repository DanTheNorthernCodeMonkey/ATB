using System.Threading.Tasks;
using ATB.Data;
using ATB.Data.ReadModel;
using ATB.Infrastructure;

namespace ATB.FrontOffice.Domain
{
    public interface IBeanOfTheDayQuery
    {
	    Task<ExecutionResult<BeanAdvertProjection>> GetBeanOfTheDay();
    }
}
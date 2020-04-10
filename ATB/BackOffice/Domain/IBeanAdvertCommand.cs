using System.Threading.Tasks;
using ATB.Data;
using ATB.Data.Repository;
using ATB.Infrastructure;

namespace ATB.BackOffice.Domain
{
    public interface IBeanAdvertCommand
    {
	    Task<ExecutionStatus> StoreBean(BeanAdvertModel model);
    }
}
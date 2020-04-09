using System.Threading.Tasks;
using ATB.BackOffice.BeanAdvert.Data;

namespace ATB.BackOffice.BeanAdvert.Domain
{
    public interface IBeanAdvertCommand
    {
        Task<int> StoreBean(BeanAdvertModel model);
    }
}
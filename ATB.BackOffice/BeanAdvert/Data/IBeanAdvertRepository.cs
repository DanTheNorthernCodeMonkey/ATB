using System.Threading.Tasks;

namespace ATB.BackOffice.BeanAdvert.Data
{
    public interface IBeanAdvertRepository
    {
        Task<int> Insert(BeanAdvertModel model);
    }
}
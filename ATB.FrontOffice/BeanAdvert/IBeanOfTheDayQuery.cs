using System.Threading.Tasks;
using ATB.Common.BeanAdvert;

namespace ATB.FrontOffice.BeanAdvert
{
    public interface IBeanOfTheDayQuery
    {
        Task<BeanAdvertProjection> GetBeanOfTheDay();
    }
}
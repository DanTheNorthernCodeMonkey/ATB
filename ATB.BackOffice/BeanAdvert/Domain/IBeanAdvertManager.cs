using System;
using System.Threading.Tasks;

namespace ATB.BackOffice.BeanAdvert.Domain
{
    public interface IBeanAdvertManager
    {
        Task<DateStatus> GetDateStatus(DateTime date);
    }
}
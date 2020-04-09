using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATB.Common.BeanAdvert
{
    public interface IBeanAdvertReadModel
    {
        Task<BeanAdvertProjection> GetBeanFor(DateTime date);
        Task<IEnumerable<BeanAdvertProjection>> GetCurrentBeans(DateTime date);
    }
}
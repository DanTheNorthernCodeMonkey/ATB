using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATB.Common.BeanAdvert;

namespace ATB.BackOffice.BeanAdvert.Domain
{
    public class BeanAdvertManager : IBeanAdvertManager
    {
        private readonly IBeanAdvertReadModel beanAdvertReadModel;

        public BeanAdvertManager(IBeanAdvertReadModel beanAdvertReadModel)
        {
            this.beanAdvertReadModel = beanAdvertReadModel;
        }

        public async Task<DateStatus> GetDateStatus(DateTime date)
        {
            var currentBeans = await this.beanAdvertReadModel.GetCurrentBeans(DateTime.UtcNow);

            if (currentBeans == null)
                return DateStatus.Error;

            if (date.Date < DateTime.UtcNow.Date)
                return DateStatus.Past;
            
            if (currentBeans.ToArray().Any(x => x.Date.Date == date.Date))
                return DateStatus.Taken;

            return DateStatus.Free;
        }
    }
}
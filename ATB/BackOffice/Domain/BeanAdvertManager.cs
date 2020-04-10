using System;
using System.Linq;
using System.Threading.Tasks;
using ATB.Data;
using ATB.Data.ReadModel;
using ATB.Infrastructure;

namespace ATB.BackOffice.Domain
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
	        if (date.Date < DateTime.UtcNow.Date)
		        return DateStatus.Past;

            var currentBeans = await this.beanAdvertReadModel.GetCurrentBeans(DateTime.UtcNow);

            if (currentBeans.Status == ExecutionStatus.Fail)
                return DateStatus.Error;

            if (currentBeans.Status == ExecutionStatus.NoData)
	            return DateStatus.Free;

            if (currentBeans.DataSet.Any(x => x.Date.Date == date.Date))
                return DateStatus.Taken;

            return DateStatus.Free;
        }
    }
}
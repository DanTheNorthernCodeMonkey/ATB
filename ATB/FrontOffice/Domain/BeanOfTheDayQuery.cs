using System;
using System.Threading.Tasks;
using ATB.Data;
using ATB.Data.ReadModel;
using ATB.Infrastructure;
using ATB.Infrastructure.Services;

namespace ATB.FrontOffice.Domain
{
    public class BeanOfTheDayQuery : IBeanOfTheDayQuery
    {
        private readonly IBeanAdvertReadModel beanReadModel;
        private readonly IFakeS3Service fakeS3Service;

        public BeanOfTheDayQuery(IBeanAdvertReadModel beanReadModel, IFakeS3Service fakeS3Service)
        {
            this.beanReadModel = beanReadModel;
            this.fakeS3Service = fakeS3Service;
        }

        public async Task<ExecutionResult<BeanAdvertProjection>> GetBeanOfTheDay()
        {
            var beanOfTheDay = await this.beanReadModel.GetBeanFor(DateTime.UtcNow.Date);

            if (beanOfTheDay.Status != ExecutionStatus.Success)
	            return beanOfTheDay;

            beanOfTheDay.Data.ImageUri = fakeS3Service.ImageUri(beanOfTheDay.Data.ImageId);

            return beanOfTheDay;
        }
    }
}
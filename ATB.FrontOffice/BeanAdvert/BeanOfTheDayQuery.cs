using System;
using System.Threading.Tasks;
using ATB.Common;
using ATB.Common.BeanAdvert;

namespace ATB.FrontOffice.BeanAdvert
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

        public async Task<BeanAdvertProjection> GetBeanOfTheDay()
        {
            var beanOfTheDay = await this.beanReadModel.GetBeanFor(DateTime.UtcNow.Date);
            
            if (beanOfTheDay == null)
                return new BeanAdvertProjection();

            beanOfTheDay.ImageUri = fakeS3Service.ImageUri(beanOfTheDay.ImageId);

            return beanOfTheDay;
        }
    }
}
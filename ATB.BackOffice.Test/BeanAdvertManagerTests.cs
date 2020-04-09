using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ATB.BackOffice.BeanAdvert.Domain;
using ATB.Common.BeanAdvert;
using ATB.Common.DataLayer;
using Moq;
using NUnit.Framework;

namespace ATB.BackOffice.Test
{
    // Should have a wrapper around datetime to enable mocking of dates that use now in logic to avoid tests failing e.g. Nightly Builds.
    public class BeanAdvertManagerTests
    {
        private Mock<IBeanAdvertReadModel> readModel;
        private IBeanAdvertManager beanAdvertManager;

        [SetUp]
        public void Setup()
        {
            var mockDataSet = new List<BeanAdvertProjection>
            {
                new BeanAdvertProjection(Guid.NewGuid(), 0, "Tomorrow","","",Guid.NewGuid(), DateTime.UtcNow.AddDays(1).Date),
                new BeanAdvertProjection(Guid.NewGuid(), 0, "Today","","",Guid.NewGuid(), DateTime.UtcNow.Date),
            };
        
            this.readModel = new Mock<IBeanAdvertReadModel>();
            this.readModel.Setup(x => x.GetCurrentBeans(It.IsAny<DateTime>())).Returns(Task.FromResult<IEnumerable<BeanAdvertProjection>>(mockDataSet.ToArray()));
            
            this.beanAdvertManager = new BeanAdvertManager(readModel.Object);
        }
        
        [Test]
        public async Task DateAllowed_DateFree_ReturnsFree()
        {
            var result = await this.beanAdvertManager.GetDateStatus(DateTime.UtcNow.AddDays(2).Date);
            
            Assert.AreEqual(DateStatus.Free, result);
        }
        
        [Test]
        public async Task DateAllowed_DateTaken_ReturnsTaken()
        {
            var result = await this.beanAdvertManager.GetDateStatus(DateTime.UtcNow.AddDays(1).Date);
            
            Assert.AreEqual(DateStatus.Taken, result);
        }
        
        [Test]
        public async Task DateAllowed_DateInPast_ReturnsPast()
        {
            var result = await this.beanAdvertManager.GetDateStatus(DateTime.UtcNow.AddDays(-1).Date);
            
            Assert.AreEqual(DateStatus.Past, result);
        }
    }
}
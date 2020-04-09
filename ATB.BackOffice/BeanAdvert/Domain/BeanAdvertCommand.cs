using System.Threading.Tasks;
using ATB.BackOffice.BeanAdvert.Data;
using ATB.Common;

namespace ATB.BackOffice.BeanAdvert.Domain
{
    public class BeanAdvertCommand : IBeanAdvertCommand
    {
        private readonly IFakeS3Service fakeS3Service;
        private readonly IBeanAdvertRepository repo;

        public BeanAdvertCommand(IFakeS3Service fakeS3Service, IBeanAdvertRepository repo)
        {
            this.fakeS3Service = fakeS3Service;
            this.repo = repo;
        }

        public async Task<int> StoreBean(BeanAdvertModel model) 
        {
            var upload = this.fakeS3Service.Upload(model.ImageId, model.Image64);
            var insertCount = repo.Insert(model);

            await Task.WhenAll(upload, insertCount);

            return insertCount.Result;
        }
    }
}
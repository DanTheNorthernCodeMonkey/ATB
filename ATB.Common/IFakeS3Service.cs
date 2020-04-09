using System;
using System.Threading.Tasks;

namespace ATB.Common
{
    public interface IFakeS3Service
    {
        Task Upload(Guid id, string base64Image);
        string ImageUri(Guid id);
    }
}
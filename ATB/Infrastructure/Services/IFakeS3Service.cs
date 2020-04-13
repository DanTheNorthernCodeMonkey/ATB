using System;
using System.Threading.Tasks;

namespace ATB.Infrastructure.Services
{
	public interface IFakeS3Service
	{
		Task<ExecutionStatus> Upload(Guid id, string base64Image);
		string ImageUri(Guid id);
	}
}
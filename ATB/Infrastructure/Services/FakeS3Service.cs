using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ATB.Infrastructure.Services
{
    public class FakeS3Service : IFakeS3Service
    {
        private const string Png = ".png";
        private const string NixDelimiter = "/";
        private const string WinDelimiter = "\\";

        private static string Project => Environment.CurrentDirectory;

        public async Task<ExecutionStatus> Upload(Guid id, string base64Image)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64Image);

                var path = GetPathToFrontOffice(id);

                await File.WriteAllBytesAsync(path, bytes);

                return ExecutionStatus.Success;
            }
            catch (Exception ex)
            {
                return ExecutionStatus.Fail;
            }
        }

        public string ImageUri(Guid id)
        {
	        return string.Concat("images/", id, Png);
        }

        private static string GetPathToFrontOffice(Guid id)
        {
            var delimiter = WinDelimiter;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                delimiter = WinDelimiter;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                delimiter = NixDelimiter;

            var frontOffice = string.Concat(delimiter, "FrontOffice", delimiter, "client", delimiter, "atb_fo", delimiter, "public", delimiter, "images", delimiter, id, Png);
            
            return string.Concat(Project, frontOffice);
        }
    }
}
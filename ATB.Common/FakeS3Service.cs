using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ATB.Common
{
    public class FakeS3Service : IFakeS3Service
    {
        private const string Png = ".png";
        private const string NixDelimiter = "/";
        private const string WinDelimiter = "\\";

        private static string project => Environment.CurrentDirectory;

        public async Task Upload(Guid id, string base64Image)
        {
            var bytes = Convert.FromBase64String(base64Image);

            var path = GetPathToFrontOffice(id);
                
            await File.WriteAllBytesAsync(path, bytes);
        }

        private string GetPathToFrontOffice(Guid id)
        {
            var delimiter = WinDelimiter;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                delimiter = WinDelimiter;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                delimiter = NixDelimiter;

            var frontOffice = string.Concat("ATB.FrontOffice", delimiter, "client", delimiter, "atb_fo", delimiter, "public", delimiter, "images", delimiter, id, Png);
            
            return project.Replace("ATB.BackOffice", frontOffice);
        }

        public string ImageUri(Guid id)
        {
            return string.Concat("images/", id, Png);
        }
    }
}
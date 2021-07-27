using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ShopBridgeAPI.Common
{
    public static class DocumentManager
    {
        public static async Task Save(Document document)
        {
            if (!Directory.Exists(document.DirectoryPath))
            {
                Directory.CreateDirectory(document.DirectoryPath);
            }

            if (File.Exists(Path.Combine(document.DirectoryPath, document.Name)))
            {
                string filePath = Path.Combine(document.DirectoryPath, document.Name);
                System.IO.File.Delete(filePath);
            }

            using (var fileStream = new FileStream(document.FileFullPath, FileMode.Create))
            {
                await document.Content.CopyToAsync(fileStream);
            }
        }
    }

    public class Document
    {
        public string Name { get; set; }

        public IFormFile Content { get; set; }

        public string DirectoryPath { get; set; }

        public string FileFullPath => Path.Combine(DirectoryPath, Name);
    }
}
using Microsoft.AspNetCore.Http;
using MimeTypes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PetClinicApp.Source.Shared.Uploads
{
    public static class UploadFile
    {
        public static async Task<string> Upload(string folder, IFormFile file, string oldFileName = "")
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\files\\" + folder + "\\";

            if (!string.IsNullOrEmpty(oldFileName))
            {
                oldFileName = path + oldFileName;
                if (File.Exists(oldFileName))
                {
                    File.Delete(oldFileName);
                }
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            var extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var newFileName = Path.GetRandomFileName();
            newFileName = newFileName + extension;

            var fullPath = path + newFileName;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                await File.WriteAllBytesAsync(fullPath, ms.GetBuffer());
            }

            return newFileName;
        }

        public static async Task<FileModel> Download(string folder, string fileName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\files\\" + folder + "\\";
            var fullPath = path + fileName;

            if (File.Exists(fullPath))
            {
                var extension = fileName.Substring(fileName.LastIndexOf('.'));
                extension = extension.Remove(0, 1);
                var mimeType = MimeTypeMap.GetMimeType(extension);

                var bytes = await File.ReadAllBytesAsync(fullPath);
                return new FileModel(bytes, mimeType);
            }
            return null;
        }
    }
}

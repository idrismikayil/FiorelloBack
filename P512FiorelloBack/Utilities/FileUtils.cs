using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace P512FiorelloBack.Utilities
{
    public class FileUtils
    {
        public static string Create(string FolderPath, IFormFile file) 
        {
            var fileName = Guid.NewGuid() + file.FileName;
            var fullPath = Path.Combine(FolderPath, fileName);
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return fileName;
        }

        public static void Delete(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

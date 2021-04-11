using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        private static string newPath(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExtension = fileInfo.Extension;
            var newPath = Guid.NewGuid().ToString() + fileExtension;
            string result = $"{newPath}";
            return result;
        }
        public static string Add(IFormFile file)
        {
            var result = newPath(file);
            var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\", result);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            //File.Move(sourcePath);
            return result;
        }
        public static void Delete(string path)
        {
            File.Delete(path);
        }
        public static string Update(string sourcePath, IFormFile file)
        {
            var result = newPath(file);
            if (sourcePath.Length > 0)
            {
                using (var stream = new FileStream(result, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            File.Delete(sourcePath);
            return result;
        }


    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using SYS.Invoice.BLL.Infrastructure;
using SYS.Invoice.BLL.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.HelperServices
{
    public class FileOperationsService : IFileOperationsService
    {
        private readonly string _directoryPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesService"/>
        /// </summary>
        /// <param name="hostEnvironment"></param>
        /// <param name="configuration"></param>
        public FileOperationsService(IHostEnvironment hostEnvironment)
        {
            _directoryPath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "Files");

            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);
        }
        public async Task<FileDetailModel> Upload(IFormFile file)
        {
            var name = GenerateFileName(file.FileName);
            
            string path = Path.Combine(_directoryPath, name);

            await using ( var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string text = ReadFile(path);
            
            return new FileDetailModel()
            {
                Name = name,
                Text = text
            };
        }

        private string GenerateFileName(string fileName)
        {
            var dotLastIndex = fileName.LastIndexOf('.');
            string ext = Path.GetExtension(fileName);
            return $"{fileName.Remove(dotLastIndex).Replace(' ', '_')}_{Guid.NewGuid().ToString().Substring(0, 6)}{ext}";
        }

        private string ReadFile(string filePath)
        {
            StringBuilder fileTextSb = new StringBuilder();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    fileTextSb.Append(line);
                }
            }

            return fileTextSb.ToString();
        }
    }
}
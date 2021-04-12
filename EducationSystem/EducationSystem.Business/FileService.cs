using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Business
{
    public class FileService : IFileService
    {
        public FileStream GetFile(string path)
        {           
            FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), path), FileMode.Open);          
            return fileStream;            
        }

        public async Task<string> WriteFile(IFormFile file)
        {                     
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            string fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine("Upload\\files", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }      
            
            return path;
        }
        public bool CheckFile(string path) => File.Exists(path);    
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Business
{
    public interface IFileService
    {
        Task<string> WriteFile(IFormFile file);
        FileStream GetFile(string path);
    }
}

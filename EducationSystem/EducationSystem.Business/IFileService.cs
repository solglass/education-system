using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Business
{
    public interface IFileService
    {
        Task<string> WriteFile(IFormFile file);
    }
}

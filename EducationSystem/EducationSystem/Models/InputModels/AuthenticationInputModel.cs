using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class AuthenticationInputModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

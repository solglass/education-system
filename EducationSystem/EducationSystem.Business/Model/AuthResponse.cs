using EducationSystem.Core.Enums;
using System.Collections.Generic;

namespace EducationSystem.Business.Model
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<Role> Roles { get; set; }
    }
}

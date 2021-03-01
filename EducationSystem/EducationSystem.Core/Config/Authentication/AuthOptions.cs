using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationSystem.Core.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "EducationSystem.Api"; 
        public const string AUDIENCE = "DevEducation"; 
        const string KEY = "djwaoijdajwdiawjdiawjdwaidjawodjawdjawodjoa*%#$$@efwsefw"; 
        public const int LIFETIME = 2880; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

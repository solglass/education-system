using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationSystem.Core.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "EducationSystem.Api"; // издатель токена
        public const string AUDIENCE = "DevEducation"; // потребитель токена
        const string KEY = "djwaoijdajwdiawjdiawjdwaidjawodjawdjawodjoa*%#$$@efwsefw";   // ключ для шифрации
        public const int LIFETIME = 1440; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

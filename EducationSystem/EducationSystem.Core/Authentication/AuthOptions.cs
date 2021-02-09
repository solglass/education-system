using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationSystem.Core.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "EducationSystem.Api"; // издатель токена
        public const string AUDIENCE = "DevEducation"; // потребитель токена
        const string KEY = "djwaoijdajwdiawjdiawjdwaidjawodjawdjawodjoa*%#$$@efwsefw";   // ключ для шифрации
        public const int LIFETIME = 2880; // время жизни токена - 2 days
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

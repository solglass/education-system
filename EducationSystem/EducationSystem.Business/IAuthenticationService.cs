using EducationSystem.Business.Model;
using EducationSystem.Data.Models;

namespace EducationSystem.Business
{
    public interface IAuthenticationService
    {
        AuthResponse GenerateToken(UserDto user);
        UserDto GetAuthentificatedUser(string login);
    }
}
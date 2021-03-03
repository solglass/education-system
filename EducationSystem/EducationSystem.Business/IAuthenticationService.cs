using EducationSystem.Data.Models;

namespace EducationSystem.Business
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserDto user);
        UserDto GetAuthentificatedUser(string login);
    }
}
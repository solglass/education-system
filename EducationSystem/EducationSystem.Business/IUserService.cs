using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IUserService
    {
        int AddRole(RoleDto roleDto);
        int AddUser(UserDto userDto);
        int ChangePassword(int id, string oldPassword, string password);
        int DeleteRole(int id);
        int DeleteUser(int id);
        List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId);
        RoleDto GetRole(int id);
        List<RoleDto> GetRoles();
        UserDto GetUserById(int id);

        int DeleteOrRecoverUser(int id, bool isDeleted);
        List<UserDto> GetUsers();
        int UpdateRole(RoleDto roleDto);
        int UpdateUser(UserDto userDto);
    }
}
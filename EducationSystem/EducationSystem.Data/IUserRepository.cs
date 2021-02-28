using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IUserRepository
    {
        int AddRole(RoleDto role);
        int AddRoleToUser(UserRoleDto userRole);
        int AddUser(UserDto user);
        int ChangeUserPassword(int id, string oldPassword, string newPassword);
        UserDto CheckUser(string login);
        int DeleteRole(int id);
        int DeleteRoleToUser(int id);
        List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId);
        RoleDto GetRoleById(int id);
        List<RoleDto> GetRoles();
        UserDto GetUserById(int id);
        List<UserDto> GetUsers();
        int HardDeleteUser(int id);
        int UpdateRole(RoleDto role);
        int UpdateUser(UserDto user);
        int DeleteOrRecoverUser(int id, bool isDeleted);
    }
}
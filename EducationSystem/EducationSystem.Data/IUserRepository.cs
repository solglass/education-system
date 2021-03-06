using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System.Collections.Generic;


namespace EducationSystem.Data
{
    public interface IUserRepository
    {
        int AddRoleToUser(int userId, int roleId);
        int AddUser(UserDto user);
        int ChangeUserPassword(int id, string oldPassword, string newPassword);
        UserDto CheckUser(string login);
        int DeleteRoleFromUser(int userId, int roleId);
        List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId);
        UserDto GetUserById(int id);
        List<UserDto> GetUsers();
        int HardDeleteUser(int id);
        int UpdateUser(UserDto user);
        int DeleteOrRecoverUser(int id, bool isDeleted);
        public List<UserDto> GetStudentsByGroupId(int groupId);
    }
}
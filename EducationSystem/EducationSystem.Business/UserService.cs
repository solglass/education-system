using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public List<UserDto> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        //public List<UserDto> PassedStudentsAttempt_SelectByGroupId(int groupId)
        //{
        //    return _repo.PassedStudentsAttempt_SelectByGroupId(groupId);
        //}
        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            return _userRepository.GetPassedStudentsAttempt_SelectByGroupId(groupId);
        }
        public int UpdateUser (UserDto userDto) 
        { 
            return _userRepository.UpdateUser(userDto); 
        }
        public int AddUser(UserDto userDto) 
        {
            userDto.Password = new SecurityService().GetHash(userDto.Password);
            return _userRepository.AddUser(userDto); 
        }

        public int DeleteUser(int id)
        {
            bool isDeleted = true;
            return _userRepository.DeleteOrRecoverUser(id, isDeleted);
        }

        public int RecoverUser(int id)
        {
            bool isDeleted = false;
            return _userRepository.DeleteOrRecoverUser(id, isDeleted);
        }

        public int HardDeleteUser(int id)
        {
            return _userRepository.HardDeleteUser(id);
        }

        public int ChangePassword(int id,string oldPassword, string password)
        {
            oldPassword = new SecurityService().GetHash(oldPassword);
            password = new SecurityService().GetHash(password);
            return _userRepository.ChangeUserPassword(id, oldPassword, password);
        }

        public int AddRole(RoleDto roleDto)
        {
            return _userRepository.AddRole(roleDto);
        }
        public int UpdateRole(RoleDto roleDto)
        {
            return _userRepository.UpdateRole(roleDto);
        }
        public int DeleteRole(int id)
        {
            return _userRepository.DeleteRole(id);
        }
        public RoleDto GetRole(int id)
        {
            return _userRepository.GetRoleById(id);
        }
        public List<RoleDto> GetRoles()
        {
            return _userRepository.GetRoles();
        }
    }
}

using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class UserService
    {
        private UserRepository _repo;
        public UserService()
        {
            _repo = new UserRepository();
        }
        public List<UserDto> GetUsers()
        {
            return _repo.GetUsers();
        }
        public List<UserDto> PassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            return _repo.PassedStudentsAttempt_SelectByGroupId(groupId);
        }
        public UserDto GetUserById(int id)
        {
            return _repo.GetUserById(id);
        }
    }
}

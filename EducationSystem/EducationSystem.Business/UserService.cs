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
        public UserDto GetUserById(int id)
        {
            return _repo.GetUserById(id);
        }
    }
}

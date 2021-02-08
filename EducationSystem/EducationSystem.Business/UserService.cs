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
        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public int UpdateUser (UserDto userDto) 
        { 
            return _userRepository.UpdateUser(userDto); 
        }
        public int AddUser(UserDto userDto) 
        { 
            return _userRepository.AddUser(userDto); 
        }
        public int DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }
        //public string ChangePassword(string login, string password)
        //{
        //    return _userRepository.CheckUser ();
        //}
        // TODO: Role 
    }
}

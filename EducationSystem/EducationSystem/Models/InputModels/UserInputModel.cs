using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;


namespace EducationSystem.API.Models.InputModels
{
    public class UserInputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string UserPic { get; set; }
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }
    }
}

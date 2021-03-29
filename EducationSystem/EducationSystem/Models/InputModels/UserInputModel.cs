using EducationSystem.API.Attributes;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class UserInputModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserPic { get; set; }
        [Required]
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }
    }
}

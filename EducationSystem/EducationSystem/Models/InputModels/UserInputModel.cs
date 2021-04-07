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
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string BirthDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(1000)]
        public string UserPic { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Email { get; set; }
        public List<int> RoleIds { get; set; }
    }
}

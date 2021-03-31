using EducationSystem.API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class UpdateUserInputModel
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
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(1000)]
        public string UserPic { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60)]
        public string Email { get; set; }       
    }
}

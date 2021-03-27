using EducationSystem.API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class NotificationInputModel
    {
        [Required]        
        public string Message { get; set; }
        [Required]
        [CustomDateTimeValidation]
        public string Date { get; set; }
    }
}

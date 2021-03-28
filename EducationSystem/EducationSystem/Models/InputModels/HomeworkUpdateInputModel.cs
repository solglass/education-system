using EducationSystem.API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkUpdateInputModel
    {
        [Required]
        public string Description { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string StartDate { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string DeadlineDate { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> ThemeIds { get; set; }
        [Required]
        public bool IsOptional { get; set; }
    }
}

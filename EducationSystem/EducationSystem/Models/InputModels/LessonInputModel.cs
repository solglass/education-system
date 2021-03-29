using EducationSystem.API.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class LessonInputModel
    {

        [Required]
        [Range(1,int.MaxValue)]
        public int GroupId { get; set; }  

        [Required]
        public string Comment { get; set; }
        [Required]
        [CustomDateTimeValidation]
        public string LessonDate { get; set; }

        public List<int> ThemesId { get; set; }
    }
}

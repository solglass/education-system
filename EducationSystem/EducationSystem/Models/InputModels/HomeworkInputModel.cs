using EducationSystem.API.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkInputModel
    {
        [Required]
        public string Description { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string StartDate { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string DeadlineDate { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int CourseId { get; set; }
        public List<int> GroupIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> ThemeIds { get; set; }
        [Required]
        public bool IsOptional { get; set; }
    }
}

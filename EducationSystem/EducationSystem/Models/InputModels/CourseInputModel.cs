
using EducationSystem.API.Models.InputModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models
{
    public class CourseInputModel
    {
        [Required(ErrorMessage ="Name is empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Duration is empty")]
        public int Duration { get; set; }
        public List<int> ThemeIds { get; set; }
        public List<int> MaterialIds { get; set; }
    }
}
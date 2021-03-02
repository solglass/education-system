
using EducationSystem.API.Models.InputModels;
using System.Collections.Generic;

namespace EducationSystem.API.Models
{
    public class CourseInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public List<int> ThemeIds { get; set; }
    }
}
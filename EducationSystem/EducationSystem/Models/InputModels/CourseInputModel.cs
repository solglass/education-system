
using EducationSystem.API.Models.InputModels;
using System.Collections.Generic;

namespace EducationSystem.API.Models
{
    public class CourseInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeInputModel> Themes { get; set; }
        public List<GroupInputModel> Groups { get; set; }

    }
}
using System;
using System.Collections.Generic;
using EducationSystem.Data.Models;

namespace EducationSystem.Data.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeDto> Themes { get; set; }
    }
}

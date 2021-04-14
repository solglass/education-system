using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    class CourseThemeDto
    {
        public int Id { get; set; }
        public CourseDto  Course{get;set;}
        public ThemeDto Theme { get; set; }
        public int Order { get; set; }
    }
}

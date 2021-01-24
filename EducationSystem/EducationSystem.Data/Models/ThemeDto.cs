using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class ThemeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<HomeworkDto> Homeworks { get; set; }
        public List<LessonDto> Lessons { get; set; }
        public List<CourseDto> Courses { get; set; }
    }
}

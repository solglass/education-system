using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class ThemeInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public List<TagDto> Tags { get; set; }
       // public List<HomeworkDto> Homeworks { get; set; }
       // public List<LessonDto> Lessons { get; set; }
        public List<CourseInputModel> Courses { get; set; }
    }
}

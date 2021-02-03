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
        public List<TagInputModel> Tags { get; set; }
        public List<HomeworkInputModel> Homeworks { get; set; }
        public List<LessonInputModel> Lessons { get; set; }
        public List<CommentInputModel> Courses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class FeedbackSearchInputModel
    {
        public int? LessonId { get; set; }
        public int? GroupId { get; set; }
        public int? CourseId { get; set; }

    }
}

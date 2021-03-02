using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class FeedbackSearchInputModel
    {
        public int? LessonID { get; set; }
        public int? GroupID { get; set; }
        public int? CourseID { get; set; }

    }
}

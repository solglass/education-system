using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class FeedbackInputModel
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public int LessonId { get; set; }
        public int UnderstandingLevelId { get; set; }
        
    }
}

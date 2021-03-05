using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class FeedbackInputModel
    {
        public int UserID { get; set; }
        public string Message { get; set; }
        public int LessonID { get; set; }
        public int UnderstandingLevelID { get; set; }
        
    }
}

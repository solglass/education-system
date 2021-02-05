using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class FeedbackOutputModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Messege { get; set; }
        public int LessonID { get; set; }
        public int UnderstandingLevelID { get; set; }
        public LessonOutputModel Lesson { get; set; }
        public UserOutputModel User { get; set; }
        public UnderstandingLevelOutputModel UnderstandingLevel { get; set; }
    }
}

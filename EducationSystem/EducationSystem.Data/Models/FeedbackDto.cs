using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class FeedbackDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Messege { get; set; }
        public int LessonID { get; set; }
        public int UnderstandingLevelID { get; set; }
        public LessonDto Lesson { get; set; }
        public UserDto User { get; set; }
        public UnderstandingLevelDto UnderstandingLevel { get; set; }
        
    }
}

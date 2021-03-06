using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public LessonDto Lesson { get; set; }
        public UserDto User { get; set; }
        public UnderstandingLevel UnderstandingLevel { get; set; }
        
    }
}

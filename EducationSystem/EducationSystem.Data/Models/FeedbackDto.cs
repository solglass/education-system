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
        
        public object Clone()
        {
            return new FeedbackDto
            {
                Id = this.Id,
                Message = this.Message,
                Lesson = this.Lesson,
                User = this.User,
                UnderstandingLevel = this.UnderstandingLevel
            };
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is FeedbackDto))
                return false;

            FeedbackDto feedbackObj = (FeedbackDto)obj;
            if (Id != feedbackObj.Id ||
                Message != feedbackObj.Message ||
                Lesson.Equals(feedbackObj.Lesson) ||
                User.Equals(feedbackObj.User) ||
                UnderstandingLevel != feedbackObj.UnderstandingLevel)
            {
                return false;
            }

            return true;
        }
    }
}

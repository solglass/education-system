using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class CommentDto : ICloneable
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public UserDto Author { get; set; }
        public HomeworkAttemptDto HomeworkAttempt { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public bool IsDeleted { get; set; }
        public object Clone()
        {
            return new CommentDto
            {
                Author = Author != null ? (UserDto)Author.Clone() : null,
                HomeworkAttempt = HomeworkAttempt != null ? (HomeworkAttemptDto)HomeworkAttempt.Clone() : null,
                Message = Message,
                IsDeleted = IsDeleted
            };
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is CommentDto))
                return false;

            CommentDto commentObj = (CommentDto)obj;
            if (Id != commentObj.Id ||
                Message != commentObj.Message ||
                IsDeleted != commentObj.IsDeleted)
            {
                return false;
            }
            return true;
        }
    }
    
}

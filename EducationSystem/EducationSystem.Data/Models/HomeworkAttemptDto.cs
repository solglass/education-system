using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace EducationSystem.Data.Models
{
    public class HomeworkAttemptDto : ICloneable
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public UserDto Author { get; set; }
        public HomeworkDto Homework { get; set; }
        public HomeworkAttemptStatus HomeworkAttemptStatus { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public List<CommentDto> Comments { get; set; }

        public object Clone()
        {
            return new HomeworkAttemptDto
            {
                Id = Id,
                Comment = Comment,
                IsDeleted = IsDeleted,
                Author = Author != null ? (UserDto)Author.Clone() : null,
                Homework = Homework != null ? (HomeworkDto)Homework.Clone() : null,
                HomeworkAttemptStatus = HomeworkAttemptStatus,
                Attachments = Attachments,
                Comments = Comments
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is HomeworkAttemptDto))
                return false;

            HomeworkAttemptDto homeworkAttemptObject = (HomeworkAttemptDto)obj;
            return (Id == homeworkAttemptObject.Id &&
                Comment == homeworkAttemptObject.Comment &&
                HomeworkAttemptStatus == homeworkAttemptObject.HomeworkAttemptStatus &&
                IsDeleted == homeworkAttemptObject.IsDeleted);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

            s +=Id + " " + Comment + " " + HomeworkAttemptStatus + " " + IsDeleted + "; ";
            return s;
        }
    }
}
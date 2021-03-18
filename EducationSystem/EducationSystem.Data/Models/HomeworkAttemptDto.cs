using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
                Id = this.Id,
                Comment = this.Comment,
                IsDeleted = this.IsDeleted,
                Author = this.Author,
                Homework = this.Homework,
                HomeworkAttemptStatus = this.HomeworkAttemptStatus,
                Attachments = this.Attachments,
                Comments = this.Comments
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is HomeworkAttemptDto))
                return false;

            HomeworkAttemptDto homeworkAttemptObject = (HomeworkAttemptDto)obj;
            if (Id != homeworkAttemptObject.Id ||
                Comment != homeworkAttemptObject.Comment ||
                HomeworkAttemptStatus != homeworkAttemptObject.HomeworkAttemptStatus ||
                IsDeleted != homeworkAttemptObject.IsDeleted)
            {
                return false;
            }

            else return true;
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
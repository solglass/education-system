using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkAttemptDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public UserDto Author { get; set; }
        public HomeworkDto Homework { get; set; }
        public HomeworkAttemptStatusDto HomeworkAttemptStatus { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public List<CommentDto> Comments { get; set; }



        public override bool Equals(object obj)
        {
            HomeworkAttemptDto attemptObj = (HomeworkAttemptDto)obj;
            if (!attemptObj.Comment.Equals(Comment) ||  attemptObj.IsDeleted != IsDeleted)
            {
                return false;
            }
            if (attemptObj.Author == null || Author == null || !attemptObj.Author.Equals(Author))
            {
                return false;
            }
            if (attemptObj.Homework == null || Homework == null || !attemptObj.Homework.Equals(Homework))
            {
                return false;
            }
            if (attemptObj.HomeworkAttemptStatus == null || HomeworkAttemptStatus == null 
                || !attemptObj.HomeworkAttemptStatus.Equals(HomeworkAttemptStatus))
            {
                return false;
            }
            if (attemptObj.Attachments.Count != Attachments.Count)
            {
                return false;
            }
            for (int i = 0; i < Attachments.Count; i++)
            {
                if (!attemptObj.Attachments[i].Path.Equals(Attachments[i].Path))
                {
                    return false;
                }
            }
            if (attemptObj.Comments.Count != Comments.Count)
            {
                return false;
            }
            for (int i = 0; i < Comments.Count; i++)
            {
                if (!attemptObj.Comments[i].Message.Equals(Comments[i].Message))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
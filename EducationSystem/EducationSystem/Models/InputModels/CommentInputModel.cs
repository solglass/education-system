using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class CommentInputModel
    {
        public int Id { get; set; }
        public int HomeworkAttemptId { get; set; }
        public string Message { get; set; }
        public UserInputModel Author { get; set; }
        public HomeworkInputModel HomeworkAttempt { get; set; }
        public List<AttachmentInputModel> Attachments { get; set; }
        public bool IsDeleted { get; set; }
    }
}

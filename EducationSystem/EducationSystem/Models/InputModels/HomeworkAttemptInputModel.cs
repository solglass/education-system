using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkAttemptInputModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public bool IsDeleted { get; set; }
        public UserInputModel Author { get; set; }
        public HomeworkInputModel Homework { get; set; }
        public HomeworkAttemptStatusInputModel HomeworkAttemptStatus { get; set; }
        public List<AttachmentInputModel> Attachments { get; set; } // TODO: Check necessity
        public List<CommentInputModel> Comments { get; set; }
    }
}

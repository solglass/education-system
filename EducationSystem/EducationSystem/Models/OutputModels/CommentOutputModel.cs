using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class CommentOutputModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public HomeworkAttemptOutputModel homeworkAttempt { get; set; }
        public UserOutputModel Author { get; set; }
        public List<AttachmentOutputModel> Attachments { get; set; }
    }
}

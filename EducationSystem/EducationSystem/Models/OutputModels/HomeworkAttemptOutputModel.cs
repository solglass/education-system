using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class HomeworkAttemptOutputModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public bool IsDeleted { get; set; }
        public UserOutputModel Author { get; set; }
        public HomeworkAttemptStatusOutputModel HomeworkAttemptStatus { get; set; }
        public List<CommentOutputModel> Comments { get; set; }
        public List<AttachmentOutputModel> Attachments { get; set; }
    }
}

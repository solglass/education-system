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
        
    }
}

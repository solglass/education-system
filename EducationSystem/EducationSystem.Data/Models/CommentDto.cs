using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public UserDto Author { get; set; }
        public HomeworkAttemptDto HomeworkAttempt { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public bool IsDeleted { get; set; }        
    }
}

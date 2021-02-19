using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class CommentAttemptDto
    {
        public int Id { get; set; }
        public int HomeworkAttemptId { get; set; }
        public string Message { get; set; }
        public UserDto Author { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
    }
}

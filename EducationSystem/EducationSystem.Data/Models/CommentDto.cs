using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int HomeworkAttemptId { get; set; }
        public string Message { get; set; }
        public bool IsDeleted { get; set; }
    }
}

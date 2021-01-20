using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkAttempt_AttachmentDto
    {
        public int Id { get; set; }
        public int HomeworkAttemptId { get; set; }
        public int AttachmentId { get; set; }

    }
}

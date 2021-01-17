using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkAttemptDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int UserID { get; set; }
        public int HomeworkAttemptId { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

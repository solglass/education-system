using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int GroupID { get; set; }
        public bool IsOptional { get; set; }
        public bool IsDeleted { get; set; }
        public List<Homework_ThemeDto> Homework_Theme { get; set; }
        public List<HomeworkAttemptStatusDto> HomeworkAttemptStatus { get; set; }
    }
}

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
        public GroupDto Group { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<ThemeDto> Themes { get; set; }
        public List<HomeworkAttemptDto> HomeworkAttempts{ get; set; }
        public bool IsOptional { get; set; }
        public bool IsDeleted { get; set; }
    }
}

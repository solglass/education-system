using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class GroupDto
    {
        public int Id { get; set; }
        public int CourseID { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public CourseDto CourseDto { get; set; }
        public GroupStatusDto GroupStatusDto { get; set; }
    }
}

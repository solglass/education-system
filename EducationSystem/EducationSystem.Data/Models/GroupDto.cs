using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class GroupDto
    {
        public int Id { get; set; }     
        public DateTime StartDate { get; set; }
        public CourseDto Course { get; set; }
        public GroupStatusDto GroupStatus { get; set; }
    }
}

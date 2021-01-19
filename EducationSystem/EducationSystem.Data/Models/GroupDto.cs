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
        public string StartDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public class AttendanceDto
    {
        public int Id { get; set; }
        public int LessonID { get; set; }
        public int UserID { get; set; }
        public bool IsAbsent { get; set; }

        public UserDto User { get; set; }
    }
}

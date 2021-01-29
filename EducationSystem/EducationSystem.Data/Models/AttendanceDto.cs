using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public class AttendanceDto
    {
        public int ID { get; set; }
        public int LessonID { get; set; }
        public int UserID { get; set; }
        public byte IsAbsent { get; set; }

        public UserDto User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public class AttendanceDto
    {
        public int Id { get; set; }
        public LessonDto Lesson { get; set; }
        public UserDto User { get; set; }
        public bool IsAbsent { get; set; }
        public string AbsenceReason { get; set; }  // ToDo: add to DB
    }
}

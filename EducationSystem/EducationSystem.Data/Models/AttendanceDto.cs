using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public class AttendanceDto : ICloneable
    {
        public int Id { get; set; }
        public LessonDto Lesson { get; set; }
        public UserDto User { get; set; }
        public bool IsAbsent { get; set; }
        public string ReasonOfAbsence { get; set; }

        public object Clone()
        {
            return new AttendanceDto
            {
                Id = Id,
                Lesson = Lesson,
                User = User,
                IsAbsent = IsAbsent,
                ReasonOfAbsence = ReasonOfAbsence
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is AttendanceDto))
                return false;

            AttendanceDto attendanceObj = (AttendanceDto)obj;
            if (Id != attendanceObj.Id 
                || IsAbsent != attendanceObj.IsAbsent 
                || ReasonOfAbsence != attendanceObj.ReasonOfAbsence)
            {
                return false;
            }
            return true;
        }
    }
}

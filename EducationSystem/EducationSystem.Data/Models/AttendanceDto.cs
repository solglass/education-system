using System;

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
            var dto = new AttendanceDto
            {
                Lesson = Lesson != null ? (LessonDto)Lesson.Clone() : null,
                User = User != null ? (UserDto)User.Clone() : null,
                IsAbsent = IsAbsent,
                ReasonOfAbsence = ReasonOfAbsence
            };
            if (Lesson != null)
            {
                dto.Lesson = (LessonDto)Lesson.Clone();
            }
            if (User != null)
            {
                dto.User = (UserDto)User.Clone();
            }
            return dto;
            
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AttendanceDto))
                return false;

            AttendanceDto attendanceObj = (AttendanceDto)obj;
            return (Id == attendanceObj.Id
                && IsAbsent == attendanceObj.IsAbsent
                && ReasonOfAbsence == attendanceObj.ReasonOfAbsence);
        }
    }
}

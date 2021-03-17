using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public class AttendanceMock
    {
        public int LessonId { get; set; }
        public int AttendanceId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int CourseId { get; set; }

        public AttendanceMock(int option)
        {
            switch (option)
            {
                case 1://simple

                    LessonId = 1;
                    AttendanceId = 1;
                    UserId = 1;
                    GroupId = 1;
                    CourseId = 1;
                    break;
                case 2:
                    LessonId = 1;
                    AttendanceId = 2;
                    UserId = 2;
                    GroupId = 1;
                    CourseId = 1;
                    break;
                case 3:
                    LessonId = 2;
                    AttendanceId = 2;
                    UserId = 2;
                    GroupId = 1;
                    CourseId = 1;
                    break;
                case 4:
                    LessonId = 2;
                    AttendanceId = 1;
                    UserId = 1;
                    GroupId = 1;
                    CourseId = 1;
                    break;
                default:
                    LessonId = 1;
                    AttendanceId = 1;
                    UserId = 1;
                    GroupId = 1;
                    CourseId = 1;
                    break;
            }
        }
    }
}

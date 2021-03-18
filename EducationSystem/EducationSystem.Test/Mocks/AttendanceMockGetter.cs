using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class AttendanceMockGetter
    {
        public static AttendanceDto GetAttendance(int caseId)
        {
            switch (caseId)
            {
                case 0:
                    return new AttendanceDto();
                    break;
                case 1:
                    return new AttendanceDto
                    {
                        IsAbsent = true,
                        ReasonOfAbsence = null
                    };
                    break;
                case 2:
                    return new AttendanceDto
                    {
                        IsAbsent = false,
                        ReasonOfAbsence = "Important"
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

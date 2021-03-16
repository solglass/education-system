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
                case 1:
                    return new AttendanceDto { };
                    break;
                default:
                    return null;
            }
        }
    }
}

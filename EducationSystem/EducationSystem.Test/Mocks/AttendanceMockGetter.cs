using EducationSystem.Data.Models;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class AttendanceMockGetter
    {
        public static AttendanceDto GetAttendance(int caseId)
        {
            return caseId switch
            {
                0 => new AttendanceDto(),
                1 => new AttendanceDto
                {
                    IsAbsent = true,
                    ReasonOfAbsence = null
                },
                2 => new AttendanceDto
                {
                    IsAbsent = false,
                    ReasonOfAbsence = "Important"
                },
                _ => null,
            };
        }
    }
}

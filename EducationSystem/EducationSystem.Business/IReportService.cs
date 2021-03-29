using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IReportService
    {
        List<GroupReportDto> GetAttachments();
        List<UserDto> GetStudentsByIsPaidInPeriod(string period);
    }
}
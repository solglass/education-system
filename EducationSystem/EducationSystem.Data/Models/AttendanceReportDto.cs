using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttendanceReportDto : UserDto
    {
        public decimal PercentOfSkipLessons { get; set; }
    }
}

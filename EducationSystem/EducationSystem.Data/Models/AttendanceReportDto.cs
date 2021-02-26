using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttendanceReportDto : AttendanceDto
    {
        public decimal PercentOfSkipLessons { get; set; }
    }
}

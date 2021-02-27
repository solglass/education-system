using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class AttendanceReportOutputModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string UserPic { get; set; }
        public decimal PercentOfSkipLessons { get; set; }
    }
}

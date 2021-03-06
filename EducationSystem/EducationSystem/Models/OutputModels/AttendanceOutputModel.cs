using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class AttendanceOutputModel
    {
        public int Id { get; set; }
        public AuthorOutputModel User { get; set; }
        public bool IsAbsent { get; set; }
        public string ReasonOfAbsence { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class AttendanceInputModel
    {
        public int UserId { get; set; }
        public bool IsAbsent { get; set; }
        public string ReasonOfAbsence { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class AttendanceUpdateInputModel
    {
        public int Id { get; set; }
        public bool IsAbsent { get; set; }
    }
}

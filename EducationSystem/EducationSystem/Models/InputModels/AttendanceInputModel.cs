using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class AttendanceInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        public bool IsAbsent { get; set; }
        public string ReasonOfAbsence { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class PeriodInputModel
    {
        [Required]
        public string PeriodFrom { get; set; }
        [Required]
        public string PeriodTo { get; set; }
    }
}

using EducationSystem.API.Attributes;
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
        [CustomPeriodValidation]
        public string PeriodFrom { get; set; }
        [Required]
        [CustomPeriodValidation]
        public string PeriodTo { get; set; }
    }
}

using EducationSystem.API.Attributes;
using EducationSystem.API.Models.InputModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models

{
    public class PaymentInputModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Amount { get; set; }
        [CustomDateTimeValidation]
        [Required]
        public string Date { get; set; }
        [Required]
        [CustomPeriodValidation]
        public string Period { get; set; }
        [Required]
        public int ContractNumber { get; set; }

    }
}

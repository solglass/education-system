using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class OrderedThemeInputModel
    {
        [Required(ErrorMessage = "Id is empty")]
        [Range(1, int.MaxValue, ErrorMessage = "Id should be grater than 0")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Order is empty")]
        [Range(1, 500, ErrorMessage ="Order should be in range between 1 and 500")]
        public int Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class ThemeInputModel
    {
        [Required(ErrorMessage = "Name is empty")]
        public string Name { get; set; }
       public List<int> TagIds { get; set; }
    }
}

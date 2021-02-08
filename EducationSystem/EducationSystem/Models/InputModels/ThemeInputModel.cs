using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class ThemeInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       public List<int> TagIds { get; set; }
    }
}

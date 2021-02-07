using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class ThemeOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagOutputModel> Tags { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class CourseWithProgramOutputModel: CourseOutputModel
    {
        public List<ThemeOrderedOutputModel> Themes { get; set; }
    }
}

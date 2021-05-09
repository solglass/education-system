using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business.Model
{
   public class CourseWithProgram : CourseDto
    {
        public List<OrderedTheme> Themes { get; set; }
    }
}

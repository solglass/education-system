using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class LessonOutputModel
    {
        public int GroupID { get; set; }
        public string Comment { get; set; }
        public string LessonDate { get; set; }
        public List<ThemeOutputModel> Themes { get; set; }
    }
}

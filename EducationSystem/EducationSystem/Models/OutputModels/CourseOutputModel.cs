using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class CourseOutputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        
        //public List<ThemeOutputModel> Themes { get; set; }
        //public List<GroupOutputModel> Groups { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class LessonOutputModel
    {
        public int Id { get; set; }
        public GroupOutputModel Group { get; set; }
        public string Comment { get; set; }
        public string LessonDate { get; set; }
        public List<ThemeOutputModel> Themes { get; set; }
        public string? RecordLink { get; set; }
    }
}

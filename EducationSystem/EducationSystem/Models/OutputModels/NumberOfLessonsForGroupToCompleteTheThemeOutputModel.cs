using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class NumberOfLessonsForGroupToCompleteTheThemeOutputModel : GroupOutputModel
    {
        public int CountLessons { get; set; }
    }
}

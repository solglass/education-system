using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class LessonInputModel
    {
        public int GroupID { get; set; }
        public string Comment { get; set; }
        public string LessonDate { get; set; }
        public List<int> ThemesId { get; set; }
    }
}

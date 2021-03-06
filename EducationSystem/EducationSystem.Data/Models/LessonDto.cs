using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public  class LessonDto
    {
        public int Id { get; set; }
        public GroupDto Group { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeDto> Themes { get; set; }
    }
}

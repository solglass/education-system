using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public  class LessonDto : ICloneable
   {
        public int Id { get; set; }
        public GroupDto Group { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeDto> Themes { get; set; }

        public object Clone()
        {
            return new LessonDto
            {               
                Description = this.Description,
                Date = this.Date,
                IsDeleted = this.IsDeleted
            };
           

        }

        public override bool Equals(object obj)
        {
            LessonDto lesson = (LessonDto)obj;
            if(!Id.Equals(lesson.Id) || !Description.Equals(lesson.Description) || Date != lesson.Date || IsDeleted != lesson.IsDeleted)
            { 
                return false;
            }
            return true;
        }
    }
}

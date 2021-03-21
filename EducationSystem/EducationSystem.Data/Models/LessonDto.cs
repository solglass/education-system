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
                Id = Id,
                Group = Group,
                Description = Description,
                Date = Date,
                IsDeleted = IsDeleted,
                Themes = Themes
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is LessonDto))
                return false;

            LessonDto lessonDto = (LessonDto)obj;
            if (Id != lessonDto.Id || Description != lessonDto.Description || IsDeleted != lessonDto.IsDeleted || Date != lessonDto.Date)
            {
                return false;
            }
            return true;
        }
    }
}

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
        public string? RecordLink { get; set; }
        public object Clone()
        {
            return new LessonDto
            {          
                Group = Group,
                Description = Description,
                Date = Date,
                IsDeleted = IsDeleted,
                Themes = Themes,
                RecordLink = RecordLink
            };
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is LessonDto))
                return false;           
            LessonDto lessonDto = (LessonDto)obj;
            return (Id == lessonDto.Id && Description == lessonDto.Description && IsDeleted == lessonDto.IsDeleted && Date == lessonDto.Date && RecordLink == RecordLink);                                     
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

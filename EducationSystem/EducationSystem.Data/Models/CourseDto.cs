using System;
using System.Collections.Generic;
using EducationSystem.Data.Models;

namespace EducationSystem.Data.Models
{
    public class CourseDto : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeDto> Themes { get; set; }

        public object Clone()
        {
            return new CourseDto
            {
                Name = Name,
                Description = Description,
                Duration = Duration,
                IsDeleted = IsDeleted,
                Themes = Themes
            };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is CourseDto))
                return false;

            var courseObj = (CourseDto)obj;

            return (Id == courseObj.Id &&
                    Name.Equals(courseObj.Name) &&
                    Description.Equals(courseObj.Description) &&
                    Duration == courseObj.Duration &&
                    IsDeleted == courseObj.IsDeleted);
        }
    }
}

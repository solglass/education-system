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
                Id = Id,
                Name = Name,
                Description = Description,
                Duration = Duration,
                IsDeleted = IsDeleted,
                Themes = Themes
            };
        }

        public override bool Equals(object obj)
        {
            CourseDto courseObj = (CourseDto)obj;
            if (object.ReferenceEquals(courseObj, null) && object.ReferenceEquals(this, null))
            {
                return true;
            }
            if (object.ReferenceEquals(courseObj, null))
            {
                return false;
            }
            if (!courseObj.Description.Equals(Description) || !courseObj.Name.Equals(Name) || courseObj.Duration!=Duration)
            {
                return false;
            }

            return true;
        }
    }
}

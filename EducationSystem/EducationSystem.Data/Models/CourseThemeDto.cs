using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class CourseThemeDto : ICloneable
    {
        public int Id { get; set; }
        public CourseDto  Course{get;set;}
        public ThemeDto Theme { get; set; }
        public int Order { get; set; }

        public object Clone()
        {
            CourseDto courseClone = null;
            ThemeDto themeClone = null;

            if (Course != null)
            {
                courseClone = (CourseDto)Course.Clone();
            }

            if (Theme != null)
            {
                themeClone = (ThemeDto)Theme.Clone();
            }

            return new CourseThemeDto()
            {
                Course = courseClone,
                Theme = themeClone,
                Order = Order
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is CourseThemeDto))
                return false;

            var courseThemeDto = (CourseThemeDto)obj;

            return ((Course.Id==courseThemeDto.Course.Id) &&
               (Course.Name==courseThemeDto.Course.Name) &&
               (Theme.Id == courseThemeDto.Theme.Id) &&
               (Theme.Name == courseThemeDto.Theme.Name) &&
               (Order == courseThemeDto.Order));
        }
    }
}

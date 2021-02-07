using System;
using System.Collections.Generic;
using EducationSystem.Data.Models;

namespace EducationSystem.Data.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
        public List<ThemeDto> Themes { get; set; }


        public override bool Equals(object obj)
        {
            CourseDto courseObj = (CourseDto)obj;
            if(!courseObj.Description.Equals(Description) || !courseObj.Name.Equals(Name) || courseObj.Duration!=Duration)
            {
                return false;
            }
            if (courseObj.Themes == null || Themes == null || courseObj.Themes.Count != Themes.Count)
            {
                return false;
            }
            for (int i = 0; i < Themes.Count; i++)
            {
                if (!courseObj.Themes[i].Name.Equals(Themes[i].Name))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

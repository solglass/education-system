using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class ThemeDto : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagDto> Tags { get; set; }

        public bool IsDeleted { get; set; }

        public object Clone()
        {
            return new ThemeDto
            {           
                Name = Name,
                Tags = Tags
            };
        }

        public override bool Equals(object obj)
        {
            ThemeDto themeObj = (ThemeDto)obj;
            if (!themeObj.Name.Equals(Name))
            {
                return false;
            }
            if (themeObj.Tags == null || Tags == null || themeObj.Tags.Count != Tags.Count)
            {
                return false;
            }
            for (int i = 0; i < Tags.Count; i++)
            {
                if (!themeObj.Tags[i].Name.Equals(Tags[i].Name))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

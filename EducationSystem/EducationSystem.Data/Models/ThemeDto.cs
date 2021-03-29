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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ThemeDto))
                return false;

            var themeObj = (ThemeDto)obj;
            return  (themeObj.Name.Equals(Name) && 
                    themeObj.Id==Id && 
                    themeObj.IsDeleted==IsDeleted);
            
        }
    }
}

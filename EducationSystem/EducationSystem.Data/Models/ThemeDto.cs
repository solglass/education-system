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
                Id = Id,
                Name = Name
            };
        }

        public override bool Equals(object obj)
        {
            ThemeDto themeObj = (ThemeDto)obj;
            return themeObj.Name.Equals(Name);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class MaterialDto : ICloneable
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public List<TagDto> Tags { get; set; }

        public object Clone()
        {
            return new MaterialDto
            {
                Link = Link,
                Description = Description,
                IsDeleted = IsDeleted,
                Tags = Tags
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is MaterialDto))
                return false;

            MaterialDto materialDto = (MaterialDto)obj;
            return (
                Id == materialDto.Id && 
                Link == materialDto.Link && 
                Description == materialDto.Description && 
                IsDeleted == materialDto.IsDeleted
            );
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            string s = "";
            s += Id + " " + Link + " " + Description + " " + IsDeleted + "; ";
            return s;
        }
    }
}

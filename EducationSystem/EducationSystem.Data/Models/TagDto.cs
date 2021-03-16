
using System;

namespace EducationSystem.Data.Models
{
    public class TagDto : ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public object Clone()
        {
            return new TagDto
            {
                Id = Id,
                Name = Name
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is ThemeDto))
                return false;

            return obj is TagDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

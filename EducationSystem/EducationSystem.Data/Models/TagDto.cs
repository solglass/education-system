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
                Name = Name
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is TagDto))
                return false;
            var dto = (TagDto)obj;
            return Id == dto.Id &&
                   Name == dto.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}

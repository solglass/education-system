﻿
namespace EducationSystem.Data.Models
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
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

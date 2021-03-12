using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public override bool Equals(object obj)
        {
            MaterialDto materialDto = (MaterialDto)obj;
            if (obj == null)
            {
                return false;
            }
            if (!(obj is MaterialDto material))
                return false;
            if (Link != materialDto.Link || Description != materialDto.Description || IsDeleted !=materialDto.IsDeleted)
            {
                return false;
            }
            return true;
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

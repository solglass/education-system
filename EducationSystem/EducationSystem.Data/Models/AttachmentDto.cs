
using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttachmentDto: ICloneable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public AttachmentType AttachmentType { get; set; }

        public object Clone()
        {
            return new AttachmentDto
            {
                Description = Description,
                Path = Path,
                AttachmentType = AttachmentType
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is AttachmentDto))
                return false;
            AttachmentDto attDto = (AttachmentDto)obj;
            return 
                (attDto.Id == Id 
                && attDto.Description == Description 
                && attDto.Path == Path 
                && attDto.AttachmentType.Equals(AttachmentType));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

            s += Description + " " + Path + " "+ AttachmentType.ToString()+"; ";
            return s;
        }

    }
}

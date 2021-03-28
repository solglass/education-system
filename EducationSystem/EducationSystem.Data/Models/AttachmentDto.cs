
using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttachmentDto: ICloneable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public AttachmentType AttachmentType { get; set; }

        public override bool Equals(object obj)
        {
            AttachmentDto attDto = (AttachmentDto)obj;
            
            if (attDto.Path == Path&& attDto.AttachmentType.Equals(AttachmentType))
            {
                return true;
            }

            return false;
        }

        public object Clone()
        {
            return new AttachmentDto
            {
                Path=Path,
                AttachmentType = AttachmentType
            };
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

                s += Path + " "+ AttachmentType.ToString()+"; ";
            return s;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public AttachmentTypeDto AttachmentType { get; set; }

        public override bool Equals(object obj)
        {
            AttachmentDto attDto = (AttachmentDto)obj;

            if (attDto.Path == Path&& attDto.AttachmentType.Equals(AttachmentType))
            {
                return true;
            }

            return false;
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

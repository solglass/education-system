
using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttachmentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AttachmentType AttachmentType { get; set; }

        public override bool Equals(object obj)
        {
            AttachmentTypeDto attTypeDto = (AttachmentTypeDto)obj;

            if (attTypeDto.Name == Name && attTypeDto.AttachmentType ==
                AttachmentType) 
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

            string output = String.Format("Имя: {0} ;",Name);
            return output;
        }

    }
}


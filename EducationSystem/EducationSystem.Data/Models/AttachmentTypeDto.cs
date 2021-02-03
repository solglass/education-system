
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class AttachmentTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            AttachmentTypeDto attTypeDto = (AttachmentTypeDto)obj;

            if (attTypeDto.Name == Name )
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

            s += Name +  "; ";
            return s;
        }

    }
}


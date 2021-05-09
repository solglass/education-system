using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business.Model
{
   public class OrderedTheme : ThemeDto
    {
        public int Order { get; set; }
    }
}

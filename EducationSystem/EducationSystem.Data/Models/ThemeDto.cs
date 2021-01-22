using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class ThemeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}

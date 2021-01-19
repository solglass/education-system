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
    }
}

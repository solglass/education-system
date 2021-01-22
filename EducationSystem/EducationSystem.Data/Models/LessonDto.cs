using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
   public  class LessonDto
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string Comment { get; set; }
        public DateTime Data { get; set; }
        public byte IsDeleted { get; set; }

    }
}

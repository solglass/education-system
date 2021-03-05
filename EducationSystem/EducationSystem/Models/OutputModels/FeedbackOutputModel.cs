using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class FeedbackOutputModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public int LessonID { get; set; }
        public String UnderstandingLevel { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class FeedbackOutputModel
    {
        public int Id { get; set; }
        public  UserOutputModel User { get; set; }
        public string Message { get; set; }
        public LessonOutputModel Lesson { get; set; }
        public string UnderstandingLevel { get; set; }
       
    }
}

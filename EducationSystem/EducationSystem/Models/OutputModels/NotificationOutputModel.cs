using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class NotificationOutputModel
    {
        public int Id { get; set; }
        public AuthorOutputModel User { get; set; }
        public AuthorOutputModel Author { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public bool IsRead { get; set; }
    }
}

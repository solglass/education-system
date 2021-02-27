using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkAttemptInputModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int AuthorId { get; set; }
        public int HomeworkAttemptStatusId { get; set; }
    }
}

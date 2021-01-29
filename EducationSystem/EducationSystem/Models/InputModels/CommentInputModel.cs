using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class CommentInputModel
    {
        public int Id { get; set; }
        public int HomeworkAttemptId { get; set; }
        public string Message { get; set; }
        public int AuthorId { get; set; }
    }
}

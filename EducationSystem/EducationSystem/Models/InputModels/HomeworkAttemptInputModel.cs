using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkAttemptInputModel
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int HomeworkAttemptStatusId { get; set; }
    }
}

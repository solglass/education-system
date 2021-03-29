using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class FeedbackInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        public string Message { get; set; }
        [Required]
        public int UnderstandingLevelId { get; set; }
        
    }
}

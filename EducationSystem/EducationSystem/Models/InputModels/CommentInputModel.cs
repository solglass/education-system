using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class CommentInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int AuthorId { get; set; }
        [Required]
        public string Message { get; set; }
        public List<AttachmentInputModel> Attachments { get; set; }
    }
}

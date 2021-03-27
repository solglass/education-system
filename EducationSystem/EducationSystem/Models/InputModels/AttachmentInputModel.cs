using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models
{
    public class AttachmentInputModel
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public int AttachmentTypeId { get; set; }
    }
}
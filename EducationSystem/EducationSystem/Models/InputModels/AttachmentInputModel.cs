using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models
{
    public class AttachmentInputModel
    {
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(250)]
        public string Path { get; set; }
        [Required]
        public int AttachmentTypeId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models
{
    public class TagInputModel
    {
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Name { get; set; }
    }
}
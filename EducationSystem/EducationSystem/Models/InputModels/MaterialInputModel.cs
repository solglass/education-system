using System.ComponentModel.DataAnnotations;


namespace EducationSystem.API.Models.InputModels
{
    public class MaterialInputModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Link { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

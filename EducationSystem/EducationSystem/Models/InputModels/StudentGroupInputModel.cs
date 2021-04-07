using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class StudentGroupInputModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ContractNumber { get; set; }

    }
}
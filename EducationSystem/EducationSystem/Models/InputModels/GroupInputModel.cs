using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class GroupInputModel
    {
        public string StartDate { get; set; }
        [Required]
        public int CourseId { get; set; }
        public int GroupStatusId { get; set; }
    }
}
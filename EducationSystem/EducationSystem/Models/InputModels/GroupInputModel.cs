using System.ComponentModel.DataAnnotations;
using EducationSystem.API.Attributes;
using EducationSystem.Core.Enums;

namespace EducationSystem.API.Models.InputModels
{
    public class GroupInputModel
    {
        [Required]
        [CustomDateTimeValidation]
        public string StartDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }
        [Required]
        [Range((int)GroupStatus.Recruitment,(int)GroupStatus.Finished)]
        public int GroupStatusId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class GroupInputModel
    {
        public string StartDate { get; set; }
        [Required]
        public CourseInputModel Course { get; set; }
        [Range(1, 1000, ErrorMessage = "����� ���������� �������� ������, ����")]
        public int GroupStatusId { get; set; }
    }
}
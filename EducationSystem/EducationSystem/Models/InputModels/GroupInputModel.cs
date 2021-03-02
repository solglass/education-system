namespace EducationSystem.API.Models.InputModels
{
    public class GroupInputModel
    {
        public string StartDate { get; set; }
        public CourseInputModel Course { get; set; }
        public int GroupStatusId { get; set; }
    }
}
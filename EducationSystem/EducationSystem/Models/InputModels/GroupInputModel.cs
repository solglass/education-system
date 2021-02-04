namespace EducationSystem.API.Models.InputModels
{
    public class GroupInputModel
    {
        public int Id { get; set; }     
        public string StartDate { get; set; }
        public CourseInputModel Course { get; set; }
        public GroupStatusInputModel GroupStatus { get; set; }
    }
}
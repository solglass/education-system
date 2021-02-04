namespace EducationSystem.API.Models.OutputModels
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public CourseOutputModel Course { get; set; }
        public GroupStatusOutputModel GroupStatus { get; set; }
    }
}
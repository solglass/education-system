using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public CourseDto Course { get; set; }                      // delete
        public GroupStatusDto GroupStatus { get; set; }            // delete
        //public CourseOutputModel Course { get; set; }
        //public GroupStatusOutputModel GroupStatus { get; set; }
    }
}
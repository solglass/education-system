using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupOutputModel
    {
        public string StartDate { get; set; }
        public CourseDto Course { get; set; }                      // delete
        public GroupStatusDto GroupStatus { get; set; }            // delete
        //public CourseOutputModel Course { get; set; }
        //public GroupStatusOutputModel GroupStatus { get; set; }
    }
}
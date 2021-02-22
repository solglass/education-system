using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public CourseDto Course { get; set; }                     
        public GroupStatus GroupStatus { get; set; } 
    }
}
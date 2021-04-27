using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public CourseOutputModel Course { get; set; }                     
        public string GroupStatus { get; set; } 
        public int GroupStatusId { get; set; }
        public string EndDate { get; set; }
    }
}
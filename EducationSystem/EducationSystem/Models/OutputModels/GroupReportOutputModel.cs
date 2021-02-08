using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupReportOutputModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StudentCount { get; set; }

        public int TutorCount { get; set; }

        public int TeacherCount { get; set; }
    }
}
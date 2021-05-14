using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class HomeworkOutputModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string DeadlineDate { get; set; }
        public bool IsOptional { get; set; }
        public CourseOutputModel Course { get; set; }
        public List<int> GroupsIds { get; set; }
        public List<TagOutputModel> Tags { get; set; }
        public List<HomeworkAttemptOutputModel> HomeworkAttempts { get; set; }
        public List<ThemeOutputModel> Themes { get; set; }
    }
}

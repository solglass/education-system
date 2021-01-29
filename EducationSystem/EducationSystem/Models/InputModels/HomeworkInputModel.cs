using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkInputModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int GroupId { get; set; }
        public List<TagInputModel> Tags { get; set; }
        public List<HomeworkInputModel> HomeworkAttempts { get; set; }
        public List<ThemeInputModel> Themes { get; set; }
        public bool IsOptional { get; set; }
        public bool IsDeleted { get; set; }
    }
}

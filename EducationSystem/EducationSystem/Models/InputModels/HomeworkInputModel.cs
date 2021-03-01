using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkInputModel
    {
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string DeadlineDate { get; set; }
        public int GroupId { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> ThemeIds { get; set; }
        public bool IsOptional { get; set; }
    }
}

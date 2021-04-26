using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class GroupWithUsersOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public CourseOutputModel Course { get; set; }
        public string GroupStatus { get; set; }
        public int GroupStatusId { get; set; }
        public List<UserOutputModel> Students { get; set; }
        public List<UserOutputModel> Teachers { get; set; }
        public List<UserOutputModel> Tutors { get; set; }
    }
}

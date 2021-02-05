using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class GroupDto
    {
        public int Id { get; set; }     
        public DateTime StartDate { get; set; }
        public CourseDto Course { get; set; }
        public GroupStatusDto GroupStatus { get; set; }

        public override bool Equals(object obj)
        {
            GroupDto groupDto = (GroupDto)obj;

            if (groupDto.StartDate == StartDate && groupDto.Course.Equals(Course) && groupDto.GroupStatus.Equals(GroupStatus))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string s = "";

            s += StartDate + " " + Course.ToString() + " " + GroupStatus.ToString() + "; ";
            return s;
        }
    }
}

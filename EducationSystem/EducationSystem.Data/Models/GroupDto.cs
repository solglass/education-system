using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class GroupDto : ICloneable
    {
        public int Id { get; set; }     
        public DateTime StartDate { get; set; }
        public CourseDto Course { get; set; }
        public GroupStatus GroupStatus { get; set; }

        public object Clone()
        {
            return new GroupDto
            {
                Id = Id,
                StartDate = StartDate,
                Course = Course,
                GroupStatus = GroupStatus
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is GroupDto))
                return false;

            GroupDto groupDto = (GroupDto)obj;

            if (groupDto.Id != Id ||
                !groupDto.StartDate.Equals(StartDate) ||
                groupDto.GroupStatus != GroupStatus)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            string s = "";

            s += StartDate + " " + Course.ToString() + " " + GroupStatus.ToString() + "; ";
            return s;
        }
    }
}

using EducationSystem.Core.Enums;
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
        public GroupStatus GroupStatus { get; set; }

        public override bool Equals(object obj)
        {
            GroupDto groupDto = (GroupDto)obj;

            if(object.ReferenceEquals(groupDto, null) && object.ReferenceEquals(this, null))
            {
                return true;
            }
            if(object.ReferenceEquals(groupDto, null))
            {
                return false;   
            }
            if (groupDto.StartDate == StartDate)
            {
                if (groupDto.Course.Equals(Course))
                {
                    if (groupDto.GroupStatus.Equals(GroupStatus))
                    {

                        return true;
                    }
                }
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

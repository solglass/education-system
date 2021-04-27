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
        public DateTime EndDate { get; set; }
        public List<UserDto> Students { get; set; }
        public List<UserDto> Teachers { get; set; }
        public List<UserDto> Tutors { get; set; }

        public object Clone()
        {
            return new GroupDto
            {             
                StartDate = StartDate,
                Course = Course != null ? (CourseDto)Course.Clone(): null,
                GroupStatus = GroupStatus,
                EndDate = EndDate
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GroupDto))
                return false;

            GroupDto groupDto = (GroupDto)obj;

            return (groupDto.Id == Id &&
                groupDto.StartDate.Equals(StartDate) &&
                groupDto.GroupStatus == GroupStatus &&
                groupDto.EndDate.Equals(EndDate));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Id} {StartDate} {Course} {GroupStatus} {EndDate}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class StudentGroupDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
        public int ContractNumber { get; set; }

        public object Clone()
        {
            UserDto userClone = null;
            GroupDto groupClone = null;

            if (User != null)
            {
                userClone = (UserDto)User.Clone();
            }

            if (Group != null)
            {
               groupClone = (GroupDto)Group.Clone();
            }

            return new StudentGroupDto()
            {
                User = userClone,
                Group = groupClone,
                ContractNumber = ContractNumber
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is StudentGroupDto))
                return false;

            var studentGroupDto = (StudentGroupDto)obj;
            return (studentGroupDto.Id == Id &&
                User.Id==studentGroupDto.User.Id &&
                Group.Id==studentGroupDto.Group.Id &&
               ContractNumber == ContractNumber);


        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = $" #{Id} user#{User.Id} {User.FirstName} {User.LastName} group#{Group.Id} {Group.GroupStatus} contract#{ContractNumber} ";
            return s;
        }
    }
}

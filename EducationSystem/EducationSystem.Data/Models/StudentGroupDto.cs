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
            return new StudentGroupDto()
            {
                User = User,
                Group = Group,
                ContractNumber = ContractNumber
            };
        }

        public override bool Equals(object obj)
        {
            var studentGroupDto = (StudentGroupDto)obj;
            return ((studentGroupDto.Id == Id) &&
                User.Equals(studentGroupDto.User) &&
                Group.Equals(studentGroupDto.Group) &&
               (ContractNumber == ContractNumber));


        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

            s += Id + " " + User + " " + Group + " " + ContractNumber + "; ";
            return s;
        }
    }
}

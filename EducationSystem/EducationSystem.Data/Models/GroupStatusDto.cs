using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class GroupStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            GroupStatusDto groupStatusDto = (GroupStatusDto)obj;

            if (object.ReferenceEquals(groupStatusDto, null) && object.ReferenceEquals(this, null))
            {
                return true;
            }
            if (object.ReferenceEquals(groupStatusDto, null))
            {
                return false;
            }
            if (
                groupStatusDto.Name == Name
                )
            {
                return true;
            }

            return false;
        }
    }
}
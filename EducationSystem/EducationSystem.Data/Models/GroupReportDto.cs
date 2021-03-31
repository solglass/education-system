using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EducationSystem.Data.Models
{
   public class GroupReportDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
  
        public DateTime EndDate { get; set; }
        public int StudentCount { get; set; }

        public int TutorCount { get; set; }

        public int TeacherCount { get; set; }

        public object Clone()
        {
            return new GroupReportDto
            {
                StartDate = StartDate,
                EndDate = EndDate,
            };
        }
        public override bool Equals(object obj)
        {
            GroupReportDto groupDto = (GroupReportDto)obj;

            if (groupDto.StartDate == StartDate && groupDto.EndDate == EndDate &&
                groupDto.GroupId == GroupId && groupDto.Name == Name)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string s = "";

            s += StartDate + " " + EndDate + " " + Name + " " + GroupId + "; ";
            return s;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}

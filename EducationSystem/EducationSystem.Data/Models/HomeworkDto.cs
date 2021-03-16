using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkDto : ICloneable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool IsOptional { get; set; }
        public bool IsDeleted { get; set; }
        public GroupDto Group { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<HomeworkAttemptDto> HomeworkAttempts{ get; set; }
        public List<ThemeDto> Themes { get; set; }

        public object Clone()
        {
            return new HomeworkDto
            {
                Id = this.Id,
                Description = this.Description,
                StartDate = this.StartDate,
                DeadlineDate = this.DeadlineDate,
                IsOptional = this.IsOptional,
                IsDeleted = this.IsDeleted
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is HomeworkDto))
                return false;

            HomeworkDto homeworkObj = (HomeworkDto)obj;
            if (Id != homeworkObj.Id ||
                Description != homeworkObj.Description ||
                StartDate.Equals(homeworkObj.StartDate) ||
                DeadlineDate.Equals(homeworkObj.DeadlineDate)  ||
                IsOptional != homeworkObj.IsOptional ||
                IsDeleted != homeworkObj.IsDeleted)
            {
                return false;
            }

            return true;
        }
    }
}

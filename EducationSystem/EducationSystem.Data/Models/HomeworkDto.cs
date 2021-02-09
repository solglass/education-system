using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class HomeworkDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public GroupDto Group { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<HomeworkAttemptDto> HomeworkAttempts{ get; set; }
        public List<ThemeDto> Themes { get; set; }
        public bool IsOptional { get; set; }
        public bool IsDeleted { get; set; }
        public override bool Equals(object obj)
        {
            HomeworkDto homeworkObj = (HomeworkDto)obj;
            if(Id != homeworkObj.Id || Description != homeworkObj.Description || StartDate != homeworkObj.StartDate || DeadlineDate != homeworkObj.DeadlineDate || IsOptional != homeworkObj.IsOptional || IsDeleted != homeworkObj.IsDeleted)
            {
                return false;
            }
            if (!Group.Equals(homeworkObj.Group))
            {
                return false;
            }
            if(Tags.Count != homeworkObj.Tags.Count || HomeworkAttempts.Count != homeworkObj.HomeworkAttempts.Count || Themes.Count != homeworkObj.Themes.Count)
            {
                return false;
            }
            for(int i = 0; i < Tags.Count; ++i)
            {
                if (Tags[i].Equals(homeworkObj.Tags[i]))
                {
                    return false;
                }
            }
            for(int i = 0; i < HomeworkAttempts.Count; ++i)
            {
                if (HomeworkAttempts[i].Equals(homeworkObj.HomeworkAttempts[i]))
                {
                    return false;
                }
            }
            for(int i = 0; i < Themes.Count; ++i)
            {
                if (Themes[i].Equals(homeworkObj.Themes[i]))
                {
                    return false;
                }
            }

                return true;
        }
    }
}

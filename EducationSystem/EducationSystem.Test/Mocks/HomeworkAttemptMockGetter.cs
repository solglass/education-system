using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class HomeworkAttemptMockGetter
    {
        public static HomeworkAttemptDto GetHomeworkAttemptDtoMock(int id)
        {
            return id switch
            {
                1 => new HomeworkAttemptDto()
                {
                    Comment = "Test Comment1 (Description) Here",
                    HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                    Attachments = new List<AttachmentDto>(),
                    Comments=new List<CommentDto>(),
                    Author = new UserDto(),
                    Homework= new HomeworkDto()
                    
                },
                2 => new HomeworkAttemptDto()
                {
                    Comment = "Test Comment2 (Description) Here",
                    HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                    Attachments = new List<AttachmentDto>(),
                    Comments = new List<CommentDto>(),
                    Author = new UserDto(),
                    Homework = new HomeworkDto()
                },
                3 => new HomeworkAttemptDto()
                {
                    Comment = "Test Comment3 (Description) Here",
                    HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                    Attachments = new List<AttachmentDto>(),
                    Comments = new List<CommentDto>(),
                    Author = new UserDto(),
                    Homework = new HomeworkDto()
                },
                _ => null,
            };
        }
    }
}

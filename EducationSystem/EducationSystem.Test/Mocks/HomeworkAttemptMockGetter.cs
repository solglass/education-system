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
            switch (id)
            {
                case 1:
                    return new HomeworkAttemptDto()
                    {
                        Comment = "Test Comment1 (Description) Here",
                        HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                        IsDeleted = false 
                    };
                    break;
                case 2:
                    return new HomeworkAttemptDto()
                    {
                        Comment = "Test Comment2 (Description) Here",
                        HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                        IsDeleted = false
                    };
                    break;
                case 3:
                    return new HomeworkAttemptDto()
                    {
                        Comment = "Test Comment3 (Description) Here",
                        HomeworkAttemptStatus = (HomeworkAttemptStatus)3,
                        IsDeleted = false
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

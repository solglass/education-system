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
                        Comment = "TestComment1",
                        HomeworkAttemptStatus = (Core.Enums.HomeworkAttemptStatus)3,
                        IsDeleted = false 
                    };
                    break;
                case 2:
                    return new HomeworkAttemptDto()
                    {
                        Comment = "TestComment2",
                        HomeworkAttemptStatus = (Core.Enums.HomeworkAttemptStatus)1,
                        IsDeleted = false
                    };
                    break;
                case 3:
                    return new HomeworkAttemptDto()
                    {
                        Comment = "TestComment3",
                        HomeworkAttemptStatus = (Core.Enums.HomeworkAttemptStatus)2,
                        IsDeleted = false
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

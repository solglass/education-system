using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class HomeworkMockGetter
    {
        public static HomeworkDto GetHomeworkDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new HomeworkDto
                    {
                        Description = "Test 1 mock",
                        StartDate = DateTime.Now,
                        DeadlineDate = DateTime.Now,
                        IsOptional = true
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

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
                case 2:
                    return new HomeworkDto
                    {
                        Description = "Test 2 mock",
                        StartDate = DateTime.Now,
                        DeadlineDate = DateTime.Now,
                        IsOptional = false
                    };
                    break;
                case 3:
                    return new HomeworkDto
                    {
                        Description = "Test 3 mock",
                        StartDate = DateTime.Now.AddDays(1),
                        DeadlineDate = DateTime.Now.AddDays(2),
                        IsOptional = true
                    };
                    break;
                case 4:
                    return new HomeworkDto();
                default:
                    return null;
            }
        }
    }
}

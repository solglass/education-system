using EducationSystem.Data.Models;
using System;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class HomeworkMockGetter
    {
        public static HomeworkDto GetHomeworkDtoMock(int id)
        {
            return id switch
            {
                1 => new HomeworkDto
                {
                    Description = "Test 1 mock",
                    StartDate = DateTime.Now,
                    DeadlineDate = DateTime.Now,
                    IsOptional = true
                },
                2 => new HomeworkDto
                {
                    Description = "Test 2 mock",
                    StartDate = DateTime.Now,
                    DeadlineDate = DateTime.Now,
                    IsOptional = false
                },
                3 => new HomeworkDto
                {
                    Description = "Test 3 mock",
                    StartDate = DateTime.Now.AddDays(1),
                    DeadlineDate = DateTime.Now.AddDays(2),
                    IsOptional = true
                },
                4 => new HomeworkDto(),
                _ => null,
            };
        }
    }
}

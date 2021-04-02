using EducationSystem.Data.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class HomeworkMockGetter
    {
        public static HomeworkDto GetHomeworkDtoMock(int id)
        {
            return id switch
            {
                1 => new HomeworkDto
                {
                    Description = "Test 1 mock",
                    StartDate = new DateTime(2021, 2, 28),
                    DeadlineDate = new DateTime(2021, 3, 1),
                    IsOptional = true
                },
                2 => new HomeworkDto
                {
                    Description = "Test 2 mock",
                    StartDate = new DateTime(2021, 3, 31),
                    DeadlineDate = new DateTime(2021, 4, 1),
                    IsOptional = false
                },
                3 => new HomeworkDto
                {
                    Description = "Test 3 mock",
                    StartDate = new DateTime(2021, 4, 30),
                    DeadlineDate = new DateTime(2021, 5, 1),
                    IsOptional = true
                },
                4 => new HomeworkDto(),
                _ => null,
            };
        }
    }
}

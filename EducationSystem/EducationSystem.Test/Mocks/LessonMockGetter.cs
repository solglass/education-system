using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class LessonMockGetter
    {
        public static LessonDto GetLessonDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new LessonDto
                    {
                        Comment = "Test LEsson 1",
                        IsDeleted = false,
                        Date = DateTime.Now.AddDays(-1)
                    };
                    break;
                case 2:
                    return new LessonDto
                    {
                        Comment = "Test Lesson 2",
                        IsDeleted = false,
                        Date = DateTime.Now.AddDays(-5)
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}

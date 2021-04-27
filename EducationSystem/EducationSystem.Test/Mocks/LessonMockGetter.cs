using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class LessonMockGetter
    {
        public static LessonDto GetLessonDtoMock(int id)
        {
            return id switch
            {
                0 => new LessonDto(),
                1 => new LessonDto
                {
                    Description = "Test Lesson 1",
                    IsDeleted = false,
                    Date = new DateTime(2020, 10, 10, 18, 30, 0).AddDays(-1),
                    Themes = new List<ThemeDto>(),
                    RecordLink = "http://downloadme.org/lesson1.mp4"
                },
                2 => new LessonDto
                {
                    Description = "Test Lesson 2",
                    IsDeleted = false,
                    Date = new DateTime(2020, 10, 10, 18, 30, 0).AddDays(-5),
                    Themes = new List<ThemeDto>(),
                    RecordLink = "http://downloadme.org/lesson2.mp4"
                },
                3 => new LessonDto
                {
                    Description = "Test Lesson 3",
                    IsDeleted = false,
                    Date = new DateTime(2020, 10, 10, 18, 30, 0).AddDays(+15),
                    Themes = new List<ThemeDto>()
                },
                4 => new LessonDto
                {
                    Description = "Test Lesson 4",
                    IsDeleted = false,
                    Date = new DateTime(2020, 10, 10, 18, 30, 0).AddDays(+20)
                },
                5 => new LessonDto(),
                _ => new LessonDto
                {
                    Description = $"Test Lesson {id}",
                    IsDeleted = false,
                    Date = new DateTime(2020, 10, 10, 18, 30, 0).AddDays(+id),
                    Themes = new List<ThemeDto>()
                },
            };
        }
    }
}

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
            return id switch
            {
                0 => new LessonDto(),
                1 => new LessonDto
                {
                    Description = "Test Lesson 1",
                    IsDeleted = false,
                    Date = DateTime.Now.AddDays(-1),
                    Themes = new List<ThemeDto>()
                },
                2 => new LessonDto
                {
                    Description = "Test Lesson 2",
                    IsDeleted = false,
                    Date = DateTime.Now.AddDays(-5),
                    Themes = new List<ThemeDto>()
                },
                3 => new LessonDto
                {
                    Description = "Test Lesson 3",
                    IsDeleted = false,
                    Date = DateTime.Now.AddDays(+15),
                    Themes = new List<ThemeDto>()
                },
                _ => new LessonDto
                {
                    Description = $"Test Lesson {id}",
                    IsDeleted = false,
                    Date = DateTime.Now.AddDays(+id),
                    Themes = new List<ThemeDto>()
                },
            };
        }
    }
}

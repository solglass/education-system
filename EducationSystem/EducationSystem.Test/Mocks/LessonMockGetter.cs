using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class LessonMockGetter
    {
        public static LessonDto GetlessonDtoMock(int id)
        {
            return id switch
            {
                1 => new LessonDto { Description = "first test comment", Date = new DateTime(2021, 4, 15)},
                2 => new LessonDto { Description = "second test comment", Date = new DateTime(2021, 8, 17) }
            };
        }
    }
}

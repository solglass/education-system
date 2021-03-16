using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class CourseMockGetter
    {
        public static CourseDto GetCourseDtoMock(int id)
        {
            return id switch
            {
                1 => new CourseDto
                {
                    Description = "Course Test 1 mock",
                    Duration = 8,
                    Name = "Test 1 C#"
                },
                2 => new CourseDto
                {
                    Description = "Course Test 2 mock",
                    Duration = 16,
                    Name = "Test 2 C#"
                },
                3 => new CourseDto
                {
                    Description = "Course Test 3 mock",
                    Duration = 32,
                    Name = "Test 3 C#"
                },
                4 => new CourseDto
                {
                    Description = "Course Test 4 mock",
                    Duration = 4,
                    Name = "Test 4 C#"
                },
                5 => new CourseDto
                {
                    Description = "Course Test 5 mock",
                    Duration = 2,
                    Name = "Test 5 C#"
                },
                _ => null,
            };
        }
    }
}

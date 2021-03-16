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
            switch (id)
            {
                case 1:
                    return new CourseDto
                    {
                        Description = "Course Test 1 mock",
                        Duration = 8,
                        Name = "Test C#"                        
                    };
                    break;
                default:
                    return null;
            }
        }

        public static CourseDto GetCourseDtoWithIdMock(int id)
        {
            return new CourseDto
            {
                Id = id,
                Description = $"Course Test {id} mock",
                Duration = 8,
                Name = "Test C#"
            };
        }
    }
}

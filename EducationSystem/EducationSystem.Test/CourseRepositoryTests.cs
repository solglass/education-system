using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Test
{
    public class CourseRepositoryTest
    {
        private int _courseId;
        private List<int> _groupIdArray;
        private List<int> _themeIdArray;




        public List<GroupDto>  GetGroupMock(int n)
        {
            List<GroupDto> groups = new List<GroupDto>() ;
            switch (n)
            {
                case 1:
                    return groups;
                case 2:
                    groups.Add(new GroupDto { StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 3:
                    groups.Add(new GroupDto { StartDate = new System.DateTime(2020, 10, 12) });
                    groups.Add(new GroupDto { StartDate = new System.DateTime(2021, 10, 12) });
                    return groups;
                default:
                    return groups;
            }
        }
        public List<ThemeDto> GetThemeMock(int n)
        {
            List<ThemeDto> themes = new List<ThemeDto>();
            switch (n)
            {
                case 1:
                    return themes;
                case 2:
                    themes.Add(new ThemeDto { Name="Test theme 1"});
                    return themes;
                case 3:
                    themes.Add(new ThemeDto { Name = "Test theme 2" });
                    themes.Add(new ThemeDto { Name = "Test theme 3" });
                    return themes;
                default:
                    return themes;
            }
        }

        public CourseDto GetCourseMock(int n)
        {
            CourseDto course = new CourseDto();
            switch (n)
            {
                case 1:
                    course = new CourseDto() { Name = "TestCourseCase 1", Description = "Test case 1", Duration = 1 };
                    return course;
                case 2:
                    course = new CourseDto() { Name = "TestCourseCase 2", Description = "Test case 2", Duration = 2 };
                    return course;
                case 3:
                    course = new CourseDto() { Name = "TestCourseCase 3", Description = "Test case 3", Duration = 3 };
                    return course;
                default:
                    return course;
            }
        }





    }
}
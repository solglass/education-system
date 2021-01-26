using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Test
{
    [TestFixture]
    public class CourseTests
    {
        private CourseRepository _courseRepo = new CourseRepository();
        private GroupRepository _groupRepo = new GroupRepository();
        private int _courseId;
        private List<int> _groupIdList;
        private List<int> _themeIdList;
        private CourseDto _expectedCourse;
        private List<CourseDto> _coursesFromDb;
        private List<int> _courseThemeIdList;
        [TestCase(1)]
      
        public void TestAddCourse(int courseMock )
        {
            CourseDto course = GetCourseMock(courseMock);
            _courseId = _courseRepo.AddCourse(_expectedCourse);
            foreach (var group in _expectedCourse.Groups)
            {
                group.Course.Id = _courseId;
                _groupIdList.Add(_groupRepo.AddGroup(group));
            }
            foreach (var id in _themeIdList)
            {
               _courseThemeIdList.Add( _courseRepo.AddCourse_Theme(_courseId, id));
            }
            CourseDto actualCourse = _courseRepo.GetCourseById(_courseId);
            Assert.AreEqual(_expectedCourse, actualCourse);
        }

        [Test]
        public void TestDeleteCourse()
        {
            foreach (var id in _courseThemeIdList)
            {
                if (_courseRepo.DeleteCourse_Theme(id) != 1)
                    throw new System.Exception();
            }
            foreach (var themeId in _themeIdList)
            {
                if (_courseRepo.DeleteTheme(themeId) != 1)
                    throw new System.Exception();
            }
            foreach (var groupId in _groupIdList)
            {
                if (_groupRepo.DeleteGroup(groupId) != 1)
                    throw new System.Exception();
            }
            if (_courseRepo.DeleteCourse(_courseId) != 1)
            {
                Assert.Fail();
            }
            else
            {
                List<CourseDto> actualCourses = _courseRepo.GetCourses();
                if (actualCourses.Count == _coursesFromDb.Count)
                {
                    for (int i = 0; i < actualCourses.Count; i++)
                    {
                        if (actualCourses[i].Id != _coursesFromDb[i].Id) Assert.Fail();
                    }
                    Assert.Pass();
                }
                else Assert.Fail();
            }
        }
        [SetUp]
        public void SetUpTest()
        {
            _coursesFromDb = new List<CourseDto>();
            _courseThemeIdList = new List<int>();
            _groupIdList = new List<int>();
            _themeIdList = new List<int>();
            _expectedCourse = GetCourseMock(1);
            _expectedCourse.Themes = GetThemeMock(3);
            _expectedCourse.Groups = GetGroupMock(3);
            foreach(var theme in _expectedCourse.Themes)
            {
                int i = _courseRepo.AddTheme(theme.Name);
                _themeIdList.Add(i);
            }
            
            _coursesFromDb.AddRange(_courseRepo.GetCourses());
        }
       

        [TearDown]
        public void TearDowTest()
        {
            foreach (int id in _courseThemeIdList)
            {
              _courseRepo.DeleteCourse_Theme(id);
            }
            foreach (int themeId in _themeIdList)
            {
              _courseRepo.DeleteTheme(themeId);
            }
            foreach (int groupId in _groupIdList)
            {
              _groupRepo.DeleteGroup(groupId);
            }
            _courseRepo.DeleteCourse(_courseId);
            
        }
        public List<GroupDto>  GetGroupMock(int n)
        {
            List<GroupDto> groups = new List<GroupDto>() ;
            switch (n)
            {
                case 1:
                    return groups;
                case 2:
                    groups.Add(new GroupDto {GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 3:
                    groups.Add(new GroupDto {GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    groups.Add(new GroupDto {GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2021, 10, 12) });
                  
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
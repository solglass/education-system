using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data.Tests
{
    [TestFixture]
    public class CourseTests
    {
        private CourseRepository _courseRepo;
        private int _courseId;
        private List<int> _themeIdList;
        private CourseDto _expectedCourse;
        private List<CourseDto> _coursesFromDb;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository();
            _coursesFromDb = new List<CourseDto>();
            _themeIdList = new List<int>();
            _expectedCourse = GetCourseMock(1);
            _expectedCourse.Themes = GetThemeMock(3);
            foreach (var theme in _expectedCourse.Themes)
            {
                _themeIdList.Add(_courseRepo.AddTheme(theme.Name));
            }
            _coursesFromDb.AddRange(_courseRepo.GetCourses());
        }


        [TestCase(1), Order(1)]
        public void TestAddCourse(int courseMock )
        {
            CourseDto course = GetCourseMock(courseMock);
            _courseId = _courseRepo.AddCourse(_expectedCourse);
            foreach (var id in _themeIdList)
            {
               _courseRepo.AddCourse_Theme(_courseId, id);
            }
            CourseDto actualCourse = _courseRepo.GetCourseById(_courseId);
            Assert.AreEqual(_expectedCourse, actualCourse);
        }

        [TestCase(2), Order(2)]
        public void TestUpdateCourse(int courseMock)
        {
            CourseDto tempCourse = GetCourseMock(courseMock);
            _expectedCourse.Id = _courseId;
            _expectedCourse.Name = tempCourse.Name;
            _expectedCourse.Duration = tempCourse.Duration;
            if (_courseRepo.UpdateCourse(_expectedCourse) == 1)
            {
                CourseDto actualCourse = _courseRepo.GetCourseById(_courseId);
                Assert.AreEqual(_expectedCourse, actualCourse);
            }
            else Assert.Fail("Course update went wrong, the amount of affected rows is not 1");
        }

        [Test, Order(3)]
        public void TestDeleteCourse()
        {
            foreach (var themeId in _themeIdList)
            {
                if (_courseRepo.DeleteCourse_Theme(_courseId, themeId) != 1)
                    throw new System.Exception("Course_theme delete went wrong, the amount of affected rows is not 1");
                if (_courseRepo.DeleteTheme(themeId) != 1)
                    throw new System.Exception("Theme delete went wrong, the amount of affected rows is not 1");
            }
            if (_courseRepo.DeleteCourse(_courseId) != 1)
            {
                Assert.Fail("Course delete went wrong, the amount of affected rows is not 1");
            }
            else
            {
                List<CourseDto> actualCourses = _courseRepo.GetCourses();
                if (actualCourses.Count == _coursesFromDb.Count)
                {
                    for (int i = 0; i < actualCourses.Count; i++)
                    {
                        if (actualCourses[i].Id != _coursesFromDb[i].Id) 
                            Assert.Fail("Something wrong was deleted");
                    }
                    Assert.Pass();
                }
                else Assert.Fail("The amount of courses before and after don't match");
            }
        }
       
       

        [OneTimeTearDown]
        public void TearDowTest()
        {
            foreach (int themeId in _themeIdList)
            {
                _courseRepo.DeleteCourse_Theme(_courseId, themeId);
                _courseRepo.DeleteTheme(themeId);
            }
            _courseRepo.DeleteCourseHard(_courseId);
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
                default:
                    return course;
            }
        }
    }
}
using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using EducationSystem.Data.Tests.Mocks;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data.Tests
{
    public class CourseTests:BaseTest
    {
        private CourseRepository _courseRepo;

        private List<int> _courseIds;
        private List<int> _themeIds;
        private List<(int, int)> _courseThemes;

        private ThemeDto _themeMock;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository(_options);

            _themeIds = new List<int>();
            _courseThemes = new List<(int, int)>();
            _courseIds = new List<int>();

            _themeMock = ThemeMockGetter.GetThemeDtoMock(1);
            _themeMock.Id = _courseRepo.AddTheme(_themeMock);
            _themeIds.Add(_themeMock.Id);
        }

        [TestCase(1)]
        public void CourseAddPositiveTest(int mockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);

            //When
            var actual = _courseRepo.GetCourseById(course.Id);

            //Then
            Assert.AreEqual(course, actual);
        }
       
        [TestCase(1)]
        public void CourseUpdatePositiveTest(int mockId)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            course.Description = "Updated course description";
            course.Name = "Updated course name";
            course.Duration = 4;
            var result = _courseRepo.UpdateCourse(course);
            Assert.AreEqual(1,result);

            //When
            var actual = _courseRepo.GetCourseById(course.Id);

            //Then
            Assert.AreEqual(course, actual);
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void CourseDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            course.IsDeleted = isDeleted;
            var result = _courseRepo.DeleteOrRecoverCourse(course.Id, isDeleted);
            Assert.AreEqual(1, result);

            //When
            var actual = _courseRepo.GetCourseById(course.Id);

            //Then
            Assert.AreEqual(course, actual);
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            DeleteCourseThemes();
            DeleteThemes();
            DeleteCourses();
        }
        private void DeleteCourseThemes()
        {
            foreach (var ids in _courseThemes)
            {
                _courseRepo.DeleteCourse_Theme(ids.Item1, ids.Item2);
            }
        }
        private void DeleteThemes()
        {
            foreach (var id in _themeIds)
            {
                _courseRepo.HardDeleteTheme(id);
            }
        }
       
        private void DeleteCourses()
        {
            foreach(var id in _courseIds)
            {
                _courseRepo.HardDeleteCourse(id);
            }
        }


    }
}
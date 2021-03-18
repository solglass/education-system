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
        private ICourseRepository _courseRepo;

        private List<int> _courseIds;
        private List<int> _themeIds;
        private List<(int, int)> _courseThemes;


        [OneTimeSetUp]
        public void SetUpTest()
        {
            _courseRepo = new CourseRepository(_options);

            _themeIds = new List<int>();
            _courseThemes = new List<(int, int)>();
            _courseIds = new List<int>();
        }
        
        [TestCase(new int[] { 1,2,3}, new int[] { })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1,2})]
        [TestCase(new int[] { 1}, new int[] { 1})]
        [TestCase(new int[] { 1 }, new int[] { 1,2 })]
        public void GetAllCoursesTest(int[] courseMockIds, int[] themeMockIds)
        {
            //Given
            var expected = _courseRepo.GetCourses();
            foreach(var courseId in courseMockIds)
            {
                var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(courseId).Clone();
                course.Id = _courseRepo.AddCourse(course);
                Assert.Greater(course.Id, 0);
                _courseIds.Add(course.Id);
                foreach (var themeId in themeMockIds)
                {
                    var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeId).Clone();
                    theme.Id = _courseRepo.AddTheme(theme);
                    Assert.Greater(course.Id, 0);
                    _themeIds.Add(theme.Id);

                    var result = _courseRepo.AddCourse_Theme(course.Id, theme.Id);
                    Assert.Greater(result, 0);
                    _courseThemes.Add((course.Id, theme.Id));
                    course.Themes.Add(theme);
                }
                expected.Add(course);
            }

            //When
            var actual = _courseRepo.GetCourses();

            //Them
            CollectionAssert.AreEqual(expected, actual);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEqual(expected[i].Themes, actual[i].Themes);
            }
        }


        [TestCase(1, new int[] { 1,2})]
        public void GetCourseByIdPositiveTest(int mockId, int[] themeIds)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);

            foreach(var item in themeIds)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(item).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                Assert.Greater(course.Id, 0);
                _themeIds.Add(theme.Id);

                var result = _courseRepo.AddCourse_Theme(course.Id, theme.Id);
                Assert.Greater(result, 0);
                _courseThemes.Add((course.Id, theme.Id));
                course.Themes.Add(theme);
            }

            //When
            var actual = _courseRepo.GetCourseById(course.Id);

            //Then
            Assert.AreEqual(course, actual);
            CollectionAssert.AreEqual(course.Themes, actual.Themes);
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
       
        [TestCase(2)]
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

        [TestCase(3, true)]
        [TestCase(4, false)]
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


        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(1, new int[] { 2 })]
        [TestCase(1, new int[] { })]
        public void AddCourseThemePositiveTest(int mockId, int[] themeMockIds)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            var themes = new List<ThemeDto>();
            foreach (var themeMockId in themeMockIds)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                Assert.Greater(theme.Id, 0);
                themes.Add(theme);
                _themeIds.Add(theme.Id);
                var result = _courseRepo.AddCourse_Theme(course.Id, theme.Id);
                Assert.Greater(result, 0);
                _courseThemes.Add((course.Id, theme.Id));
            }

            //When
            var actual = _courseRepo.GetCourseById(course.Id);
            //Then
            CollectionAssert.AreEqual(themes, actual.Themes);
        }

        [TestCase(1, new int[] { 1, 2, 3 })]
        [TestCase(1, new int[] { 2 })]
        [TestCase(1, new int[] { })]
        public void DeleteCourseThemePositiveTest(int mockId, int[] themeMockIds)
        {
            //Given
            var course = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            course.Id = _courseRepo.AddCourse(course);
            Assert.Greater(course.Id, 0);
            _courseIds.Add(course.Id);
            var themes = new List<ThemeDto>();
           
            foreach (var themeMockId in themeMockIds)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                Assert.Greater(theme.Id, 0);

                themes.Add(theme);
                _themeIds.Add(theme.Id);
                var result = _courseRepo.AddCourse_Theme(course.Id, theme.Id);
                Assert.Greater(result, 0);

                _courseThemes.Add((course.Id, theme.Id));
            }

            foreach (var themeMockId in themeMockIds)
            {
                var theme = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
                theme.Id = _courseRepo.AddTheme(theme);
                Assert.Greater(theme.Id, 0);

                _themeIds.Add(theme.Id);
                var result = _courseRepo.AddCourse_Theme(course.Id, theme.Id);
                Assert.Greater(result, 0);

                _courseThemes.Add((course.Id, theme.Id));
                result = _courseRepo.DeleteCourse_Theme(course.Id, theme.Id);
                Assert.Greater(result, 0);
                
            }

            //When
            var actual = _courseRepo.GetCourseById(course.Id);
            //Then
            CollectionAssert.AreEqual(themes, actual.Themes);
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
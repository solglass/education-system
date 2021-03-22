using EducationSystem.Core.Config;
using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class LessonTest : BaseTest
    {
        private ILessonRepository _lessonRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;

        private List<int> _lessonIdList;
        private List<int> _groupIdList;
        private List<int> _themeIdList;
        private List<int> _courseIdList;
        private List<(int, int)> _lessonThemeList;

        private GroupDto _groupDtoMock;

        [OneTimeSetUp]
        public void LessonOneTimeSetUp()
        {
            _lessonRepo = new LessonRepository(_options);
            _groupRepo = new GroupRepository(_options);
            _courseRepo = new CourseRepository(_options);

            _groupIdList = new List<int>();
            _themeIdList = new List<int>();
            _courseIdList = new List<int>();
            _lessonThemeList = new List<(int, int)>();
            _lessonIdList = new List<int>();

            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_groupDtoMock.Course);
            _courseIdList.Add(addedCourseId);
            _groupDtoMock.Course.Id = addedCourseId;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;
        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void AddLessonThemePositiveTest(int[] mockIds)
        {
            // Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepo.AddLesson(lessonDto);
            _lessonIdList.Add(addedLessonId);
            lessonDto.Id = addedLessonId;

            // When
            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);

                _lessonRepo.AddLessonTheme(addedLessonId, addedThemeId);
                _lessonThemeList.Add((addedLessonId, addedThemeId));
            }

            var actual = _lessonRepo.GetLessonById(addedLessonId).Themes;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2, 3 })]
        public void DeleteLessonThemePositiveTest(int[] mockIds)
        {
            // Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepo.AddLesson(lessonDto);
            _lessonIdList.Add(addedLessonId);
            lessonDto.Id = addedLessonId;
            // When
            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);

                _lessonRepo.AddLessonTheme(addedLessonId, addedThemeId);
                _lessonThemeList.Add((addedLessonId, addedThemeId));
            }

            var toDeleteIdList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;

                _lessonRepo.AddLessonTheme(addedLessonId, addedThemeId);
                toDeleteIdList.Add((addedLessonId, addedThemeId));
            }
            var actual1 = _lessonRepo.GetLessonById(addedLessonId).Themes;
            // When
            toDeleteIdList.ForEach(themeTag =>
            {
                _lessonRepo.DeleteLessonTheme(themeTag.Item1, themeTag.Item2);
            });
            var actual = _lessonRepo.GetLessonById(addedLessonId).Themes;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void AddLessonTheme_NotExistLesson_NegativeTest()
        {
            //Given
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);
            themeDto.Id = addedThemeId;
            //When
            try
            {
                _lessonRepo.AddLessonTheme(-1, addedThemeId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddLessonTheme_NotExistTheme_NegativeTest()
        {
            //Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepo.AddLesson(lessonDto);
            _lessonIdList.Add(addedLessonId);
            lessonDto.Id = addedLessonId;
            //When
            try
            {
                _lessonRepo.AddLessonTheme(addedLessonId, -1);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddLessonThemeDoubleNegativeTest()
        {
                var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
                lessonDto.Group = _groupDtoMock;
                var addedLessonId = _lessonRepo.AddLesson(lessonDto);
                _lessonIdList.Add(addedLessonId);
                lessonDto.Id = addedLessonId;

                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
            try
            {

                _lessonRepo.AddLessonTheme(addedLessonId, addedThemeId);
                _lessonThemeList.Add((addedLessonId,addedThemeId));
                _lessonRepo.AddLessonTheme(addedLessonId, addedThemeId);
                _lessonThemeList.Add((addedLessonId, addedThemeId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [OneTimeTearDown]
        public void TearDowTest()
        {
            DeteleLessonThemes();
            DeleteLessons();
            DeleteGroups();
            DeleteCourses();
            DeleteThemes();
    }


        private void DeleteLessons()
        {
            foreach (int lessonId in _lessonIdList)
            {
                _lessonRepo.HardDeleteLesson(lessonId);
            }
        }
        private void DeleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }
        private void DeleteCourses()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }
        private void DeleteThemes()
        {
            foreach (int themeId in _themeIdList)
            {
                _courseRepo.HardDeleteTheme(themeId);
            }
        }
        private void DeteleLessonThemes()
        {
            foreach ((int lessonId,int themeId) in _lessonThemeList)
            {
                _lessonRepo.DeleteLessonTheme(lessonId, themeId);
            }
        }
    }
}
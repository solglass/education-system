using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    class LessonTests : BaseTest
    {
        private LessonRepository _lessonRepository;
        private GroupRepository _groupRepository;
        private CourseRepository _courseRepository;
        private List<int> _addedGroupsId;
        private List<int> _addedLessonId;
        private List<int> _addedCourseId;
        private List<int> _addedThemeId;
        private GroupDto _groupDtoMock;

        [OneTimeSetUp]
        public void OneTimeSerupTest()
        {
            _lessonRepository = new LessonRepository(_options);
            _groupRepository = new GroupRepository(_options);
            _courseRepository = new CourseRepository(_options);
            _addedGroupsId = new List<int>();
            _addedLessonId = new List<int>();
            _addedCourseId = new List<int>();
            _addedThemeId = new List<int>();
            _groupDtoMock = (GroupDto)GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = CourseMockGetter.GetCourseDtoMock(1);
            var courseId = _courseRepository.AddCourse(_groupDtoMock.Course);
            _groupDtoMock.Course.Id = courseId;
            _addedCourseId.Add(courseId);

        }

        [TestCase(2)]
        public void AddLessonPositiveTest(int mockId)
        {
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            expected.Group = groupDto;
            expected.Id = _lessonRepository.AddLesson(expected);
            _addedLessonId.Add(expected.Id);
            var actual = _lessonRepository.GetLessonById(expected.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void UpdateLessonPositiveTest(int mockId)
        {
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var lessonDto = LessonMockGetter.GetLessonDtoMock(mockId);
            lessonDto.Group = groupDto;
            lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
            _addedLessonId.Add(lessonDto.Id);
            var expected = new LessonDto
            {
                Id = lessonDto.Id,
                Description = "Update description",
                Date = DateTime.Now.AddDays(-20)
               
            };
            _lessonRepository.UpdateLesson(expected);
            var actual = _lessonRepository.GetLessonById(lessonDto.Id);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2 })]
        public void GetLessonsByGroupIdPositiveTest(int[] lessonIdMock)
        {
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var expected = new List<LessonDto>();
            for (int id = 0; id < lessonIdMock.Length; id++)
            {
                var lessonMock = (LessonDto)LessonMockGetter.GetLessonDtoMock(lessonIdMock[id]).Clone();
                lessonMock.Group = groupDto;
                var lessonId = _lessonRepository.AddLesson(lessonMock);
                lessonMock.Id = lessonId;
                _addedLessonId.Add(lessonId);
                expected.Add(lessonMock);
            }
            var actual = _lessonRepository.GetLessonsByGroupId(groupDto.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        public void GetLessonByIdPositiveTest(int lessonMockId)
        {
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(lessonMockId).Clone();
            expected.Group = groupDto;
            var addedLessonId = _lessonRepository.AddLesson(expected);
            expected.Id = addedLessonId;
            _addedLessonId.Add(addedLessonId);
            var actual = _lessonRepository.GetLessonById(addedLessonId);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(1, true)]
        [TestCase(2, false)]
        public void DeleteOrRecoverLessonPositiveTest(int lessonMockId, bool isDeleted)
        {
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(lessonMockId).Clone();
            expected.Group = groupDto;
            expected.Id = _lessonRepository.AddLesson(expected);
            _addedLessonId.Add(expected.Id);
            expected.IsDeleted = isDeleted;
            var countRows = _lessonRepository.DeleteOrRecoverLesson(expected.Id, expected.IsDeleted);
            Assert.AreEqual(1, countRows);
            var actual = _lessonRepository.GetLessonById(expected.Id);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2 })]
        public void GetLessonByThemeIdPositiveTest(int[] lessonMockcId)
        {
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            themeDto.Id = _courseRepository.AddTheme(themeDto);
            _addedThemeId.Add(themeDto.Id);
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupsId.Add(groupDto.Id);
            var expected = new List<LessonDto>();
            foreach(int id in lessonMockcId)
            {
                var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(id).Clone();
                lessonDto.Group = groupDto;
                lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
                _addedLessonId.Add(lessonDto.Id);
                _lessonRepository.AddLessonTheme(lessonDto.Id, themeDto.Id);
                expected.Add(lessonDto);
            }         
            var actual = _lessonRepository.GetLessonsByThemeId(themeDto.Id);
            CollectionAssert.AreEqual(expected, actual);
        }

        [OneTimeTearDown]
        public void TearDownTest()
        {
            DeleteGroups();
            DeleteLessons();
            DeleteCourses();
            DeleteThemes();
        }
        public void DeleteGroups()
        {
            foreach (int groupId in _addedGroupsId)
            {
                _groupRepository.HardDeleteGroup(groupId);
            }
        }
        public void DeleteLessons()
        {
            foreach (int lessonId in _addedLessonId)
            {
                _lessonRepository.HardDeleteLesson(lessonId);
            }
        }
        public void DeleteCourses()
        {
            foreach (int courseId in _addedCourseId)
            {
                _courseRepository.HardDeleteCourse(courseId);
            }
        }
        public void DeleteThemes()
        {
            foreach (int themeId in _addedThemeId)
            {
                _courseRepository.HardDeleteTheme(themeId);
            }
        }
    }
}

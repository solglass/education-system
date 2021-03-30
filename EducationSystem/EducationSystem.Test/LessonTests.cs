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
        private ILessonRepository _lessonRepository;
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;
        private List<int> _addedGroupIds;
        private List<int> _addedLessonIds;
        private List<int> _addedCourseIds;
        private List<int> _addedThemeIds;
        private List<(int, int)> _addedLessonThemeIds;
        private GroupDto _groupDtoMock;
        private CourseDto _courseDtoMock;

        [SetUp]
        public void OneTimeSerupTest()
        {
            _lessonRepository = new LessonRepository(_options);
            _groupRepository = new GroupRepository(_options);
            _courseRepository = new CourseRepository(_options);
            _addedGroupIds = new List<int>();
            _addedLessonIds = new List<int>();
            _addedCourseIds = new List<int>();
            _addedThemeIds = new List<int>();
            _addedLessonThemeIds = new List<(int, int)>();
            _courseDtoMock = CourseMockGetter.GetCourseDtoMock(1);
            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _courseDtoMock.Id = _courseRepository.AddCourse(_courseDtoMock);            
            _groupDtoMock.Course = _courseDtoMock;
            _groupDtoMock.Id = _groupRepository.AddGroup(_groupDtoMock);
            _addedCourseIds.Add(_courseDtoMock.Id);
            _addedGroupIds.Add(_groupDtoMock.Id);

        }

        [TestCase(2)]
        public void AddLessonPositiveTest(int mockId)
        {
            //Given
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            expected.Group = _groupDtoMock;
            expected.Id = _lessonRepository.AddLesson(expected);
            _addedLessonIds.Add(expected.Id);
            //When
            var actual = _lessonRepository.GetLessonById(expected.Id);
            //Then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(5)]
        public void AddLesson_Empty_NegativeTest(int mockId)
        {
            //Given
            var dto = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            dto.Group = _groupDtoMock;
            //When, Then
            try
            {
                var addedLessonId = _lessonRepository.AddLesson(dto);
                _addedLessonIds.Add(addedLessonId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddLesson_WithoutGroup_NegativeTest(int mockId)
        {
            //Given
            var dto = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            //When, Then
            try
            {
                var addedLessonId = _lessonRepository.AddLesson(dto);
                _addedLessonIds.Add(addedLessonId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddLesson_Null_NegativeTest()
        {
            //Given
            //When, Then
            try
            {
                var addedLessonId = _lessonRepository.AddLesson(null);
                _addedLessonIds.Add(addedLessonId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        [TestCase(2)]
        public void UpdateLessonPositiveTest(int mockId)
        {
            //Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            lessonDto.Group = _groupDtoMock;
            lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(lessonDto.Id);
            var expected = new LessonDto
            {
                Id = lessonDto.Id,
                Description = "Update description",
                Date = DateTime.Now.AddDays(-20)
               
            };
            expected.IsDeleted = true;
            _lessonRepository.UpdateLesson(expected);
            expected.IsDeleted = false;
            //When
            var actual = _lessonRepository.GetLessonById(lessonDto.Id);
            //Then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(5)]
        public void UpdateLesson_Empty_NegativeTest(int mockId)
        {
            //Given

            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(lessonDto.Id);
            var dto = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            dto.Group = _groupDtoMock;
            //When, Then
            try
            {
                _lessonRepository.UpdateLesson(dto);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void UpdateLesson_Null_NegativeTest()
        {
            //Given

            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(lessonDto.Id);
            //When, Then
            try
            {
                _lessonRepository.UpdateLesson(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2 })]
        public void GetLessonsByGroupIdPositiveTest(int[] lessonIdMock)
        {
            //Given
            var firstGroupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            firstGroupDto.Course = _courseDtoMock;          
            firstGroupDto.Id = _groupRepository.AddGroup(firstGroupDto);
            _addedGroupIds.Add(firstGroupDto.Id);
            var secondGroupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(2).Clone();
            secondGroupDto.Course = _courseDtoMock;
            secondGroupDto.Id = _groupRepository.AddGroup(secondGroupDto);
            _addedGroupIds.Add(secondGroupDto.Id);
            
            var firstExpected = new List<LessonDto>();
            for (int i = 0; i < lessonIdMock.Length; i++)
            {
                var lessonMock = LessonMockGetter.GetLessonDtoMock(lessonIdMock[i]);
                lessonMock.Group = firstGroupDto;
                var lessonId = _lessonRepository.AddLesson(lessonMock);
                lessonMock.Id = lessonId;
                _addedLessonIds.Add(lessonId);
                firstExpected.Add(lessonMock);
            }

            var secondExpected = new List<LessonDto>();
            for (int i = 0; i < lessonIdMock.Length; i++)
            {
                var lessonMock = LessonMockGetter.GetLessonDtoMock(lessonIdMock[i]);
                lessonMock.Group = secondGroupDto;
                var lessonId = _lessonRepository.AddLesson(lessonMock);
                lessonMock.Id = lessonId;
                _addedLessonIds.Add(lessonId);
                secondExpected.Add(lessonMock);
            }
            //When
            var actualFirst = _lessonRepository.GetLessonsByGroupId(firstGroupDto.Id);
            var actualSecond = _lessonRepository.GetLessonsByGroupId(secondGroupDto.Id);
            //Then
            CollectionAssert.AreEqual(firstExpected, actualFirst);
            CollectionAssert.AreEqual(secondExpected, actualSecond);
        }

        [TestCase(new int[] { 1, 2 })]
        public void GetLessonsByGroupId_OtherGroup_NegativeTest(int[] lessonIdMock)
        {
            //Given

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepository.AddGroup(groupDto);
            _addedGroupIds.Add(groupDto.Id);

            for (int i = 0; i < lessonIdMock.Length; i++)
            {
                var lessonMock = LessonMockGetter.GetLessonDtoMock(lessonIdMock[i]);
                lessonMock.Group = groupDto;
                var lessonId = _lessonRepository.AddLesson(lessonMock);
                lessonMock.Id = lessonId;
                _addedLessonIds.Add(lessonId);
            }
            //When

            var actual = _lessonRepository.GetLessonsByGroupId(-1);
            // Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestCase(1)]
        public void GetLessonByIdPositiveTest(int lessonMockId)
        {
            //Given
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(lessonMockId).Clone();
            expected.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(expected);
            expected.Id = addedLessonId;
            _addedLessonIds.Add(addedLessonId);
            //When
            var actual = _lessonRepository.GetLessonById(addedLessonId);
            //Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLessonById_NotExist_NegativeTest()
        {
            //Given
            var dto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            dto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(dto);
            dto.Id = addedLessonId;
            _addedLessonIds.Add(addedLessonId);
            //When
            var actual = _lessonRepository.GetLessonById(-1);
            //Then
            Assert.IsNull(actual);
        }

        [TestCase(1, true)]
        [TestCase(2, false)]
        public void DeleteOrRecoverLessonPositiveTest(int lessonMockId, bool isDeleted)
        {
            //Given
            var expected = (LessonDto)LessonMockGetter.GetLessonDtoMock(lessonMockId).Clone();
            expected.Group = _groupDtoMock;
            expected.Id = _lessonRepository.AddLesson(expected);
            _addedLessonIds.Add(expected.Id);
            expected.IsDeleted = isDeleted;
            var countRows = _lessonRepository.DeleteOrRecoverLesson(expected.Id, expected.IsDeleted);
            //When
            Assert.AreEqual(1, countRows);
            var actual = _lessonRepository.GetLessonById(expected.Id);
            //Then
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteOrRecoverLesson_NotExist_NegativeTest()
        {
            //Given
            //When
            var deletedRows = _lessonRepository.DeleteOrRecoverLesson(-1, true);

            //Then
            Assert.AreEqual(0, deletedRows);
        }
        [TestCase(new int[] { 1, 2 })]
        public void GetLessonByThemeIdPositiveTest(int[] lessonMockcId)
        {
            //Given
            var firstThemeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            firstThemeDto.Id = _courseRepository.AddTheme(firstThemeDto);
            _addedThemeIds.Add(firstThemeDto.Id);
            var secondThemeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(2).Clone();
            secondThemeDto.Id = _courseRepository.AddTheme(secondThemeDto);
            _addedThemeIds.Add(secondThemeDto.Id);

            var firstExpected = new List<LessonDto>();
            foreach(int id in lessonMockcId)
            {
                var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(id).Clone();
                lessonDto.Group = _groupDtoMock;
                lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
                _addedLessonIds.Add(lessonDto.Id);
                _lessonRepository.AddLessonTheme(lessonDto.Id, firstThemeDto.Id);
                _addedLessonThemeIds.Add((lessonDto.Id, firstThemeDto.Id));
                firstExpected.Add(lessonDto);
            }

            var secondExpected = new List<LessonDto>();
            for(int id = 3; id < 5; id++)
            {
                var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(id).Clone();
                lessonDto.Group = _groupDtoMock;
                lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
                _addedLessonIds.Add(lessonDto.Id);
                _lessonRepository.AddLessonTheme(lessonDto.Id, secondThemeDto.Id);
                _addedLessonThemeIds.Add((lessonDto.Id, secondThemeDto.Id));
                secondExpected.Add(lessonDto);
            }
            //When
            var actualFirst = _lessonRepository.GetLessonsByThemeId(firstThemeDto.Id);
            var actualSecond = _lessonRepository.GetLessonsByThemeId(secondThemeDto.Id);
            //Then
            CollectionAssert.AreEqual(firstExpected, actualFirst);
            CollectionAssert.AreEqual(secondExpected, actualSecond);
        }

        [TestCase(new int[] { 1, 2 })]
        public void GetLessonByThemeId_NotExistTheme_NegativeTest(int[] lessonMockcId)
        {
            //Given
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            themeDto.Id = _courseRepository.AddTheme(themeDto);
            _addedThemeIds.Add(themeDto.Id);

            foreach (int id in lessonMockcId)
            {
                var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(id).Clone();
                lessonDto.Group = _groupDtoMock;
                lessonDto.Id = _lessonRepository.AddLesson(lessonDto);
                _addedLessonIds.Add(lessonDto.Id);
                _lessonRepository.AddLessonTheme(lessonDto.Id, themeDto.Id);
                _addedLessonThemeIds.Add((lessonDto.Id, themeDto.Id));
            }
            //When
            var actual = _lessonRepository.GetLessonsByThemeId(-1);
            //Then
            Assert.AreEqual(0, actual.Count);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void AddLessonThemePositiveTest(int[] mockIds)
        {
            // Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(addedLessonId);
            lessonDto.Id = addedLessonId;

            // When
            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepository.AddTheme(themeDto);
                _addedThemeIds.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);

                _lessonRepository.AddLessonTheme(addedLessonId, addedThemeId);
                _addedLessonThemeIds.Add((addedLessonId, addedThemeId));
            }

            var actual = _lessonRepository.GetLessonById(addedLessonId).Themes;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AddLessonTheme_NotUnique_NegativeTest()
        {
            // Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(addedLessonId);
            lessonDto.Id = addedLessonId;

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepository.AddTheme(themeDto);
            _addedThemeIds.Add(addedThemeId);
            themeDto.Id = addedThemeId;
            // When

            _lessonRepository.AddLessonTheme(addedLessonId, addedThemeId);
            _addedLessonThemeIds.Add((addedLessonId, addedThemeId));
            try
            {
                _lessonRepository.AddLessonTheme(addedLessonId, addedThemeId);
                _addedLessonThemeIds.Add((addedLessonId, addedThemeId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddLessonTheme_NotExistLesson_NegativeTest()
        {
            // Given

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepository.AddTheme(themeDto);
            _addedThemeIds.Add(addedThemeId);
            themeDto.Id = addedThemeId;
            // When
            try
            {
                _lessonRepository.AddLessonTheme(-1, addedThemeId);
                _addedLessonThemeIds.Add((-1, addedThemeId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddLessonTheme_NotExistTheme_NegativeTest()
        {
            // Given

            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(addedLessonId);
            lessonDto.Id = addedLessonId;
            // When
            try
            {
                _lessonRepository.AddLessonTheme(addedLessonId, -1);
                _addedLessonThemeIds.Add((addedLessonId, -1));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(new int[] { 1, 2, 3 })]
        public void DeleteLessonThemePositiveTest(int[] mockIds)
        {
            // Given
            var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
            lessonDto.Group = _groupDtoMock;
            var addedLessonId = _lessonRepository.AddLesson(lessonDto);
            _addedLessonIds.Add(addedLessonId);
            lessonDto.Id = addedLessonId;
            // When
            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepository.AddTheme(themeDto);
                _addedThemeIds.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);

                _lessonRepository.AddLessonTheme(addedLessonId, addedThemeId);
                _addedLessonThemeIds.Add((addedLessonId, addedThemeId));
            }

            var toDeleteIdList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                themeDto.Name += "toDelete";
                var addedThemeId = _courseRepository.AddTheme(themeDto);
                _addedThemeIds.Add(addedThemeId);
                themeDto.Id = addedThemeId;

                _lessonRepository.AddLessonTheme(addedLessonId, addedThemeId);
                toDeleteIdList.Add((addedLessonId, addedThemeId));
            }
            var actual1 = _lessonRepository.GetLessonById(addedLessonId).Themes;
            // When
            toDeleteIdList.ForEach(themeTag =>
            {
                _lessonRepository.DeleteLessonTheme(themeTag.Item1, themeTag.Item2);
            });
            var actual = _lessonRepository.GetLessonById(addedLessonId).Themes;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TearDown]
        public void TearDownTest()
        {
            DeleteLessonThemes();
            DeleteThemes();
            DeleteLessons();
            DeleteGroups();         
            DeleteCourses();          
        }
        public void DeleteGroups()
        {
            foreach (int groupId in _addedGroupIds)
            {
                _groupRepository.HardDeleteGroup(groupId);
            }
        }
        public void DeleteLessons()
        {
            foreach (int lessonId in _addedLessonIds)
            {
                _lessonRepository.HardDeleteLesson(lessonId);
            }
        }
        public void DeleteCourses()
        {
            foreach (int courseId in _addedCourseIds)
            {
                _courseRepository.HardDeleteCourse(courseId);
            }
        }
        public void DeleteThemes()
        {
            foreach (int themeId in _addedThemeIds)
            {
                _courseRepository.HardDeleteTheme(themeId);
            }
        }
        public void DeleteLessonThemes()
        {
            foreach((int,int) lessonThemeId in _addedLessonThemeIds)
            {
                _lessonRepository.DeleteLessonTheme(lessonThemeId.Item1, lessonThemeId.Item2);
            }
        }
    }
}

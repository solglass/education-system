using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class HomeworkTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private ITagRepository _tagRepo;

        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _themeIdList;
        private List<int> _tagIdList;
        private List<(int, int)> _groupHomeworkList;
        private List<(int, int)> _themeHomeworkList;
        private List<(int, int)> _tagHomeworkList;

        private CourseDto _courseDtoMock;

        [SetUp]
        public void OneTimeSetUpTest()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _tagRepo = new TagRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _themeIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _tagIdList = new List<int>();
            _groupHomeworkList = new List<(int, int)>();
            _themeHomeworkList = new List<(int, int)>();
            _tagHomeworkList = new List<(int, int)>(); 

            _courseDtoMock = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_courseDtoMock);
            _courseDtoMock.Id = addedCourseId;
            _courseIdList.Add(addedCourseId);
        }

        [TestCase(1)]
        public void HomeworkAddPositiveTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Course = _courseDtoMock;

            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            Assert.Greater(addedHomeworkId, 0);

            _homeworkIdList.Add(addedHomeworkId);
            dto.Id = addedHomeworkId;

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId);

            //Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(4)]
        public void HomeworkAdd_EmptyHomework_NegativeTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Course = _courseDtoMock;
            //When, Then
            try
            {
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void HomeworkAdd_WithoutCourse_NegativeTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();

            //When, Then
            try
            {
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void HomeworkAdd_Null_NegativeTest()
        {
            //Given

            //When, Then
            try
            {
                var addedHomeworkId = _homeworkRepo.AddHomework(null);
                _homeworkIdList.Add(addedHomeworkId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void HomeworkUpdatePositiveTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);

            dto = new HomeworkDto
            {
                Id = addedHomeworkId,
                Description = "Homework Updated Test",
                StartDate = dto.StartDate.AddDays(2),
                DeadlineDate = dto.DeadlineDate.AddDays(2),
                IsOptional = false,
                Course = new CourseDto
                {
                    Id = _courseRepo.AddCourse(_courseDtoMock)
                }
            };
            _courseIdList.Add(dto.Course.Id);
            _homeworkRepo.UpdateHomework(dto);

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        [TestCase(4)]
        public void HomeworkUpdate_Empty_NegativeTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            dto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);

            dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            //When, Then
            try
            {
                _homeworkRepo.UpdateHomework(dto);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void HomeworkUpdate_Null_NegativeTest()
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            dto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);
            //When, Then
            try
            {
                _homeworkRepo.UpdateHomework(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void HomeworkDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);
            dto.Id = addedHomeworkId;
            dto.IsDeleted = isDeleted;

            //When
            var affectedRowsCount = _homeworkRepo.DeleteOrRecoverHomework(addedHomeworkId, isDeleted);

            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);

        }

        [Test]
        public void HomeworkDeleteOrRecover_NotExistHomework_NegativeTest()
        {
            //Given
            //When
            var deletedRows = _homeworkRepo.DeleteOrRecoverHomework(-1, true);

            //Then
            Assert.AreEqual(0, deletedRows);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void AddHomeworkThemePositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            }

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Themes;

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void AddHomeworkGroupPositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<GroupDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                var addedGroupId = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(addedGroupId);
                groupDto.Id = addedGroupId;
                expected.Add(groupDto);
                _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            }

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Groups;

            //Then
            Assert.AreEqual(expected.Count, actual.Count);
            for(int i = 0; i < expected.Count; ++i)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
            }
        }

        [Test]
        public void AddHomeworkTheme_NotExistHomework_NegativeTest()
        {
            //Given
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Theme(-1, addedThemeId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
       [Test]
        public void AddHomeworkGroup_NotExistHomework_NegativeTest()
        {
            //Given
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Theme(-1, addedGroupId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddHomeworkTheme_NotExistTheme_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, -1);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void AddHomeworkGroup_NotExistGroup_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, -1);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddHomeworkTheme_Unique_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
            _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void AddHomeworkGroup_Unique_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
            _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            //When, Then
            try
            {
                _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void DeleteHomeworkThemePositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<ThemeDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                expected.Add(themeDto);
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            }

            var toDeleteList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
                themeDto.Name += "toDelete";
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                toDeleteList.Add((addedHomeworkId, addedThemeId));
            }

            //When
            toDeleteList.ForEach((homeworkTheme) =>
            {
                _homeworkRepo.DeleteHomework_Theme(homeworkTheme.Item1, homeworkTheme.Item2);
            });

            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Themes;

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void DeleteHomeworkGroupPositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<GroupDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                var addedGroupId = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(addedGroupId);
                groupDto.Id = addedGroupId;
                expected.Add(groupDto);
                _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            }

            var toDeleteList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                var addedGroupId = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(addedGroupId);
                groupDto.Id = addedGroupId;
                _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, addedGroupId));

                toDeleteList.Add((addedHomeworkId, addedGroupId));
            }

            //When
            toDeleteList.ForEach((homeworkGroup) =>
            {
                _homeworkRepo.DeleteHomework_Group(homeworkGroup.Item1, homeworkGroup.Item2);
            });

            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Groups;

            //Then
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; ++i)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
            }

        }

        [Test]
        public void DeleteHomeworkTheme_NotExistTheme_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
            _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            //When
            var affectedRows = _homeworkRepo.DeleteHomework_Theme(addedHomeworkId, -1);

            //Then
            Assert.AreEqual(0, affectedRows);
        }
        [Test]
        public void DeleteHomeworkGroup_NotExistGroup_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
            _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            //When
            var affectedRows = _homeworkRepo.DeleteHomework_Group(addedHomeworkId, -1);

            //Then
            Assert.AreEqual(0, affectedRows);
        }

        [Test]
        public void DeleteHomeworkTheme_NotExistHomework_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
            _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            //When
            var affectedRows = _homeworkRepo.DeleteHomework_Theme(-1, addedThemeId);

            //Then
            Assert.AreEqual(0, affectedRows);
        }
        [Test]
        public void DeleteHomeworkGroup_NotExistHomework_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
            _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            //When
            var affectedRows = _homeworkRepo.DeleteHomework_Group(-1, addedGroupId);

            //Then
            Assert.AreEqual(0, affectedRows);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void AddHomeworkTagPositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedTagId = _tagRepo.TagAdd(tagDto);
                _tagIdList.Add(addedTagId);
                tagDto.Id = addedTagId;
                expected.Add(tagDto);
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            }

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Tags;

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AddHomeworkTag_NotExistHomework_NegativeTest()
        {
            //Given
            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);
            tagDto.Id = addedTagId;

            //When, Then
            try
            {
                _homeworkRepo.HomeworkTagAdd(-1, addedTagId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddHomeworkTag_NotExistTag_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            //When, Then
            try
            {
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, -1);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddHomeworkTag_Unique_NegativeTest()
        {
            //Given
            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
            _tagHomeworkList.Add((addedHomeworkId, addedTagId));

            //When, Then
            try
            {
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void DeleteHomeworkTagPositiveTest(int[] mockIds)
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);
            homeworkDto.Id = addedHomeworkId;

            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedTagId = _tagRepo.TagAdd(tagDto);
                _tagIdList.Add(addedTagId);
                tagDto.Id = addedTagId;
                expected.Add(tagDto);
                var addedThemeHomework = _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            }

            var toDeleteList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                tagDto.Name += "toDelete";
                var addedTagId = _tagRepo.TagAdd(tagDto);
                _tagIdList.Add(addedTagId);
                tagDto.Id = addedTagId;
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                toDeleteList.Add((addedHomeworkId, addedTagId));
            }

            //When
            toDeleteList.ForEach((homeworkTheme) =>
            {
                _homeworkRepo.HomeworkTagDelete(homeworkTheme.Item1, homeworkTheme.Item2);
            });

            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId).Tags;

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void DeleteHomeworkTag_NotExistTag_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);

            _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
            _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            //When
            var affectedRows = _homeworkRepo.HomeworkTagDelete(addedHomeworkId, -1);

            //Then
            Assert.AreEqual(0, affectedRows);
        }

        [Test]
        public void DeleteHomeworkTag_NotExistHomework_NegativeTest()
        {
            //Given
            var homeworkDto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            homeworkDto.Course = _courseDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(homeworkDto);
            _homeworkIdList.Add(addedHomeworkId);

            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);

            _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
            _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            //When
            var affectedRows = _homeworkRepo.HomeworkTagDelete(-1, addedTagId);

            //Then
            Assert.AreEqual(0, affectedRows);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void SearchHomeworksByCourseIdPositiveTest(int[] mockIds)
        {
            //Given
            var courseDto = _courseDtoMock;
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
            }

            var secondCourseDto = _courseDtoMock;
            var secondAddedCourseId = _courseRepo.AddCourse(secondCourseDto);
            _courseIdList.Add(secondAddedCourseId);
            secondCourseDto.Id = secondAddedCourseId;


            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = secondCourseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
            }

            //When
            var actual = _homeworkRepo.SearchHomeworks(addedCourseId, null, null, null);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void SearchHomeworksByGroupIdPositiveTest(int[] mockIds)
        {
            //Given
            var courseDto = (CourseDto)_courseDtoMock.Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = courseDto;
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);
            groupDto.Id = addedGroupId;

            var secondGroupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            secondGroupDto.Course = courseDto;
            var secondAddedGroupId = _groupRepo.AddGroup(secondGroupDto);
            _groupIdList.Add(secondAddedGroupId);
            secondGroupDto.Id = secondAddedGroupId;

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
                _homeworkRepo.AddHomework_Group(addedHomeworkId, addedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, addedGroupId));
            }

            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                _homeworkRepo.AddHomework_Group(addedHomeworkId, secondAddedGroupId);
                _groupHomeworkList.Add((addedHomeworkId, secondAddedGroupId));
            }
            //When
            var actual = _homeworkRepo.SearchHomeworks(null, addedGroupId, null, null);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
           [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void SearchHomeworksByThemeIdPositiveTest(int[] mockIds)
        {
            //Given
            var courseDto = (CourseDto)_courseDtoMock.Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);
            themeDto.Name += "secondTheme";
            var secondAddedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(secondAddedThemeId);

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                _themeHomeworkList.Add((addedHomeworkId, addedThemeId));
            }

            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                _homeworkRepo.AddHomework_Theme(addedHomeworkId, secondAddedThemeId);
                _themeHomeworkList.Add((addedHomeworkId, secondAddedThemeId));
            }
            //When
            var actual = _homeworkRepo.SearchHomeworks(null, null, addedThemeId, null);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void SearchHomeworksByTagIdPositiveTest(int[] mockIds)
        {
            //Given
            var courseDto = (CourseDto)_courseDtoMock.Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);
            tagDto.Name += "secondTag";
            var secondAddedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(secondAddedTagId);

            // должны быть хоумворки с другими тегами
            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            }

            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Course = courseDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                _homeworkRepo.HomeworkTagAdd(addedHomeworkId, secondAddedTagId);
                _tagHomeworkList.Add((addedHomeworkId, secondAddedTagId));
            }
            //When
            var actual = _homeworkRepo.SearchHomeworks(null, null, null, addedTagId);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SearchHomeworks_Null_NegativeTest()
        {
            //Given

            var courseDto = (CourseDto)_courseDtoMock.Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);

            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            dto.Course = courseDto;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);
            dto.Id = addedHomeworkId;

            var addedThemeHomework = _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
            _themeHomeworkList.Add((addedHomeworkId, addedThemeId));

            var addedTagHomework = _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
            _tagHomeworkList.Add((addedHomeworkId, addedTagId));

            //When, Then
            try
            {
                _homeworkRepo.SearchHomeworks(null, null, null, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TearDown]
        public void TearDowTest()
        {
            DeteleGroupHomeworks();
            DeleteThemeHomeworks();
            DeteleTagHomeworks();
            DeleteHomeworks();
            DeleteGroups();
            DeleteCourse();
            DeleteThemes();
            DeleteTags();
        }


        private void DeleteTags()
        {
            foreach (int tagId in _tagIdList)
            {
                _tagRepo.TagDelete(tagId);
            }
        }

        private void DeteleGroupHomeworks()
        {
            foreach (var groupHomework in _groupHomeworkList)
            {
                _homeworkRepo.DeleteHomework_Group(groupHomework.Item1, groupHomework.Item2);
            }
        }

        private void DeteleTagHomeworks()
        {
            foreach (var tagHomework in _tagHomeworkList)
            {
                _homeworkRepo.HomeworkTagDelete(tagHomework.Item1, tagHomework.Item2);
            }
        }

        private void DeleteThemeHomeworks()
        {
            foreach (var theneHomeworkPair in _themeHomeworkList)
            {
                _homeworkRepo.DeleteHomework_Theme(theneHomeworkPair.Item1, theneHomeworkPair.Item2);
            }
        }

        private void DeleteThemes()
        {
            foreach (int themeId in _themeIdList)
            {
               _courseRepo.HardDeleteTheme(themeId);
            }
        }

        private void DeleteHomeworks()
        {
            foreach (int homeworkId in _homeworkIdList)
            {
                _homeworkRepo.HardDeleteHomework(homeworkId);
            }
        }

        private void DeleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }
        private void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }
        
    }
}

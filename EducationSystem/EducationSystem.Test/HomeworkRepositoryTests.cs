using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkRepositoryTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IUserRepository _userRepo;
        private ITagRepository _tagRepo;

        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _themeIdList;
        private List<int> _tagIdList;
        private List<(int, int)> _themeHomeworkList;
        private List<(int, int)> _tagHomeworkList;

        private GroupDto _groupDtoMock;


        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _tagRepo = new TagRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _themeIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _tagIdList = new List<int>();
            _themeHomeworkList = new List<(int, int)>();
            _tagHomeworkList = new List<(int, int)>(); 

            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_groupDtoMock.Course);
            _courseIdList.Add(addedCourseId);
            _groupDtoMock.Course.Id = addedCourseId;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;
        }

        [TestCase(1)]
        public void HomeworkAddPositiveTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Group = _groupDtoMock;

            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            Assert.Greater(addedHomeworkId, 0);

            _homeworkIdList.Add(addedHomeworkId);
            dto.Id = addedHomeworkId;

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        [TestCase(1)]
        public void HomeworkUpdatePositiveTest(int mockId)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Group = _groupDtoMock;
            var addedHomeworkId = _homeworkRepo.AddHomework(dto);
            _homeworkIdList.Add(addedHomeworkId);

            dto = new HomeworkDto
            {
                Id = addedHomeworkId,
                Description = "Homework Updated Test",
                StartDate = DateTime.Now.AddDays(1),
                DeadlineDate = DateTime.Now.AddDays(2),
                IsOptional = false
            };
            _homeworkRepo.UpdateHomework(dto);

            //When
            var actual = _homeworkRepo.GetHomeworkById(addedHomeworkId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void HomeworkDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
            dto.Group = _groupDtoMock;
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

        [TestCase(new int[] { 1,2,3})]
        public void SearchHomeworksByGroupIdPositiveTest(int[] mockIds)
        {
            //Given
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);
            groupDto.Id = addedGroupId;

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Group = groupDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
            }

            //When

            var actual = _homeworkRepo.SearchHomeworks(groupDto.Id, null, null);

            //Then
            CollectionAssert.AreEqual(expected, actual);

        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void SearchHomeworksByThemeIdPositiveTest(int[] mockIds)
        {
            //Given
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
            var addedThemeId = _courseRepo.AddTheme(themeDto);
            _themeIdList.Add(addedThemeId);

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Group = groupDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
                var addedThemeHomework = _homeworkRepo.AddHomework_Theme(addedHomeworkId, addedThemeId);
                _themeHomeworkList.Add((addedHomeworkId,addedThemeId));
            }

            //When

            var actual = _homeworkRepo.SearchHomeworks(null, addedThemeId, null);

            //Then
            CollectionAssert.AreEqual(expected, actual);

        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void SearchHomeworksByTagIdPositiveTest(int[] mockIds)
        {
            //Given
            var groupDto = (GroupDto)_groupDtoMock.Clone();
            var addedGroupId = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(addedGroupId);

            var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(1).Clone();
            var addedTagId = _tagRepo.TagAdd(tagDto);
            _tagIdList.Add(addedTagId);

            var expected = new List<HomeworkDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockIds[i]).Clone();
                dto.Group = groupDto;
                var addedHomeworkId = _homeworkRepo.AddHomework(dto);
                _homeworkIdList.Add(addedHomeworkId);
                dto.Id = addedHomeworkId;
                expected.Add(dto);
                var addedTagHomework = _homeworkRepo.HomeworkTagAdd(addedHomeworkId, addedTagId);
                _tagHomeworkList.Add((addedHomeworkId, addedTagId));
            }

            //When

            var actual = _homeworkRepo.SearchHomeworks(null, null, addedTagId);

            //Then
            CollectionAssert.AreEqual(expected, actual);

        }

        [OneTimeTearDown]
        public void TearDowTest()
        {
            DeleteThemeHomeworks();
            DeteleTagHomeworks();
            DeleteHomeworks();
            DeleteGroups();
            DeleteCourse();
            DeleteThemes();
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

        public void DeleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.HardDeleteGroup(groupId);
            }
        }
        public void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }
        
    }
}

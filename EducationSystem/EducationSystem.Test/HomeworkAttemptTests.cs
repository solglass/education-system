using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkAttemptTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IUserRepository _userRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;

        private UserDto _userDtoMock;
        private GroupDto _groupDtoMock;
        private HomeworkDto _homeworkDtoMock;

        private List<int> _homeworkAttemptIdList; 
        private List<int> _homeworkIdList; 
        private List<int> _userIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _groupRepo = new GroupRepository(_options);
            _courseRepo = new CourseRepository(_options);

            _homeworkAttemptIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _userIdList = new List<int>();
            _groupIdList = new List<int>();
            _courseIdList = new List<int>();

            _userDtoMock = UserMockGetter.GetUserDtoMock(1);
            var addedUserId = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(addedUserId);
            _userDtoMock.Id = addedUserId;

            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_groupDtoMock.Course);
            _courseIdList.Add(addedCourseId);
            _groupDtoMock.Course.Id = addedCourseId;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;

            _homeworkDtoMock = HomeworkMockGetter.GetHomeworkDtoMock(1);
            _homeworkDtoMock.Group = _groupDtoMock;
            var addedhomeworkId = _homeworkRepo.AddHomework(_homeworkDtoMock);
            _homeworkIdList.Add(addedhomeworkId);
            _homeworkDtoMock.Id = addedhomeworkId;
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddHomeworkAttemptPositiveTest(int mockId)
        {
            // Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
            Assert.Greater(addedHomeworkAttemptId, 0);

            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            dto.Id = addedHomeworkAttemptId;
            

            // When
            var actual = _homeworkRepo.GetHomeworkAttemptById(addedHomeworkAttemptId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        public void UpdateHomeworkAttemptPositiveTest(int mockId, int updateMockId)
        {
            // Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);

            dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(updateMockId).Clone();
            dto.Id = addedHomeworkAttemptId;
            var affectedRowsCount = _homeworkRepo.UpdateHomeworkAttempt(dto);


            // When
            var actual = _homeworkRepo.GetHomeworkAttemptById(addedHomeworkAttemptId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        //[TestCase(new int[] { 1, 2, 3 })]
        //[TestCase(new int[] { 3, 2, 1 })]
        //[TestCase(new int[] { 1, 2, 3, 2, 1, 3, 2, 1 })]
        //public void GetThemesPositiveTest(int[] mockIds)
        //{
        //    // Given
        //    var expected = _homeworkRepo.GetThemes();
        //    for (int i = 0; i < mockIds.Length; i++)
        //    {
        //        var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
        //        var addedHomeworkAttemptId = _homeworkRepo.AddTheme(dto);
        //        _homeworkIdList.Add(addedHomeworkAttemptId);
        //        dto.Id = addedHomeworkAttemptId;
        //        expected.Add(dto);
        //    }

        //    // When
        //    var actual = _homeworkRepo.GetThemes();

        //    // Then
        //    CollectionAssert.AreEqual(expected, actual);
        //}

        //[TestCase(1, true)]
        //[TestCase(1, false)]
        //[TestCase(2, true)]
        //[TestCase(3, false)]
        //public void ThemeDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        //{

        //    //Given
        //    var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockId).Clone();
        //    var addedHomeworkAttemptId = _homeworkRepo.AddTheme(dto);
        //    _homeworkIdList.Add(addedHomeworkAttemptId);
        //    dto.Id = addedHomeworkAttemptId;
        //    dto.IsDeleted = isDeleted;

        //    // When
        //    var affectedRowsCount = _homeworkRepo.DeleteOrRecoverTheme(addedHomeworkAttemptId, isDeleted);

        //    var actual = _homeworkRepo.GetThemeById(addedHomeworkAttemptId);

        //    // Then
        //    Assert.AreEqual(1, affectedRowsCount);
        //    Assert.AreEqual(dto, actual);
        //}

        //[TestCase(new int[] { 1, 2, 3 })]
        //[TestCase(new int[] { 3, 2, 1 })]
        //[TestCase(new int[] { 1, 2, 3, 2, 1, 3, 2, 1 })]
        //public void GetThemesByCourseIdPositiveTest(int[] mockIds)
        //{
        //    // Given
        //    var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1);
        //    var addedCourseId = _homeworkRepo.AddCourse(courseDto);
        //    _courseIdList.Add(addedCourseId);

        //    var expected = new List<ThemeDto>();
        //    for (int i = 0; i < mockIds.Length; i++)
        //    {
        //        var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
        //        var addedHomeworkAttemptId = _homeworkRepo.AddTheme(dto);
        //        _homeworkIdList.Add(addedHomeworkAttemptId);
        //        dto.Id = addedHomeworkAttemptId;
        //        expected.Add(dto);

        //        _homeworkRepo.AddCourse_Theme(addedCourseId, addedHomeworkAttemptId);
        //        _courseThemeList.Add((addedCourseId, addedHomeworkAttemptId));
        //    }

        //    // When
        //    var actual = _homeworkRepo.GetThemesByCourseId(addedCourseId);

        //    // Then
        //    CollectionAssert.AreEqual(expected, actual);
        //}

        //[TestCase(new int[] { 1, 2, 3 })]
        //[TestCase(new int[] { 3, 2, 1 })]
        //[TestCase(new int[] { 1, 2, 3, 2, 1, 3, 2, 1 })]
        //public void GetUncoveredThemesByGroupIdPositiveTest(int[] mockIds)
        //{
        //    // Given
        //    var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1);
        //    var addedCourseId = _homeworkRepo.AddCourse(courseDto);
        //    _courseIdList.Add(addedCourseId);
        //    courseDto.Id = addedCourseId;

        //    var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1);
        //    groupDto.Course = courseDto;
        //    var addedGroupId = _groupRepo.AddGroup(groupDto);
        //    _groupIdList.Add(addedGroupId);
        //    groupDto.Id = addedGroupId;

        //    var lessonDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(1).Clone();
        //    lessonDto.Date = DateTime.Now.AddDays(10);
        //    lessonDto.Group = groupDto;
        //    var addedLessonId = _lessonRepo.AddLesson(lessonDto);
        //    _lessonIdList.Add(addedLessonId);

        //    var expected = new List<ThemeDto>();
        //    for (int i = 0; i < mockIds.Length; i++)
        //    {
        //        var dto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(mockIds[i]).Clone();
        //        var addedHomeworkAttemptId = _homeworkRepo.AddTheme(dto);
        //        _homeworkIdList.Add(addedHomeworkAttemptId);
        //        dto.Id = addedHomeworkAttemptId;
        //        expected.Add(dto);

        //        _homeworkRepo.AddCourse_Theme(addedCourseId, addedHomeworkAttemptId);
        //        _courseThemeList.Add((addedCourseId, addedHomeworkAttemptId));

        //        _lessonRepo.AddLessonTheme(addedLessonId, addedHomeworkAttemptId);
        //        _lessonThemeList.Add((addedLessonId, addedHomeworkAttemptId));
        //    }

        //    // When
        //    var actual = _homeworkRepo.GetUncoveredThemesByGroupId(addedGroupId);

        //    // Then
        //    CollectionAssert.AreEqual(expected, actual);
        //}

        [OneTimeTearDown]
        public void TestTearDown()
        {
            DeleteHomeworkAttempt();
            DeleteHomework();
            DeleteGroups();
            DeleteCourse();
            DeleteUser();
        }

        private void DeleteUser()
        {
            foreach (var userId in _userIdList)
            {
                _userRepo.HardDeleteUser(userId);
            }
        }

        public void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }

        public void DeleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.HardDeleteGroup(groupId);
            }
        }

        private void DeleteHomework()
        {
            foreach (var homeworkId in _homeworkIdList)
            {
                _homeworkRepo.HardDeleteHomework(homeworkId);
            }
        }

        private void DeleteHomeworkAttempt()
        {
            foreach (var hwAttemptId in _homeworkAttemptIdList)
            {
                _homeworkRepo.HardDeleteHomeworkAttempt(hwAttemptId);
            }
        }
    }
}

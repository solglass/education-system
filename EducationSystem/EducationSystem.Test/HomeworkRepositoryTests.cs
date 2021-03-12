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
        private HomeworkRepository _homeworkRepo;
        private GroupRepository _groupRepo;
        private CourseRepository _courseRepo;
        private UserRepository _userRepo;

        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _userIdList;
        private List<int> _homeworkAttemptIdList;
        private List<int> _homeworkAttemptStatusIdList;

        private GroupDto _groupDtoMock;


        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _courseRepo = new CourseRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _userIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _homeworkAttemptIdList = new List<int>();
            _homeworkAttemptStatusIdList = new List<int>();

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

        //[Test, Order(2)]
        //public void TestUpdateHomework()
        //{
        //    _homeworkFromDb = _homeworkRepo.GetHomeworks();
        //    HomeworkDto homework;
        //    foreach (int homeworkId in _homeworkIdList)
        //    {
        //        homework = _homeworkRepo.GetHomeworkById(homeworkId);
        //        homework.Description = $"New Description {homeworkId}";
        //        _homeworkRepo.UpdateHomework(homework);
        //        if (!homework.Equals(_homeworkRepo.GetHomeworkById(homeworkId)))
        //        {
        //            Assert.Fail();
        //        }
        //    }
        //    Assert.Pass();
        //}
        //[Test, Order(3)]
        //public void TestAddHomeworkAttempt()
        //{
        //    _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
        //    HomeworkAttemptDto homeworkAttempt;
        //    for (int i = 2; i < 4; ++i)
        //    {
        //        homeworkAttempt = GetHomeworkAttemptMock(i);
        //        _homeworkAttemptFromDb.Add(homeworkAttempt);
        //        _homeworkAttemptIdList.Add(_homeworkRepo.AddHomeworkAttempt(homeworkAttempt));
        //    }



        //    int lastIndex = 0;

        //    for (int i = _homeworkAttemptFromDb.Count - _homeworkAttemptIdList.Count; i < _homeworkAttemptFromDb.Count; ++i)
        //    {
        //        HomeworkAttemptDto homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttemptById(_homeworkAttemptIdList[lastIndex]);
        //        if (!_homeworkAttemptFromDb[i].Equals(homeworkAttemptFromDb))
        //        {
        //            Assert.Fail();
        //        }
        //        ++lastIndex;


        //    }

        //}

        //[Test, Order(4)]
        //public void TestUpdateHomeworkAttempt()
        //{
        //    _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
        //    HomeworkAttemptDto homeworkAttempt;
        //    foreach (int homeworkAttemptId in _homeworkAttemptIdList)
        //    {
        //        homeworkAttempt = _homeworkRepo.GetHomeworkAttemptById(homeworkAttemptId);
        //        homeworkAttempt.Comment = $"New Comment {homeworkAttemptId}";
        //        _homeworkRepo.UpdateHomeworkAttempt(homeworkAttempt);
        //        if (!homeworkAttempt.Equals(_homeworkRepo.GetHomeworkAttemptById(homeworkAttemptId)))
        //        {
        //            Assert.Fail();
        //        }
        //    }
        //    Assert.Pass();
        //}

        //[Test, Order(5)]
        //public void TestDeleteHomeworkAttempt()
        //{
        //    _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
        //    HomeworkAttemptDto deleted;
        //    foreach (int homeworkAttemptId in _homeworkAttemptIdList)
        //    {
        //        _homeworkRepo.DeleteHomeworkAttempt(homeworkAttemptId);
        //        deleted = _homeworkRepo.GetHomeworkAttemptById(homeworkAttemptId);
        //        List<HomeworkAttemptDto> newHomeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();

        //        if (_homeworkAttemptFromDb.Count == newHomeworkAttemptFromDb.Count)
        //        {

        //            Assert.Fail("Nothing was deleted");
        //        }
        //        else
        //        {
        //            _homeworkAttemptFromDb = newHomeworkAttemptFromDb;
        //        }
        //        if (deleted != null)
        //        {
        //            Assert.Fail("Something wrong was deleted");
        //        }
        //    }
        //    Assert.Pass();

        //}

        //[Test, Order(6)]
        //public void TestDeleteHomework()
        //{
        //    _homeworkFromDb = _homeworkRepo.GetHomeworks();
        //    HomeworkDto deleted;
        //    foreach (int homeworkId in _homeworkIdList)
        //    {
        //        _homeworkRepo.DeleteHomework(homeworkId);
        //        deleted = _homeworkRepo.GetHomeworkById(homeworkId);
        //        List<HomeworkDto> newHomeworkFromDb = _homeworkRepo.GetHomeworks();
        //        if (_homeworkFromDb.Count == newHomeworkFromDb.Count)
        //        {
        //            Assert.Fail("Nothing was deleted");
        //        }
        //        else
        //        {
        //            _homeworkFromDb = newHomeworkFromDb;
        //        }
        //        if (deleted != null)
        //        {
        //            Assert.Fail("Something wrong was deleted");
        //        }
        //    }
        //    Assert.Pass();

        //}
        [OneTimeTearDown]
        public void TearDowTest()
        {
            DeleteHomeworks();
            DeleteGroups();
            DeleteCourse();
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
        //public void DeleteAttemptStatus()
        //{
        //    foreach (int homeworkAttemptStatusId in _homeworkAttemptStatusIdList)
        //    {
        //        _homeworkRepo.DeleteHomeworkAttemptStatus(homeworkAttemptStatusId);
        //    }
        //}
        //public void DeleteUsers()
        //{
        //    foreach (int userId in _userIdList)
        //    {
        //        _userRepo.DeleteUser(userId);
        //    }
        //}
        //public HomeworkAttemptStatusDto GetHomeworkAttemptStatusMock(int n)
        //{
        //    HomeworkAttemptStatusDto result = new HomeworkAttemptStatusDto();
        //    switch (n)
        //    {
        //        case 1:
        //            return result;
        //        case 2:
        //            result = (new HomeworkAttemptStatusDto { Name = "Test HomeworkAttemptStatusDto 1" });
        //            return result;
        //        case 3:
        //            result = (new HomeworkAttemptStatusDto { Name = "Test HomeworkAttemptStatusDto 2" });
        //            return result;
        //        default:
        //            return result;
        //    }
        //}
        //public HomeworkAttemptDto GetHomeworkAttemptMock(int n)
        //{
        //    HomeworkAttemptDto result = new HomeworkAttemptDto();

        //    switch (n)
        //    {

        //        case 1:
        //            return result;
        //        case 2:
        //            result = (new HomeworkAttemptDto
        //            {
        //                Comment = "Test comment 1",
        //                HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
        //                IsDeleted = false
        //            });

        //            UserDto author = GetUserMock(n);
        //            author.Login += _userIdList.Count.ToString();
        //            author.Email += _userIdList.Count.ToString();
        //            _userIdList.Add(_userRepo.AddUser(author));
        //            result.Author = author;
        //            result.Author.Id = _userIdList[_userIdList.Count - 1];

        //            HomeworkDto homework = GetHomeworkMock(n);
        //            _homeworkIdList.Add(_homeworkRepo.AddHomework(homework));
        //            result.Homework = homework;
        //            result.Homework.Id = _homeworkIdList[_homeworkIdList.Count - 1];

        //            HomeworkAttemptStatusDto homeworkAttemptStatus = GetHomeworkAttemptStatusMock(n);

        //            homeworkAttemptStatus.Name += _homeworkAttemptStatusIdList.Count.ToString();

        //            _homeworkAttemptStatusIdList.Add(_homeworkRepo.AddHomeworkAttemptStatus(homeworkAttemptStatus));

        //            result.HomeworkAttemptStatus = homeworkAttemptStatus;
        //            result.HomeworkAttemptStatus.Id = _homeworkAttemptStatusIdList[_homeworkAttemptStatusIdList.Count - 1];

        //            return result;
        //        case 3:
        //            result = (new HomeworkAttemptDto
        //            {
        //                Comment = "Test comment 1",
        //                HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 2" },
        //                IsDeleted = false
        //            });
        //            author = GetUserMock(n);
        //            author.Login += _userIdList.Count.ToString();
        //            author.Email += _userIdList.Count.ToString();
        //            _userIdList.Add(_userRepo.AddUser(author));
        //            result.Author = author;
        //            result.Author.Id = _userIdList[_userIdList.Count - 1];

        //            homework = GetHomeworkMock(n);
        //            _homeworkIdList.Add(_homeworkRepo.AddHomework(homework));
        //            result.Homework = homework;
        //            result.Homework.Id = _homeworkIdList[_homeworkIdList.Count - 1];

        //            homeworkAttemptStatus = GetHomeworkAttemptStatusMock(n);
        //            homeworkAttemptStatus.Name += _homeworkAttemptStatusIdList.Count.ToString();
        //            _homeworkAttemptStatusIdList.Add(_homeworkRepo.AddHomeworkAttemptStatus(homeworkAttemptStatus));
        //            result.HomeworkAttemptStatus = homeworkAttemptStatus;
        //            result.HomeworkAttemptStatus.Id = _homeworkAttemptStatusIdList[_homeworkAttemptStatusIdList.Count - 1];

        //            return result;
        //        default:
        //            return result;
        //    }
        //}

        //public UserDto GetUserMock(int n)
        //{
        //    UserDto result = new UserDto();
        //    switch (n)
        //    {
        //        case 1:
        //            return result;
        //        case 2:
        //            result = (new UserDto
        //            {
        //                FirstName = "Петр",
        //                LastName = "Петров",
        //                BirthDate = new DateTime(1980, 3, 12),
        //                Login = "Petr01",
        //                Password = "qqq123",
        //                Phone = "89825553535",
        //                UserPic = "ddsa",
        //                Email = "Petr.Petrov@mail.ru",
        //                IsDeleted = false
        //            });
        //            return result;
        //        case 3:
        //            result = (new UserDto
        //            {
        //                FirstName = "Вася",
        //                LastName = "Васильев",
        //                BirthDate = new DateTime(1980, 7, 12),
        //                Login = "Vasya01",
        //                Password = "qqq123",
        //                Phone = "8982543535",
        //                UserPic = "ddsasda",
        //                Email = "Vasya.Vaskin@mail.ru",
        //                IsDeleted = false
        //            });
        //            return result;
        //        case 4:

        //            result = (new UserDto
        //            {
        //                FirstName = "Максим",
        //                LastName = "Максимов",
        //                BirthDate = new DateTime(1982, 1, 11),
        //                Login = "Max01",
        //                Password = "qqq123",
        //                Phone = "8982552535",
        //                UserPic = "ddsa",
        //                Email = "Max@mail.ru",
        //                IsDeleted = false
        //            });
        //            return result;
        //        default:
        //            return result;
        //    }
        //}
        //public HomeworkDto GetHomeworkMock(int n)
        //{
        //    HomeworkDto result = new HomeworkDto();
        //    switch (n)
        //    {
        //        case 1:
        //            result = new HomeworkDto() { Description = "Test case 1", StartDate = new DateTime(2021, 1, 5), DeadlineDate = new DateTime(2021, 1, 11), IsOptional = true };
        //            result.Group = GetGroupMock(n);
        //            _groupIdList.Add(_groupRepo.AddGroup(result.Group));
        //            result.Group.Id = _groupIdList[_groupIdList.Count - 1];

        //            result.Tags = new List<TagDto>();
        //            result.Themes = new List<ThemeDto>();
        //            result.HomeworkAttempts = new List<HomeworkAttemptDto>();

        //            return result;
        //        case 2:
        //            result = new HomeworkDto() { Description = "Test case 2", StartDate = new DateTime(2021, 1, 12), DeadlineDate = new DateTime(2021, 1, 19), IsOptional = true };
        //            result.Group = GetGroupMock(n);
        //            _groupIdList.Add(_groupRepo.AddGroup(result.Group));
        //            result.Group.Id = _groupIdList[_groupIdList.Count - 1];


        //            result.Tags = new List<TagDto>();
        //            result.Themes = new List<ThemeDto>();
        //            result.HomeworkAttempts = new List<HomeworkAttemptDto>();

        //            return result;
        //        case 3:
        //            result = new HomeworkDto() { Description = "Test case 3", StartDate = new DateTime(2021, 1, 20), DeadlineDate = new DateTime(2021, 1, 25), IsOptional = false };
        //            result.Group = GetGroupMock(n);
        //            _groupIdList.Add(_groupRepo.AddGroup(result.Group));
        //            result.Group.Id = _groupIdList[_groupIdList.Count - 1];


        //            result.Tags = new List<TagDto>();
        //            result.Themes = new List<ThemeDto>();
        //            result.HomeworkAttempts = new List<HomeworkAttemptDto>();

        //            return result;
        //        default:
        //            return result;
        //    }
        //}

        //public CourseDto GetCourseMock(int n)
        //{
        //    CourseDto course = new CourseDto();
        //    switch (n)
        //    {
        //        case 1:
        //            course = new CourseDto() { Name = "TestCourseCase 1", Description = "Test case 1", Duration = 1 };
        //            return course;
        //        case 2:
        //            course = new CourseDto() { Name = "TestCourseCase 2", Description = "Test case 2", Duration = 2 };
        //            return course;
        //        case 3:
        //            course = new CourseDto() { Name = "TestCourseCase 3", Description = "Test case 3", Duration = 3 };
        //            return course;
        //        case 4:
        //            course = new CourseDto() { Name = "TestCourseCase 4", Description = "Test case 4", Duration = 4 };
        //            return course;
        //        default:
        //            return course;
        //    }
        //}
        //public GroupDto GetGroupMock(int n)
        //{
        //    GroupDto groups = new GroupDto();
        //    switch (n)
        //    {
        //        case 1:
        //            return groups;
        //        case 2:
        //            CourseDto course = GetCourseMock(n);
        //            _courseIdList.Add(_courseRepo.AddCourse(course));
        //            course.Id = _courseIdList[_courseIdList.Count - 1];

        //            groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = course, StartDate = new System.DateTime(2020, 10, 12) });
        //            return groups;
        //        case 3:
        //            course = GetCourseMock(n);
        //            _courseIdList.Add(_courseRepo.AddCourse(course));
        //            course.Id = _courseIdList[_courseIdList.Count - 1];

        //            groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = course, StartDate = new System.DateTime(2020, 10, 12) });
        //            return groups;
        //        case 4:
        //            course = GetCourseMock(n);
        //            _courseIdList.Add(_courseRepo.AddCourse(course));
        //            course.Id = _courseIdList[_courseIdList.Count - 1];

        //            groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = course.Id }, Course = course, StartDate = new System.DateTime(2021, 10, 12) });

        //            return groups;
        //        default:
        //            return groups;
        //    }
        //}
    }
}

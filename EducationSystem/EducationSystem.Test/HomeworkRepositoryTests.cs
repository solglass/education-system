using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkRepositoryTests
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

        private HomeworkDto _expectedHomework;
        private List<HomeworkDto> _homeworkFromDb;
        private List<HomeworkAttemptDto> _homeworkAttemptFromDb;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _groupRepo = new GroupRepository();
            _homeworkRepo = new HomeworkRepository();
            _userRepo = new UserRepository();
            _courseRepo = new CourseRepository();

            _homeworkFromDb = new List<HomeworkDto>();
            _homeworkAttemptFromDb = new List<HomeworkAttemptDto>();
            _expectedHomework = new HomeworkDto();

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _userIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _homeworkAttemptIdList = new List<int>();
            _homeworkAttemptStatusIdList = new List<int>();

        }

        [Test, Order(1)]
        public void TestAddHomework()
        {
            _homeworkFromDb = _homeworkRepo.GetHomeworks();
            HomeworkDto homework;
            for (int i = 2; i < 4; ++i)
            {
                homework= GetHomeworkMock(i);
                _homeworkFromDb.Add(homework);
                _homeworkIdList.Add(_homeworkRepo.AddHomework(homework));
            }

            List<HomeworkDto> actualHomeworkRepo = _homeworkRepo.GetHomeworks();
            Assert.AreEqual(_homeworkFromDb, actualHomeworkRepo);

        }

        [Test, Order(2)]
        public void TestUpdateHomework()
        {
            _homeworkFromDb = _homeworkRepo.GetHomeworks();
            HomeworkDto homework;
            foreach (int homeworkId in _homeworkIdList)
            {
                homework = _homeworkRepo.GetHomeworkById(homeworkId);
                homework.Description = $"New Description {homeworkId}";
                _homeworkRepo.UpdateHomework(homework);
                if (!homework.Equals(_homeworkRepo.GetHomeworkById(homeworkId)))
                {
                    Assert.Fail();
                }
            }
            Assert.Pass();
        }
        [Test, Order(3)]
        public void TestAddHomeworkAttempt()
        {
            _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
            HomeworkAttemptDto homeworkAttempt;
            for (int i = 2; i < 4; ++i)
            {
                homeworkAttempt = GetHomeworkAttemptMock(i);
                _homeworkAttemptFromDb.Add(homeworkAttempt);
                _homeworkAttemptIdList.Add(_homeworkRepo.AddHomeworkAttempt(homeworkAttempt));
            }


            Assert.AreEqual(_homeworkAttemptFromDb, _homeworkRepo.GetHomeworkAttempts());

        }

        [Test, Order(4)]
        public void TestUpdateHomeworkAttempt()
        {
            _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
            HomeworkAttemptDto homeworkAttempt;
            foreach (int homeworkAttemptId in _homeworkAttemptIdList)
            {
                homeworkAttempt = _homeworkRepo.GetHomeworkAttemptById(homeworkAttemptId);
                homeworkAttempt.Comment = $"New Comment {homeworkAttemptId}";
                _homeworkRepo.UpdateHomeworkAttempt(homeworkAttempt);
                if (!homeworkAttempt.Equals(_homeworkRepo.GetHomeworkAttemptById(homeworkAttemptId)))
                {
                    Assert.Fail();
                }
            }
            Assert.Pass();
        }

        [Test, Order(5)]
        public void TestDeleteHomeworkAttempt()
        {
            _homeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();
            int deletedId;
            foreach (int homeworkAttemptId in _homeworkAttemptIdList)
            {
                deletedId = _homeworkRepo.DeleteHomeworkAttempt(homeworkAttemptId);

                List < HomeworkAttemptDto > newHomeworkAttemptFromDb = _homeworkRepo.GetHomeworkAttempts();

                if (_homeworkAttemptFromDb.Count != newHomeworkAttemptFromDb.Count)
                {

                    Assert.Fail("Nothing was deleted");
                }
                else
                {
                    _homeworkAttemptFromDb = newHomeworkAttemptFromDb;
                }
                if(deletedId != homeworkAttemptId)
                {
                    Assert.Fail("Something wrong was deleted");
                }
            }
            Assert.Pass();

        }

        [Test, Order(6)]
        public void TestDeleteHomework()
        {
            _homeworkFromDb = _homeworkRepo.GetHomeworks();
            int deletedId;
            foreach (int homeworkId in _homeworkIdList)
            {
                deletedId = _homeworkRepo.DeleteHomework(homeworkId);
                List<HomeworkDto> newHomeworkFromDb = _homeworkRepo.GetHomeworks();
                if (_homeworkFromDb.Count != newHomeworkFromDb.Count)
                {
                    Assert.Fail("Nothing was deleted");
                }
                else
                {
                    _homeworkFromDb = newHomeworkFromDb;
                }
                if (deletedId != homeworkId)
                {
                    Assert.Fail("Something wrong was deleted");
                }
            }
            Assert.Pass();

        }
        [OneTimeTearDown]
        public void TearDowTest()
        {
            DeleteGroups();
            DeleteCourse();
            DeleteUsers();
            DeleteAttemptStatus();
        }

        public void DeleteAttemptStatus()
        {
            foreach (int homeworkAttemptStatusId in _homeworkAttemptStatusIdList)
            {
                _homeworkRepo.DeleteHomeworkAttemptStatus(homeworkAttemptStatusId);
            }
        }
        public void DeleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }
        public void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.DeleteCourse(courseId);
            }
        }
        public void DeleteUsers()
        {
            foreach (int userId in _userIdList)
            {
                _userRepo.DeleteUser(userId);
            }
        }
        public HomeworkAttemptStatusDto GetHomeworkAttemptStatusMock(int n)
        {
            HomeworkAttemptStatusDto result = new HomeworkAttemptStatusDto();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result=  (new HomeworkAttemptStatusDto { Name= "Test HomeworkAttemptStatusDto 1" });
                    return result;
                case 3:
                    result = (new HomeworkAttemptStatusDto { Name = "Test HomeworkAttemptStatusDto 2" });
                    return result;
                default:
                    return result;
            }
        }
        public HomeworkAttemptDto GetHomeworkAttemptMock(int n)
        {
            HomeworkAttemptDto result = new HomeworkAttemptDto();

            switch (n)
            {

                case 1:
                    return result;
                case 2:
                    result = (new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });

                    UserDto author = GetUserMock(n);
                    _userIdList.Add(_userRepo.AddUser(author));
                    result.Author = author;
                    result.Author.Id = _userIdList[_userIdList.Count - 1];

                    HomeworkDto homework =  GetHomeworkMock(n);
                    _homeworkIdList.Add(_homeworkRepo.AddHomework(homework));
                    result.Homework = homework;
                    result.Homework.Id = _homeworkIdList[_homeworkIdList.Count - 1];

                    HomeworkAttemptStatusDto homeworkAttemptStatus = GetHomeworkAttemptStatusMock(n);
                    _homeworkAttemptStatusIdList.Add(_homeworkRepo.AddHomeworkAttemptStatus(homeworkAttemptStatus));
                    result.HomeworkAttemptStatus = homeworkAttemptStatus;
                    result.HomeworkAttemptStatus.Id = _homeworkAttemptStatusIdList[_homeworkAttemptStatusIdList.Count - 1];

                    return result;
                case 3:
                    result = (new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });
                    author = GetUserMock(n);
                    _userIdList.Add(_userRepo.AddUser(author));
                    result.Author = author;
                    result.Author.Id = _userIdList[_userIdList.Count - 1];

                    homework = GetHomeworkMock(n);
                    _homeworkIdList.Add(_homeworkRepo.AddHomework(homework));
                    result.Homework = homework;
                    result.Homework.Id = _homeworkIdList[_homeworkIdList.Count - 1];

                    homeworkAttemptStatus = GetHomeworkAttemptStatusMock(n);
                    _homeworkAttemptStatusIdList.Add(_homeworkRepo.AddHomeworkAttemptStatus(homeworkAttemptStatus));
                    result.HomeworkAttemptStatus = homeworkAttemptStatus;
                    result.HomeworkAttemptStatus.Id = _homeworkAttemptStatusIdList[_homeworkAttemptStatusIdList.Count - 1];

                    return result;
                default:
                    return result;
            }
        }

        public UserDto GetUserMock(int n)
        {
            UserDto result = new UserDto();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result = (new UserDto 
                    {
                        FirstName = "Петр", 
                        LastName = "Петров", 
                        BirthDate = new DateTime(1980,3,12),
                        Login = "Petr01", 
                        Password = "qqq123", 
                        Phone = "89825553535",
                        UserPic = "ddsa",
                        Email = "Petr.Petrov@mail.ru",
                        IsDeleted = false 
                    });
                    return result;
                case 3:
                    result=(new UserDto
                    {
                        FirstName = "Вася",
                        LastName = "Васильев",
                        BirthDate = new DateTime(1980, 7, 12),
                        Login = "Vasya01",
                        Password = "qqq123",
                        Phone = "8982543535",
                        UserPic = "ddsasda",
                        Email = "Vasya.Vaskin@mail.ru",
                        IsDeleted = false
                    });
                    return result;
                case 4:

                    result = (new UserDto
                    {
                        FirstName = "Максим",
                        LastName = "Максимов",
                        BirthDate = new DateTime(1982, 1, 11),
                        Login = "Max01",
                        Password = "qqq123",
                        Phone = "8982552535",
                        UserPic = "ddsa",
                        Email = "Max.Maximov@mail.ru",
                        IsDeleted = false
                    });
                    return result;
                default:
                    return result;
            }
        }
        public HomeworkDto GetHomeworkMock(int n)
        {
            HomeworkDto result = new HomeworkDto();
            switch (n)
            {
                case 1:
                    result = new HomeworkDto() { Description = "Test case 1", StartDate = new DateTime(2021, 1, 5), DeadlineDate = new DateTime(2021, 1, 11), IsOptional = true};
                    result.Group = GetGroupMock(n);
                    _groupIdList.Add(_groupRepo.AddGroup(result.Group));
                    result.Group.Id = _groupIdList[_groupIdList.Count - 1];

                    return result;
                case 2:
                    result = new HomeworkDto() { Description = "Test case 2", StartDate = new DateTime(2021, 1, 12), DeadlineDate = new DateTime(2021, 1, 19),  IsOptional = true };
                    result.Group = GetGroupMock(n);
                    _groupIdList.Add(_groupRepo.AddGroup(result.Group));
                    result.Group.Id = _groupIdList[_groupIdList.Count - 1];
                    return result;
                case 3:
                    result = new HomeworkDto() { Description = "Test case 3", StartDate = new DateTime(2021, 1, 20), DeadlineDate = new DateTime(2021, 1, 25), IsOptional = false };
                    result.Group = GetGroupMock(n);
                    _groupIdList.Add(_groupRepo.AddGroup(result.Group));
                    result.Group.Id = _groupIdList[_groupIdList.Count - 1];
                    return result;
                default:
                    return result;
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
                case 3:
                    course = new CourseDto() { Name = "TestCourseCase 3", Description = "Test case 3", Duration = 3 };
                    return course;
                case 4:
                    course = new CourseDto() { Name = "TestCourseCase 4", Description = "Test case 4", Duration = 4 };
                    return course;
                default:
                    return course;
            }
        }
        public GroupDto GetGroupMock(int n)
        {
            GroupDto groups = new GroupDto();
            switch (n)
            {
                case 1:
                    return groups;
                case 2:
                    CourseDto course = GetCourseMock(n);
                    _courseIdList.Add(_courseRepo.AddCourse(course));
                    course.Id = _courseIdList[_courseIdList.Count - 1];

                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = course, StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 3:
                    course = GetCourseMock(n);
                    _courseIdList.Add(_courseRepo.AddCourse(course));
                    course.Id = _courseIdList[_courseIdList.Count - 1];

                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1}, Course = course, StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 4:
                    course = GetCourseMock(n);
                    _courseIdList.Add(_courseRepo.AddCourse(course));
                    course.Id = _courseIdList[_courseIdList.Count - 1];

                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = course.Id }, Course = course, StartDate = new System.DateTime(2021, 10, 12) });

                    return groups;
                default:
                    return groups;
            }
        }
    }
}

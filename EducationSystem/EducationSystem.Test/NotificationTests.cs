using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class NotificationTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IUserRepository _userRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IAttachmentRepository _attachmentRepo;
        private INotificationRepository _notificationRepo;

        private UserDto _userDtoMock;
        private UserDto _authorDtoMock;
        private GroupDto _groupDtoMock;
        private HomeworkDto _homeworkDtoMock;

        private List<int> _homeworkAttemptIdList; 
        private List<int> _homeworkIdList; 
        private List<int> _userIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _attachmentIdList;
        private List<(int, int)> _attemptAttachmentIdList;
        private List<int> _notificationIdList; 

        [SetUp]
        public void SetUpTest()
        {
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _groupRepo = new GroupRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _attachmentRepo = new AttachmentRepository(_options);
            _notificationRepo = new NotificationRepository(_options);

            _homeworkAttemptIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _userIdList = new List<int>();
            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _attachmentIdList = new List<int>();
            _attemptAttachmentIdList = new List<(int, int)>();
            _notificationIdList = new List<int>();

            _userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            _userDtoMock.Id = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(_userDtoMock.Id);

            _authorDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(2).Clone();
            _authorDtoMock.Id = _userRepo.AddUser(_authorDtoMock);
            _userIdList.Add(_authorDtoMock.Id);

            _groupDtoMock = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            _groupDtoMock.Course = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var addedCourseId = _courseRepo.AddCourse(_groupDtoMock.Course);
            _courseIdList.Add(addedCourseId);
            _groupDtoMock.Course.Id = addedCourseId;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;

            _homeworkDtoMock = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            _homeworkDtoMock.Group = _groupDtoMock;
            var addedhomeworkId = _homeworkRepo.AddHomework(_homeworkDtoMock);
            _homeworkIdList.Add(addedhomeworkId);
            _homeworkDtoMock.Id = addedhomeworkId;
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddNotificationPositiveTest(int mockId)
        {
            // Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            Assert.Greater(addedNotificationId, 0);

            _notificationIdList.Add(addedNotificationId);
            dto.Id = addedNotificationId;

            // When
            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        //[Test]
        //public void AddHomeworkAttemptNullEntityNegativeTest()
        //{
        //    //Given

        //    //When
        //    try
        //    {
        //        var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(null);
        //        _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
        //    }

        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        //[Test]
        //public void AddHomeworkAttemptEmptyPropertyNegativeTest()
        //{
        //    //Given
        //    var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(4).Clone();

        //    //When
        //    try
        //    {
        //        var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
        //        _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
        //    }

        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        //[Test]
        //public void GetHomeworkAttemptByIdEntityNotExistNegativeTest()
        //{
        //    //Given

        //    //When
        //    var hwattempt = _homeworkRepo.GetHomeworkAttemptById(-1);
        //    //Then
        //    Assert.IsNull(hwattempt);
        //}

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        public void UpdateNotificationTest(int mockId, int updateMockId)
        {
            // Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            _notificationIdList.Add(addedNotificationId);

            dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(updateMockId).Clone();
            dto.Id = addedNotificationId;
            var affectedRowsCount = _notificationRepo.UpdateNotification(dto);

            // When
            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [Test]
        public void UpdateHomeworkAttemptNullEntityNegativeTest()
        {
            //Given
            
            //When
            try
            {
                var homeworkAttemptId = _homeworkRepo.UpdateHomeworkAttempt(null);
                _homeworkAttemptIdList.Add(homeworkAttemptId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void UpdateHomeworkAttemptEntityNotExistNegativeTest()
        {
            //Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(1).Clone();
            dto.Id = -1;

            //When
            var result = _homeworkRepo.UpdateHomeworkAttempt(dto);

            //Then
            Assert.AreEqual(0, result);

        }

        [Test]
        public void UpdateHomeworkAttemptEmptyPropertyNegativeTest()
        {
            //Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(1).Clone();
            dto.Author = _userDtoMock;
            dto.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);

            var updto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(4).Clone();
            updto.Id = addedHomeworkAttemptId;
            //When
            try
            {

                var homeworkAttemptId = _homeworkRepo.UpdateHomeworkAttempt(updto);
                _homeworkAttemptIdList.Add(homeworkAttemptId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        

        [TearDown]
        public void TestTearDown()
        {
            DeleteNotifications();
            DeleteHomworkAttemptAttachments();
            DeleteAttachments();
            DeleteHomeworkAttempt();
            DeleteHomework();
            DeleteGroups();
            DeleteCourse();
            DeleteUser();
        }
        private void DeleteNotifications()
        {
            foreach (var item in _notificationIdList)
            {
                _notificationRepo.DeleteNotification(item);
            }
        }
        private void DeleteHomworkAttemptAttachments()
        {
            foreach (var item in _attemptAttachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentFromHomeworkAttempt(item.Item2, item.Item1);
            }
        }
        private void DeleteAttachments()
        {
            foreach(var item in _attachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentById(item);
            }
        }

        private void DeleteUser()
        {
            foreach (var userId in _userIdList)
            {
                _userRepo.HardDeleteUser(userId);
            }
        }

        private void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }

        private void DeleteGroups()
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

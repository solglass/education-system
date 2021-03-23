using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class CommentTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IUserRepository _userRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;

        private List<int> _commentIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _userIdList;
        private List<int> _homeworkIdList;
        private List<int> _homeworkAttemptIdList;

        private UserDto _userDtoMock;
        private CourseDto _courseDtoMock;
        private GroupDto _groupDtoMock;
        private HomeworkDto _homeworkDtoMock;
        private HomeworkAttemptDto _homeworkAttemptDtoMock;

        [SetUp]
        public void OneTimeSetUpTest()
        {
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _groupRepo = new GroupRepository(_options);

            _commentIdList = new List<int>();
            _userIdList = new List<int>();
            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _homeworkAttemptIdList = new List<int>();

            _courseDtoMock = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(addedCourseId);
            _courseDtoMock.Id = addedCourseId;

            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;

            _userDtoMock = UserMockGetter.GetUserDtoMock(3);
            var addedUserId = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(addedUserId);
            _userDtoMock.Id = addedUserId;

            _homeworkDtoMock = HomeworkMockGetter.GetHomeworkDtoMock(1);
            _homeworkDtoMock.Group = _groupDtoMock;
            var addedhomeworkId = _homeworkRepo.AddHomework(_homeworkDtoMock);
            _homeworkIdList.Add(addedhomeworkId);
            _homeworkDtoMock.Id = addedhomeworkId;

            _homeworkAttemptDtoMock = HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(1);
            _homeworkAttemptDtoMock.Author = _userDtoMock;
            _homeworkAttemptDtoMock.Homework = _homeworkDtoMock;
            var addedhomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(_homeworkAttemptDtoMock);
            _homeworkAttemptIdList.Add(addedhomeworkAttemptId);
            _homeworkAttemptDtoMock.Id = addedhomeworkAttemptId;
        }

        [TestCase(1)]
        public void CommentAddPositiveTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(dto);
            Assert.Greater(addedCommentId, 0);

            _commentIdList.Add(addedCommentId);
            dto.Id = addedCommentId;

            //When
            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.AreEqual(dto, actual);
        }

        //[TestCase(4)]
        //public void CommentAdd_EmptyComment_NegativeTest(int mockId)
        //{
        //    //Given
        //    var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();
        //    dto.Group = _groupDtoMock;
        //    //When
        //    try
        //    {
        //        var addedHomeworkId = _homeworkRepo.AddHomework(dto);
        //        _homeworkIdList.Add(addedHomeworkId);
        //    }
        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        //[TestCase(1)]
        //public void CommentAdd_WithoutGroup_NegativeTest(int mockId)
        //{
        //    //Given
        //    var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();

        //    //When
        //    try
        //    {
        //        var addedHomeworkId = _homeworkRepo.AddHomework(dto);
        //        _homeworkIdList.Add(addedHomeworkId);
        //    }
        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        //[Test]
        //public void CommentAdd_Null_NegativeTest()
        //{
        //    //Given

        //    //When
        //    try
        //    {
        //        var addedHomeworkId = _homeworkRepo.AddHomework(null);
        //        _homeworkIdList.Add(addedHomeworkId);
        //    }
        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        [TestCase(1)]
        public void CommentUpdatePositiveTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(dto);
            _commentIdList.Add(addedCommentId);

            dto = new CommentDto
            {
                Id = addedCommentId,
                Message = "Test 2 mock",
                IsDeleted = false
            };
            _homeworkRepo.UpdateComment(dto);

            //When
            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        //[TestCase(4)]
        //public void CommentUpdate_Empty_NegativeTest(int mockId)
        //{
        //    //Given
        //    var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
        //    dto.Group = _groupDtoMock;
        //    var addedHomeworkId = _homeworkRepo.AddHomework(dto);
        //    _homeworkIdList.Add(addedHomeworkId);

        //    dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(mockId).Clone();

        //    //When
        //    try
        //    {
        //        _homeworkRepo.UpdateHomework(dto);
        //    }
        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        //[Test]
        //public void CommentUpdate_Null_NegativeTest()
        //{
        //    //Given
        //    var dto = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
        //    dto.Group = _groupDtoMock;
        //    var addedHomeworkId = _homeworkRepo.AddHomework(dto);
        //    _homeworkIdList.Add(addedHomeworkId);
        //    //When
        //    try
        //    {
        //        _homeworkRepo.UpdateHomework(null);
        //    }
        //    //Then
        //    catch
        //    {
        //        Assert.Pass();
        //    }
        //    Assert.Fail();
        //}

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void CommentDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(dto);
            _commentIdList.Add(addedCommentId);

            dto.Id = addedCommentId;
            dto.IsDeleted = isDeleted;

            //When
            var affectedRowsCount = _homeworkRepo.DeleteOrRecoverComment(addedCommentId, isDeleted);

            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);

        }

        //[Test]
        //public void CommentDeleteOrRecover_NotExistComment_NegativeTest()
        //{
        //    //Given
        //    //When
        //    var deletedRows = _homeworkRepo.DeleteOrRecoverHomework(-1, true);

        //    //Then
        //    Assert.AreEqual(0, deletedRows);
        //}

        [TearDown]
        public void TearDowTest()
        {
            DeleteComment();
            DeleteHomeworkAttempt();
            DeleteHomework();
            DeleteUser();
            DeleteGroup();
            DeleteCourse();

        }

        private void DeleteComment()
        {
            foreach (int commentId in _commentIdList)
            {
                _homeworkRepo.HardDeleteComment(commentId);
            }
        }
        public void DeleteCourse()
        {
            foreach (int courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }

        public void DeleteGroup()
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

        private void DeleteUser()
        {
            foreach (var user in _userIdList)
            {
                _userRepo.HardDeleteUser(user);
            }
        }

        private void DeleteHomeworkAttempt()
        {
            foreach (var homeworkAttempt in _homeworkAttemptIdList)
            {
                _homeworkRepo.HardDeleteHomeworkAttempt(homeworkAttempt);
            }
        }
    }
}
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
        private IAttachmentRepository _attachmentRepo;

        private List<(int, int)> _commentAttachmentIdList;
        private List<int> _attachmentIdList;
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
        public void SetUpTest()
        {
            _attachmentRepo = new AttachmentRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _groupRepo = new GroupRepository(_options);

            _commentAttachmentIdList = new List<(int,int)>();
            _attachmentIdList = new List<int>();
            _commentIdList = new List<int>();
            _userIdList = new List<int>();
            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _homeworkAttemptIdList = new List<int>();

            _courseDtoMock = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var addedCourseId = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(addedCourseId);
            _courseDtoMock.Id = addedCourseId;

            _groupDtoMock = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            _groupDtoMock.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;

            var groupDtoMock = (GroupDto)GroupMockGetter.GetGroupDtoMock(2).Clone();
            groupDtoMock.Course = _courseDtoMock;
            var addedGroupId1 = _groupRepo.AddGroup(groupDtoMock);
            _groupIdList.Add(addedGroupId1);
            groupDtoMock.Id = addedGroupId1;

            _userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(3).Clone();
            var addedUserId = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(addedUserId);
            _userDtoMock.Id = addedUserId;

            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(4).Clone();
            var addedUserId1 = _userRepo.AddUser(userDtoMock);
            _userIdList.Add(addedUserId1);
            userDtoMock.Id = addedUserId1;

            _homeworkDtoMock = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(1).Clone();
            _homeworkDtoMock.Group = _groupDtoMock; 
            var addedhomeworkId = _homeworkRepo.AddHomework(_homeworkDtoMock);
            _homeworkIdList.Add(addedhomeworkId);
            _homeworkDtoMock.Id = addedhomeworkId;

            var homeworkDtoMock = (HomeworkDto)HomeworkMockGetter.GetHomeworkDtoMock(3).Clone();
            homeworkDtoMock.Group = groupDtoMock;
            var addedhomeworkId1 = _homeworkRepo.AddHomework(homeworkDtoMock);
            _homeworkIdList.Add(addedhomeworkId1);
            homeworkDtoMock.Id = addedhomeworkId1;

            _homeworkAttemptDtoMock = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(1).Clone();
            _homeworkAttemptDtoMock.Author = _userDtoMock;
            _homeworkAttemptDtoMock.Homework = _homeworkDtoMock;
            var addedhomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(_homeworkAttemptDtoMock);
            _homeworkAttemptIdList.Add(addedhomeworkAttemptId);
            _homeworkAttemptDtoMock.Id = addedhomeworkAttemptId;

            var homeworkAttemptDtoMock = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(3).Clone();
            homeworkAttemptDtoMock.Author = userDtoMock;
            homeworkAttemptDtoMock.Homework = homeworkDtoMock;
            var addedhomeworkAttemptId1 = _homeworkRepo.AddHomeworkAttempt(homeworkAttemptDtoMock);
            _homeworkAttemptIdList.Add(addedhomeworkAttemptId1);
            homeworkAttemptDtoMock.Id = addedhomeworkAttemptId1;

        }

        [TestCase(1)]
        public void CommentAddPositiveTest(int mockId)
        {
            //Given
            var expected = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            expected.Author = _userDtoMock;
            expected.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(expected);
            Assert.Greater(addedCommentId, 0);

            _commentIdList.Add(addedCommentId);
            expected.Id = addedCommentId;

            //When
            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.IsTrue(CustomCompare(expected, actual));
        }
        [TestCase(1)]
        public void CommentUpdatePositiveTest(int mockId)
        {
            //Given
            var comment = (CommentDto)CommentMockGetter.GetCommentDtoMock(2).Clone();
            comment.Author = _userDtoMock;
            comment.HomeworkAttempt = _homeworkAttemptDtoMock;
            _commentIdList.Add(_homeworkRepo.AddComment(comment));

            var expected = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            expected.Author = _userDtoMock;
            expected.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(expected);
            _commentIdList.Add(addedCommentId);

            var dto = new CommentDto
            {
                Id = addedCommentId,
                Message = "Test 2 mock",
                IsDeleted = false
            };
            expected.Id = addedCommentId;
            expected.Message = dto.Message;
            expected.IsDeleted = dto.IsDeleted;

            var affectedRowsCount = _homeworkRepo.UpdateComment(dto);

            //When
            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsTrue(CustomCompare(expected, actual));

        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void CommentDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var comment = (CommentDto)CommentMockGetter.GetCommentDtoMock(2).Clone();
            comment.Author = _userDtoMock;
            comment.HomeworkAttempt = _homeworkAttemptDtoMock;
            _commentIdList.Add(_homeworkRepo.AddComment(comment));

            var expected = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            expected.Author = _userDtoMock;
            expected.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(expected);
            _commentIdList.Add(addedCommentId);

            expected.Id = addedCommentId;
            expected.IsDeleted = isDeleted;

            //When
            var affectedRowsCount = _homeworkRepo.DeleteOrRecoverComment(addedCommentId, isDeleted);

            var actual = _homeworkRepo.GetCommentById(addedCommentId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(expected, actual);

        }
        [TestCase(new int[] { 1, 2 })]
        public void SearchCommentsByHomeworkAttemptIdPositiveTest(int[] mockIds)
        {
            //Given
            var expected = new List<CommentDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockIds[i]).Clone();
                dto.Author = _userDtoMock;
                dto.HomeworkAttempt = _homeworkAttemptDtoMock;
                dto.Attachments = new List<AttachmentDto>();

                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
                dto.Id = addedCommentId;

                for (int j = 0; j < mockIds.Length; j++)
                {
                    var attachmentDto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockIds[j]).Clone();
                    var addedAttachmentId = _attachmentRepo.AddAttachment(attachmentDto);
                    _attachmentIdList.Add(addedAttachmentId);
                    attachmentDto.Id = addedAttachmentId;
                    dto.Attachments.Add(attachmentDto);

                    _attachmentRepo.AddAttachmentToComment(addedCommentId, addedAttachmentId);
                    _commentAttachmentIdList.Add((addedCommentId, addedAttachmentId));
                }           
                expected.Add(dto);
            }
            //When
            var actual = _homeworkRepo.SearchComments(_homeworkAttemptDtoMock.Id, null);

            //Then
            for (var i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(CustomCompare(expected[i], actual[i]));
            }
        }
        [TestCase(new int[] { 1, 2})]
        public void SearchCommentsByHomeworkIdPositiveTest(int[] mockIds)
        {
            //Given
            var expected = new List<CommentDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockIds[i]).Clone();

                var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(mockIds[i]).Clone();
                var addedUserId = _userRepo.AddUser(userDtoMock);
                _userIdList.Add(addedUserId);
                userDtoMock.Id = addedUserId;

                var homeworkAttemptDtoMock = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockIds[i]).Clone();
                homeworkAttemptDtoMock.Author = userDtoMock;
                homeworkAttemptDtoMock.Homework = _homeworkDtoMock;
                var addedhomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(homeworkAttemptDtoMock);
                _homeworkAttemptIdList.Add(addedhomeworkAttemptId);
                homeworkAttemptDtoMock.Id = addedhomeworkAttemptId;

                dto.Author = userDtoMock;
                dto.HomeworkAttempt = homeworkAttemptDtoMock;
                dto.Attachments = new List<AttachmentDto>();

                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
                dto.Id = addedCommentId;

                for (int j = 0; j < mockIds.Length; j++)
                {
                    var attachmentDto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockIds[j]).Clone();
                    var addedAttachmentId = _attachmentRepo.AddAttachment(attachmentDto);
                    _attachmentIdList.Add(addedAttachmentId);
                    attachmentDto.Id = addedAttachmentId;
                    dto.Attachments.Add(attachmentDto);

                    _attachmentRepo.AddAttachmentToComment(addedCommentId, addedAttachmentId);
                    _commentAttachmentIdList.Add((addedCommentId, addedAttachmentId));
                }
                expected.Add(dto);
            }
            //When
            var actual = _homeworkRepo.SearchComments(null, _homeworkDtoMock.Id);

            //Then
            for (var i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(CustomCompare(expected[i], actual[i]));
            }
        }

        [TestCase(4)]
        public void CommentAdd_EmptyComment_NegativeTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;
            //When
            try
            {
                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void CommentAdd_WithoutUser_NegativeTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;

            //When
            try
            {
                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(1)]
        public void CommentAdd_WithoutHomeworkAttempt_NegativeTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;

            //When
            try
            {
                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(1)]
        public void CommentAdd_WithoutUserAndHomeworkAttempt_NegativeTest(int mockId)
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(mockId).Clone();

            //When
            try
            {
                var addedCommentId = _homeworkRepo.AddComment(dto);
                _commentIdList.Add(addedCommentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void CommentAdd_Null_NegativeTest()
        {
            //Given

            //When
            try
            {
                var addedCommentId = _homeworkRepo.AddComment(null);
                _commentIdList.Add(addedCommentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void CommentUpdate_Null_NegativeTest()
        {
            //Given
            var dto = (CommentDto)CommentMockGetter.GetCommentDtoMock(1).Clone();
            dto.Author = _userDtoMock;
            dto.HomeworkAttempt = _homeworkAttemptDtoMock;

            var addedCommentId = _homeworkRepo.AddComment(dto);
            _commentIdList.Add(addedCommentId);
            //When
            try
            {
                _homeworkRepo.UpdateComment(null);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [Test]
        public void CommentDeleteOrRecover_NotExistComment_NegativeTest()
        {
            //Given
            //When
            var deletedRows = _homeworkRepo.DeleteOrRecoverComment(-1, true);

            //Then
            Assert.AreEqual(0, deletedRows);
        }

        [TearDown]
        public void TearDownTest()
        {
            DeleteCommentAttachment();
            DeleteAttachment();
            DeleteComment();
            DeleteHomeworkAttempt();
            DeleteHomework();
            DeleteUser();
            DeleteGroup();
            DeleteCourse();
        }
        private bool CustomCompare(CommentDto expected, CommentDto actual)
        {
            bool isEqual = true;
            if(actual.Attachments != null)
            {
                for (int i = 0; i < actual.Attachments.Count; i++)
                {
                    if (!expected.Attachments[i].Equals(actual.Attachments[i])) isEqual = false;
                }
            }
            return isEqual && expected.Id == actual.Id
                && expected.Message == actual.Message
                && expected.HomeworkAttempt.Id == actual.HomeworkAttempt.Id
                && expected.HomeworkAttempt.Comment == actual.HomeworkAttempt.Comment
                && expected.Author.Id == actual.Author.Id
                && expected.Author.FirstName == actual.Author.FirstName
                && expected.Author.LastName == actual.Author.LastName;
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
                _groupRepo.DeleteGroup(groupId);
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
        private void DeleteAttachment()
        {
            foreach (var attachment in _attachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentById(attachment);
            }
        }
        private void DeleteCommentAttachment()
        {
            foreach (var commentAttachmentPair in _commentAttachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentFromComment(commentAttachmentPair.Item2, commentAttachmentPair.Item1);
            }
        }
    }
}
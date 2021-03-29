using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class HomeworkAttemptTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IUserRepository _userRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IAttachmentRepository _attachmentRepo;

        private UserDto _userDtoMock;
        private GroupDto _groupDtoMock;
        private HomeworkDto _homeworkDtoMock;

        private List<int> _homeworkAttemptIdList; 
        private List<int> _homeworkIdList; 
        private List<int> _userIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _attachmentIdList;
        private List<(int, int)> _attemptAttachmentIdList;

        [SetUp]
        public void SetUpTest()
        {
            _homeworkRepo = new HomeworkRepository(_options);
            _userRepo = new UserRepository(_options);
            _groupRepo = new GroupRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _attachmentRepo = new AttachmentRepository(_options);

            _homeworkAttemptIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _userIdList = new List<int>();
            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _attachmentIdList = new List<int>();
            _attemptAttachmentIdList = new List<(int, int)>();

            _userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var addedUserId = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(addedUserId);
            _userDtoMock.Id = addedUserId;

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

        [Test]
        public void AddHomeworkAttemptNullEntityNegativeTest()
        {
            //Given

            //When
            try
            {
                var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(null);
                _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            }

            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddHomeworkAttemptEmptyPropertyNegativeTest()
        {
            //Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(4).Clone();

            //When
            try
            {
                var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
                _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            }

            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void GetHomeworkAttemptByIdEntityNotExistNegativeTest()
        {
            //Given

            //When
            var hwattempt = _homeworkRepo.GetHomeworkAttemptById(-1);
            //Then
            Assert.IsNull(hwattempt);
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

        [TestCase(1, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(3, true)]
        public void DeleteOrRecoverHomeworkAttemptPositiveTest(int mockId, bool isDeleted)
        {
            //Given
            var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockId).Clone();
            dto.Author = _userDtoMock;
            dto.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            dto.Id = addedHomeworkAttemptId;
            dto.IsDeleted = isDeleted;

            // When
            var affectedRowsCount = _homeworkRepo.DeleteOrRecoverHomeworkAttempt(addedHomeworkAttemptId, isDeleted);

            var actual = _homeworkRepo.GetHomeworkAttemptById(addedHomeworkAttemptId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [Test]
        public void DeleteOrRecoverHomeworkAttemptNotExistEntityNegativeTest()
        {
            //Given
            //When
            var deletedRows = _homeworkRepo.DeleteOrRecoverHomeworkAttempt(-1, true);

            //Then
            Assert.AreEqual(0, deletedRows);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        [TestCase(new int[] { })]
        public void GetHomeworkAttemptsByUserIdPositiveTest(int[] mockIds)
        {
            // Given
            var addedUserId = _userDtoMock.Id;

            var expected = new List<HomeworkAttemptDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockIds[i]).Clone();
                dto.Author = _userDtoMock;
                dto.Homework = _homeworkDtoMock;
                var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
                _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
                dto.Id = addedHomeworkAttemptId;
                expected.Add(dto);
            }

            // When
            var actual = _homeworkRepo.GetHomeworkAttemptsByUserId(addedUserId);

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        [TestCase(new int[] { })]
        public void GetHomeworkAttemptsByHomeworkIdPositiveTest(int[] mockIds)
        {
            // Given
            var addedHomeworkId = _homeworkDtoMock.Id;

            var expected = new List<HomeworkAttemptDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockIds[i]).Clone();
                dto.Author = _userDtoMock;
                dto.Homework = _homeworkDtoMock;
                var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
                _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
                dto.Id = addedHomeworkAttemptId;
                expected.Add(dto);
            }

            // When
            var actual = _homeworkRepo.GetHomeworkAttemptsByHomeworkId(addedHomeworkId);

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, 3)]
        [TestCase(new int[] {  }, 3)]
        public void GetHomeworkAttemptsByStatusIdAndGroupIdPositiveTest(int[] mockIds, int statusId)
        {
            // Given
            var groupId = _groupDtoMock.Id;

            var expected = new List<HomeworkAttemptDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(mockIds[i]).Clone();
                dto.Author = _userDtoMock;
                dto.Homework = _homeworkDtoMock;
                var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(dto);
                _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
                dto.Id = addedHomeworkAttemptId;
                dto.HomeworkAttemptStatus = (Core.Enums.HomeworkAttemptStatus)statusId;
                expected.Add(dto);
            }

            // When
            var actual = _homeworkRepo.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId);

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestCase(1, new int[] { 1,2})]
        [TestCase(1, new int[] { 1})]
        public void AddHomeworkAttemptAttachmentPositiveTest(int attemptMockId, int[] attachmentMockIds)
        {
            //Given
            var expected = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(attemptMockId).Clone();
            expected.Author = _userDtoMock;
            expected.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(expected);
            Assert.Greater(addedHomeworkAttemptId, 0);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            expected.Id = addedHomeworkAttemptId;
            
            foreach(var attachmentMockId in attachmentMockIds)
            {
                var attachment = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(attachmentMockId).Clone();
                attachment.Id = _attachmentRepo.AddAttachment(attachment);
                Assert.Greater(attachment.Id, 0);
                _attachmentIdList.Add(attachment.Id);
                var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(addedHomeworkAttemptId, attachment.Id);
                Assert.Greater(result, 0);
                _attemptAttachmentIdList.Add((addedHomeworkAttemptId, attachment.Id));
                expected.Attachments.Add(attachment);
            }

            //When
            var actual = _attachmentRepo.GetAttachmentsByHomeworkAttemptId(addedHomeworkAttemptId);

            //Then
            CollectionAssert.AreEqual(expected.Attachments, actual);
        }

        [TestCase(1)]
        public void AddHomeworkAttemptAttachmentNegativeTestHomeworkAttemptNotExists(int attachmentMockId)
        {
            //Given
            var attachment = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(attachmentMockId).Clone();
            attachment.Id = _attachmentRepo.AddAttachment(attachment);
            Assert.Greater(attachment.Id, 0);
            _attachmentIdList.Add(attachment.Id);
            //When
            try
            {
                var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(-1, attachment.Id);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddHomeworkAttemptAttachmentNegativeTestAttachmentNotExists(int attemptMockId)
        {
            //Given
            var attempt = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(attemptMockId).Clone();
            attempt.Author = _userDtoMock;
            attempt.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(attempt);
            Assert.Greater(addedHomeworkAttemptId, 0);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            attempt.Id = addedHomeworkAttemptId;
            //When
            try
            {
                var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(attempt.Id, -1);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1,1)]
        public void AddHomeworkAttemptAttachmentNegativeTestNotUniqueEntity(int attemptMockId, int attachmentMockId)
        {
            //Given
            var attempt = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(attemptMockId).Clone();
            attempt.Author = _userDtoMock;
            attempt.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(attempt);
            Assert.Greater(addedHomeworkAttemptId, 0);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            attempt.Id = addedHomeworkAttemptId;

            var attachment = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(attachmentMockId).Clone();
            attachment.Id = _attachmentRepo.AddAttachment(attachment);
            Assert.Greater(attachment.Id, 0);
            _attachmentIdList.Add(attachment.Id);

            var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(addedHomeworkAttemptId, attachment.Id);
            Assert.Greater(result, 0);
            _attemptAttachmentIdList.Add((addedHomeworkAttemptId, attachment.Id));

            //When
            try
            {
                result = _attachmentRepo.AddAttachmentToHomeworkAttempt(addedHomeworkAttemptId, attachment.Id);
                _attemptAttachmentIdList.Add((addedHomeworkAttemptId, attachment.Id));
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(1, new int[] { 1, 2 })]
        [TestCase(1, new int[] { 1 })]
        public void DeleteHomeworkAttemptAttachmentPositiveTest(int attemptMockId, int[] attachmentMockIds)
        {
            //Given
            var expected = (HomeworkAttemptDto)HomeworkAttemptMockGetter.GetHomeworkAttemptDtoMock(attemptMockId).Clone();
            expected.Author = _userDtoMock;
            expected.Homework = _homeworkDtoMock;
            var addedHomeworkAttemptId = _homeworkRepo.AddHomeworkAttempt(expected);
            Assert.Greater(addedHomeworkAttemptId, 0);
            _homeworkAttemptIdList.Add(addedHomeworkAttemptId);
            expected.Id = addedHomeworkAttemptId;

            foreach (var attachmentMockId in attachmentMockIds)
            {
                var attachment = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(attachmentMockId).Clone();
                attachment.Id = _attachmentRepo.AddAttachment(attachment);
                Assert.Greater(attachment.Id, 0);

                _attachmentIdList.Add(attachment.Id);
                var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(addedHomeworkAttemptId, attachment.Id);
                Assert.Greater(result, 0);

                _attemptAttachmentIdList.Add((addedHomeworkAttemptId, attachment.Id));
                expected.Attachments.Add(attachment);
            }

            foreach (var attachmentMockId in attachmentMockIds)
            {
                var attachment = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(attachmentMockId).Clone();
                attachment.Id = _attachmentRepo.AddAttachment(attachment);
                Assert.Greater(attachment.Id, 0);

                _attachmentIdList.Add(attachment.Id);
                var result = _attachmentRepo.AddAttachmentToHomeworkAttempt(addedHomeworkAttemptId, attachment.Id);
                Assert.Greater(result, 0);

                _attemptAttachmentIdList.Add((addedHomeworkAttemptId, attachment.Id));
                result = _attachmentRepo.DeleteAttachmentFromHomeworkAttempt(attachment.Id, addedHomeworkAttemptId);
                Assert.AreEqual(1, result);
            }
            //When
            var actual = _attachmentRepo.GetAttachmentsByHomeworkAttemptId(addedHomeworkAttemptId);

            //Then
            CollectionAssert.AreEqual(expected.Attachments, actual);

        }

        [Test]
        public void DeleteHomeworkAttemptAttachmentNegativeTestRelationNotExists()
        {
            //Given

            //When
            var result = _attachmentRepo.DeleteAttachmentFromHomeworkAttempt(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }

        [TearDown]
        public void TestTearDown()
        {
            DeleteHomworkAttemptAttachments();
            DeleteAttachments();
            DeleteHomeworkAttempt();
            DeleteHomework();
            DeleteGroups();
            DeleteCourse();
            DeleteUser();
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

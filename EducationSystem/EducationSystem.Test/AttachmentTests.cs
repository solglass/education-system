using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class AttachmentTests : BaseTest
    {
        private IAttachmentRepository _attachmentRepo;
        private List<int> _attachmentIdList;

        [SetUp]
        public void SetUpTest()
        {
            _attachmentRepo = new AttachmentRepository(_options);
            _attachmentIdList = new List<int>();
        }

        [TestCase(1)]
        public void AttachmentAddPositiveTest(int mockId)
        {
            //Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();

            var addedAttachmentId = _attachmentRepo.AddAttachment(expected);
            Assert.Greater(addedAttachmentId, 0);

            _attachmentIdList.Add(addedAttachmentId);
            expected.Id = addedAttachmentId;

            //When
            var actual = _attachmentRepo.GetAttachmentById(addedAttachmentId);

            //Then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(3)]
        public void AttachmentAdd_EmptyAttachment_NegativeTest(int mockId)
        {
            //Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            //When, Then
            try
            {
                var addedAttachmentId = _attachmentRepo.AddAttachment(expected);
                _attachmentIdList.Add(addedAttachmentId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(2)]
        public void AttachmentAdd_WithoutAttachmentType_NegativeTest(int mockId)
        {
            //Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            //When, Then
            try
            {
                var addedAttachmentId = _attachmentRepo.AddAttachment(expected);
                _attachmentIdList.Add(addedAttachmentId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AttachmentAdd_Null_NegativeTest()
        {
            //Given

            //When, Then
            try
            {
                var addedAttachmentId = _attachmentRepo.AddAttachment(null);
                _attachmentIdList.Add(addedAttachmentId);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AttachmentUpdatePositiveTest(int mockId)
        {
            //Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(expected);

            expected = new AttachmentDto
            {
                Id = addedAttachmentId,
                Description = "Update description",
                Path = "Update Test",
                AttachmentType = AttachmentType.Link
            };

            var affectedRowsCount = _attachmentRepo.UpdateAttachment(expected);
            _attachmentIdList.Add(addedAttachmentId);

            //When
            var actual = _attachmentRepo.GetAttachmentById(addedAttachmentId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(expected, actual);

        }
        [TestCase(1)]
        public void AttachmentDeletePositiveTest(int mockId)
        {
            // Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(expected);
            _attachmentIdList.Add(addedAttachmentId);

            // When
            var affectedRowsCount = _attachmentRepo.DeleteAttachmentById(addedAttachmentId);

            var actual = _attachmentRepo.GetAttachmentById(addedAttachmentId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);
        }

        [Test]
        public void AttachmentUpdate_Null_NegativeTest()
        {
            //Given
            var expected = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(1).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(expected);
            _attachmentIdList.Add(addedAttachmentId);
            //When, Then
            try
            {
                _attachmentRepo.UpdateAttachment(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TearDown]
        public void TearDownTest()
        {
            DeleteAttachment();
        }

        private void DeleteAttachment()
        {
            foreach (int attachmentId in _attachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentById(attachmentId);
            }
        }

    }
}

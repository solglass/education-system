using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class AttachmentTests : BaseTest
    {
        private IAttachmentRepository _attachmentRepo;
        private List<int> _attachmentIdList;

        [SetUp]
        public void OneTimeSetUpTest()
        {
            _attachmentRepo = new AttachmentRepository(_options);
            _attachmentIdList = new List<int>();
        }

        [TestCase(1)]
        public void AttachmentAddPositiveTest(int mockId)
        {
            //Given
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();

            var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
            Assert.Greater(addedAttachmentId, 0);

            _attachmentIdList.Add(addedAttachmentId);
            dto.Id = addedAttachmentId;

            //When
            var actual = _attachmentRepo.GetAttachmentById(addedAttachmentId);

            //Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(3)]
        public void AttachmentAdd_EmptyAttachment_NegativeTest(int mockId)
        {
            //Given
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            //When, Then
            try
            {
                var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
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
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            //When, Then
            try
            {
                var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
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
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
            
            dto = new AttachmentDto
            {
                Id = addedAttachmentId,
                Path = "Update Test",
                AttachmentType = AttachmentType.Link
            };

            var affectedRowsCount = _attachmentRepo.UpdateAttachment(dto);
            _attachmentIdList.Add(addedAttachmentId);

            //When
            var actual = _attachmentRepo.GetAttachmentById(addedAttachmentId);

            //Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);

        }
        [TestCase(1)]
        public void AttachmentDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(mockId).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
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
            var dto = (AttachmentDto)AttachmentMockGetter.GetAttachmentDtoMock(1).Clone();
            var addedAttachmentId = _attachmentRepo.AddAttachment(dto);
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
        public void TearDowTest()
        {
            DeleteAttachment();
        }

        private void DeleteAttachment()
        {
            foreach (int tagId in _attachmentIdList)
            {
                _attachmentRepo.DeleteAttachmentById(tagId);
            }
        }

    }
}

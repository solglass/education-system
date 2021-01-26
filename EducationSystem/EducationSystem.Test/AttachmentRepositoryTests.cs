
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;

namespace NUnitTestProject
{
    public class AttachmentsTests
    {
        private int attachmentId;
        private int attachmentTypeId;
        [SetUp]
        public void AttachmentsTestsSetup()
        {

        }

        [TestCase(1)]
        public void Attachment_Add(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            attachmentId = aRepo.AddAttachment(expected);
            AttachmentDto actual = aRepo.GetAttachmentById(attachmentId);

            Assert.AreEqual(expected, actual);

        }

        [TestCase(1)]
        public void AttachmentType_Add(int dtoMockNumber)
        {
            AttachmentTypeDto expected = GetMockAttachmentType_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            attachmentTypeId = aRepo.AddAttachmentType(expected);
            AttachmentTypeDto actual = aRepo.GetAttachmentTypeById(attachmentTypeId);

            Assert.AreEqual(expected, actual);

        }

        [TearDown]
        public void AttachmentsTestsTearDown()
        {
            AttachmentRepository aRepo = new AttachmentRepository();
            if (attachmentId != 0)
            {
                aRepo.DeleteAttachmentById(attachmentId);
            }
            if (attachmentTypeId != 0)
            aRepo.DeleteAttachmentTypeById(attachmentTypeId);
        }

        public AttachmentDto GetMockAttachment_Add(int n)
        {
            switch (n)
            {
                case 1:
                    AttachmentDto attachmentDto = new AttachmentDto();
                    attachmentDto.Path = "C_TESTCASE1";
                    AttachmentTypeDto attachmentTypeDto = new AttachmentTypeDto();
                    attachmentTypeDto.Id = 15;
                    attachmentTypeDto.Name = "IMAGE";
                    attachmentDto.AttachmentType = attachmentTypeDto;
                    return attachmentDto;
                default:
                    throw new Exception();
            }
        }

        public AttachmentTypeDto GetMockAttachmentType_Add(int n)
        {
            switch (n)
            {
                case 1:

                    AttachmentTypeDto attachmentTypeDto = new AttachmentTypeDto();
                    attachmentTypeDto.Id = 15;
                    attachmentTypeDto.Name = "IMAGE";
                    return attachmentTypeDto;
                default:
                    throw new Exception();
            }
        }
    }
}

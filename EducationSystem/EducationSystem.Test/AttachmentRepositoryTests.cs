
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;

namespace NUnitTestProject
{
    public class AttachmentsTests
    {
        private int _attachmentId;
        private int _attachmentTypeId;
        [SetUp]
        public void AttachmentsTestsSetup()
        {
//setup attachment add
//check if attachment type exists
        }

        [TestCase(1)]
        public void Attachment_Add(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            _attachmentId = aRepo.AddAttachment(expected);
            AttachmentDto actual = aRepo.GetAttachmentById(_attachmentId);

            Assert.AreEqual(expected, actual);

        }

        [TestCase(1)]
        public void AttachmentType_Add(int dtoMockNumber)
        {
            AttachmentTypeDto expected = GetMockAttachmentType_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            _attachmentTypeId = aRepo.AddAttachmentType(expected);
            AttachmentTypeDto actual = aRepo.GetAttachmentTypeById(_attachmentTypeId);

            Assert.AreEqual(expected, actual);

        }

        [TearDown]
        public void AttachmentsTestsTearDown()
        {
            AttachmentRepository aRepo = new AttachmentRepository();
            if (_attachmentId != 0)
            {
                aRepo.DeleteAttachmentById(_attachmentId);
            }
            if (_attachmentTypeId != 0)
            aRepo.DeleteAttachmentTypeById(_attachmentTypeId);
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

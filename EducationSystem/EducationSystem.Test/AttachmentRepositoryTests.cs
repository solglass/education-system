
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using EducationSystem.Core.Enums;

namespace EducationSystem.Data.Tests
{
    public class AttachmentsTests
    {
        private List<int> _attachmentId;
        private AttachmentRepository aRepo;
        [SetUp]
        public void AttachmentsTestsSetup()
        {
            _attachmentId = new List<int>();
            AttachmentDto expected = GetMockAttachment_Add(1);


        }

        [TestCase(1)]
        public void Attachment_Add(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            _attachmentId.Add(aRepo.AddAttachment(expected));
            if (_attachmentId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                AttachmentDto actual = aRepo.GetAttachmentById(_attachmentId[_attachmentId.Count - 1]);
                Assert.AreEqual(expected, actual);
            }

        }


        [TestCase(1)]
        public void Attachment_Delete(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            _attachmentId.Add(aRepo.AddAttachment(expected));
            if (_attachmentId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                int newId = _attachmentId[_attachmentId.Count - 1];
                aRepo.DeleteAttachmentById(newId);
                AttachmentDto actual = aRepo.GetAttachmentById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(1)]
        public void Attachment_Update(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            _attachmentId.Add(aRepo.AddAttachment(expected));
            if (_attachmentId.Count == 0) { Assert.Fail("Attachment addition failed"); }

            int newId = _attachmentId[_attachmentId.Count - 1];
            expected.Path = "B\\TESTCASE2";
            expected.Id = newId;
            aRepo.ModifyAttachment(expected);
            AttachmentDto actual = aRepo.GetAttachmentById(newId);

            Assert.AreEqual(expected, actual);

        }



        [TearDown]
        public void AttachmentsTestsTearDown()
        {
            foreach (int elem in _attachmentId)
            {
                aRepo.DeleteAttachmentById(elem);
            }

        }

        public AttachmentDto GetMockAttachment_Add(int n)
        {
            switch (n)
            {
                case 1:
                    AttachmentDto attachmentDto = new AttachmentDto();
                    attachmentDto.Path = "C\\TESTCASE1";
                    AttachmentType attachmentType = AttachmentType.File;
                    attachmentDto.AttachmentType = attachmentType;
                    return attachmentDto;
                default:
                    throw new Exception();
            }
        }

    }
}

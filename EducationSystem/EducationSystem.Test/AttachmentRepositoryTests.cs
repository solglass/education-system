
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;

namespace NUnitTestProject
{
    public class AttachmentsTests
    {
        private List<int> _attachmentId;
        private List<int> _attachmentTypeId;
        [SetUp]
        public void AttachmentsTestsSetup()
        {
            _attachmentId = new List<int>();
            _attachmentTypeId = new List<int>();
            AttachmentDto expected = GetMockAttachment_Add(1);
            AttachmentRepository aRepo = new AttachmentRepository();
            AttachmentTypeDto attachmentType = aRepo.GetAttachmentTypeById(expected.AttachmentType.Id);
            if (attachmentType == null)
            { Assert.Fail("Attachment type used in mock was not found"); }
           
        }

        [TestCase(1)]
        public void Attachment_Add(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            _attachmentId.Add(aRepo.AddAttachment(expected));
            if (_attachmentId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else {
                AttachmentDto actual = aRepo.GetAttachmentById(_attachmentId[_attachmentId.Count - 1]);
                Assert.AreEqual(expected, actual); }

        }

        [TestCase(1)]
        public void AttachmentType_Add(int dtoMockNumber)
        {
            AttachmentTypeDto expected = GetMockAttachmentType_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            _attachmentTypeId.Add(aRepo.AddAttachmentType(expected.Name));
            if (_attachmentTypeId.Count == 0) { Assert.Fail("Attachment type addition failed"); }
            else
            {
                AttachmentTypeDto actual = aRepo.GetAttachmentTypeById(_attachmentTypeId[_attachmentTypeId.Count - 1]);

                Assert.AreEqual(expected, actual);
            }

        }

        [TestCase(1)]
        public void Attachment_Update(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
            _attachmentId.Add(aRepo.AddAttachment(expected));
            if (_attachmentId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                int newId = _attachmentId[_attachmentId.Count - 1];
                expected.Path = "B_TESTCASE2";
                expected.Id = newId;
                aRepo.ModifyAttachment(expected);
                AttachmentDto actual = aRepo.GetAttachmentById(newId);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1)]
        public void AttachmentType_Update(int dtoMockNumber)
        {
            AttachmentTypeDto expected = GetMockAttachmentType_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();

            _attachmentTypeId.Add(aRepo.AddAttachmentType(expected.Name));
            if (_attachmentTypeId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                int newId = _attachmentTypeId[_attachmentTypeId.Count - 1];
                expected.Name = "TEST";
                expected.Id = newId;
                aRepo.ModifyAttachmentType(expected.Id,expected.Name);
                AttachmentTypeDto actual = aRepo.GetAttachmentTypeById(newId);

                Assert.AreEqual(expected, actual);
            }

        }

        [TestCase(1)]
        public void AttachmentType_Delete(int dtoMockNumber)
        {
            AttachmentTypeDto expected = GetMockAttachmentType_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();

            _attachmentTypeId.Add(aRepo.AddAttachmentType(expected.Name));
            if (_attachmentTypeId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                int newId = _attachmentTypeId[_attachmentTypeId.Count - 1];
                aRepo.DeleteAttachmentTypeById(newId);
                AttachmentTypeDto actual = aRepo.GetAttachmentTypeById(newId);

                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }

        }

        [TestCase(1)]
        public void Attachment_Delete(int dtoMockNumber)
        {
            AttachmentDto expected = GetMockAttachment_Add(dtoMockNumber);
            AttachmentRepository aRepo = new AttachmentRepository();
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



        [TearDown]
        public void AttachmentsTestsTearDown()
        {
            AttachmentRepository aRepo = new AttachmentRepository();
           foreach( int elem in _attachmentId)
            {
                aRepo.DeleteAttachmentById(elem);
            }
            foreach (int elem in _attachmentTypeId)
            aRepo.DeleteAttachmentTypeById(elem);
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

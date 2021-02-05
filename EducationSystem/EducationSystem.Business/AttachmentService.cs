using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
   public class AttachmentService
    {

        private AttachmentRepository _attachmentRepository;

        public AttachmentService()
        {
            _attachmentRepository = new AttachmentRepository();
        }

        public List<AttachmentDto> GetAttachments() { return _attachmentRepository.GetAttachments(); }
        public AttachmentDto GetAttachmentById(int id) { return _attachmentRepository.GetAttachmentById(id); }
        public int ModifyAttachment(AttachmentDto attachmentDto) { return _attachmentRepository.ModifyAttachment(attachmentDto); }
        public int AddAttachment(AttachmentDto attachmentDto) { return _attachmentRepository.AddAttachment(attachmentDto); }
        public int DeleteAttachmentById(int id) { return _attachmentRepository.DeleteAttachmentById(id); }
        public List<AttachmentTypeDto> GetAttachmentTypes() { return _attachmentRepository.GetAttachmentTypes(); }
        public AttachmentTypeDto GetAttachmentTypeById(int id) { return _attachmentRepository.GetAttachmentTypeById(id); }
        public int ModifyAttachmentType(int id, string name) { return _attachmentRepository.ModifyAttachmentType(id, name); }
        public int AddAttachmentType(string name) { return _attachmentRepository.AddAttachmentType(name); }
        public int DeleteAttachmentTypeById(int id) { return _attachmentRepository.DeleteAttachmentTypeById(id); }

    }
}

using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class AttachmentService : IAttachmentService
    {

        private IAttachmentRepository _attachmentRepository;

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        public AttachmentDto GetAttachmentById(int id)
        {
            return _attachmentRepository.GetAttachmentById(id);
        }
        public int ModifyAttachment(AttachmentDto attachmentDto, int id)
        {
            return _attachmentRepository.ModifyAttachment(attachmentDto, id);
        }
        public int AddAttachment(AttachmentDto attachmentDto)
        {
            return _attachmentRepository.AddAttachment(attachmentDto);
        }
        public int DeleteAttachmentById(int id)
        {
            return _attachmentRepository.DeleteAttachmentById(id);
        }

        public int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId)
        {
            return _attachmentRepository.AddAttachmentToHomeworkAttempt(attachmentDto, homeworkAttemptId);
        }
        public int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId)
        {
            return _attachmentRepository.AddAttachmentToComment(attachmentDto, commentId);
        }

        public int DeleteHomeworkAttemptAttachment(int attachmentId, int homeworkAttemptId)
        {
          return _attachmentRepository.DeleteHomeworkAttemptAttachment(attachmentId, homeworkAttemptId);
        }

        public int DeleteCommentAttachment(int attachmentId, int commentId)
        {
            return _attachmentRepository.DeleteCommentAttachment(attachmentId, commentId);
        }
    }
}

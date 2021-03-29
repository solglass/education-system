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
        public int UpdateAttachment(AttachmentDto attachmentDto)
        {
            return _attachmentRepository.UpdateAttachment(attachmentDto);
        }

        public int AddAttachment(AttachmentDto attachmentDto)
        {
            return _attachmentRepository.AddAttachment(attachmentDto);
        }
        public int DeleteAttachmentById(int id)
        {
            return _attachmentRepository.DeleteAttachmentById(id);
        }

        public int AddHomeworkAttemptAttachment(AttachmentDto attachmentDto, int homeworkAttemptId)
        {
            var attachmentId = _attachmentRepository.AddAttachment(attachmentDto);
            return _attachmentRepository.AddAttachmentToHomeworkAttempt(attachmentId, homeworkAttemptId);
        }
        public int AddCommentAttachment(AttachmentDto attachmentDto, int commentId)
        {
            var attachmentId = _attachmentRepository.AddAttachment(attachmentDto);
            return _attachmentRepository.AddAttachmentToComment(attachmentId, commentId);
        }



        public int DeleteHomeworkAttemptAttachment(int attachmentId, int homeworkAttemptId)

        {

            var result = _attachmentRepository.DeleteAttachmentFromHomeworkAttempt(attachmentId, homeworkAttemptId);

            return _attachmentRepository.DeleteAttachmentById(attachmentId);

                

        }



        public int DeleteCommentAttachment(int attachmentId, int commentId)

        {

            var result = _attachmentRepository.DeleteAttachmentFromComment(attachmentId, commentId);

            return _attachmentRepository.DeleteAttachmentById(attachmentId);

        }
    }
}

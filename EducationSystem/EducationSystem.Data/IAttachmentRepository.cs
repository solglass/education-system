using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IAttachmentRepository
    {
        int AddAttachment(AttachmentDto attachmentDto);
        int AddAttachmentToComment(int commentId, int attachmentId);
        int AddAttachmentToHomeworkAttempt(int homeworkAttemptId, int attachmentId);
        int DeleteAttachmentById(int id);
        int DeleteAttachmentFromHomeworkAttempt(int attachmentId, int homeworkAttemptId);
        int DeleteAttachmentFromComment(int attachmentId, int commentId);
        AttachmentDto GetAttachmentById(int id);
        int UpdateAttachment(AttachmentDto attachmentDto);
        List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id);
        List<AttachmentDto> GetAttachmentsByCommentId(int id);
    }
}
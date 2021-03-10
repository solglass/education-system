using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;

namespace EducationSystem.Data
{
    public interface IAttachmentRepository
    {
        int AddAttachment(AttachmentDto attachmentDto);
        int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId);
        int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId);
        int DeleteAttachmentById(int id);
        int DeleteHomeworkAttemptAttachment(int attachmentId, int homeworkAttemptId);
        int DeleteCommentAttachment(int attachmentId, int commentId);
        AttachmentDto GetAttachmentById(int id);
        int ModifyAttachment(AttachmentDto attachmentDto, int id);


    }
}
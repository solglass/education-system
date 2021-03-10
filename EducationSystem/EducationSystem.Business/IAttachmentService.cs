using EducationSystem.Data.Models;

namespace EducationSystem.Business
{
    public interface IAttachmentService
    {
        int AddAttachment(AttachmentDto attachmentDto);
        int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId);
        int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId);
        int DeleteAttachmentById(int id);

        int DeleteHomeworkAttemptAttachment(int attachmentId, int attemptId);

        int DeleteCommentAttachment(int attachmentId, int commentId);

        AttachmentDto GetAttachmentById(int id);
        int ModifyAttachment(AttachmentDto attachmentDto, int id);
    }
}
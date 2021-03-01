using EducationSystem.Data.Models;

namespace EducationSystem.Business
{
    public interface IAttachmentService
    {
        int AddAttachment(AttachmentDto attachmentDto);
        int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId);
        int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId);
        int DeleteAttachmentById(int id);
        AttachmentDto GetAttachmentById(int id);
        int ModifyAttachment(AttachmentDto attachmentDto);
    }
}
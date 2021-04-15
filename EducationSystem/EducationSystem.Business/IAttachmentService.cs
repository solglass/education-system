using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EducationSystem.Business
{
    public interface IAttachmentService
    {
        int AddAttachment(AttachmentDto attachmentDto);
        int AddCommentAttachment(AttachmentDto attachmentDto, int commentId);
        int AddHomeworkAttemptAttachment(AttachmentDto attachmentDto, int homeworkAttemptId);
        int DeleteAttachmentById(int id);

        int DeleteHomeworkAttemptAttachment(int attachmentId, int attemptId);

        int DeleteCommentAttachment(int attachmentId, int commentId);

        AttachmentDto GetAttachmentById(int id);
        int UpdateAttachment(AttachmentDto attachmentDto);
    }
}
using System.Collections.Generic;

namespace EducationSystem.Data.Models
{
    public interface IHomeworkAttemptRepository
    {
        int AddHomeworkAttempt(string comment, int userId, int homeworkAttemptId, int statusId);
        int AddHomeworkAttempt_Attachment(HomeworkAttempt_AttachmentDto newObject);
        int DeleteHomeworkAttempt(int id);
        int DeleteHomeworkAttempt_Attachment(int homeworkAttemptId, int attachmentId);
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        List<HomeworkAttemptDto> GetHomeworkAttempts();
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id);
        HomeworkAttempt_AttachmentDto GetHomeworkAttempt_AttachmentById(int id);
        int UpdateHomeworkAttempt(int id, string comment, int statusId);
    }
}
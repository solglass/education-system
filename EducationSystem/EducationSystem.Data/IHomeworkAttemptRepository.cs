using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IHomeworkAttemptRepository
    {
        int AddHomeworkAttempt(string comment, int userId, int homeworkAttemptId, int statusId);
        int AddHomeworkAttempt_Attachment(HomeworkAttempt_AttachmentDto newObject);
        int DeleteHomeworkAttempt_Attachment(int homeworkAttemptId, int attachmentId);
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        List<HomeworkAttemptDto> GetHomeworkAttempts();
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id);
        HomeworkAttempt_AttachmentDto GetHomeworkAttempt_AttachmentById(int id);
        int UpdateHomeworkAttempt(int id, string comment, int statusId);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId);
    }
}
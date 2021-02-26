using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IHomeworkService
    {
        int AddHomework(HomeworkDto homeworkDto);
        int AddHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
        int AddHomework_Theme(int homeworkId, int themeId);
        int DeleteHomework(int id);
        int DeleteHomeworkAttempt(int id);
        int DeleteHomeworkAttemptAttachment(int homeworkAttemptId, int attachmentId);
        int DeleteHomeworkAttemptStatus(int id);
        int DeleteHomework_Theme(int homeworkId, int themeId);
        CommentDto GetCommentById(int id);
        List<CommentDto> GetComments();
        List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id);
        List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatuses();
        HomeworkDto GetHomeworkById(int id);
        List<HomeworkDto> GetHomeworksByGroupId(int groupId);
        List<HomeworkDto> GetHomeworksByTagId(int tagId);
        List<HomeworkDto> GetHomeworksByThemeId(int themeId);
        int UpdateHomework(HomeworkDto homeworkDto);
        int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
    }
}
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IHomeworkService
    {
        int AddHomework(HomeworkDto homeworkDto);
        int AddHomeworkAttempt(int homeworkId, HomeworkAttemptDto homeworkAttempt);
        int AddHomework_Theme(int homeworkId, int themeId);
        int DeleteHomework(int id);
        int DeleteHomeworkAttempt(int id);
        int DeleteHomework_Theme(int homeworkId, int themeId);
        CommentDto GetCommentById(int id);
        List<CommentDto> GetComments();
        List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id);
        HomeworkDto GetHomeworkById(int id);
        List<HomeworkDto> GetHomeworksByGroupId(int groupId);
        List<HomeworkDto> GetHomeworksByTagId(int tagId);
        List<HomeworkDto> GetHomeworksByThemeId(int themeId);
        int UpdateHomework(int homeworkId, HomeworkDto homeworkDto);
        int UpdateHomeworkAttempt(int attemptId, HomeworkAttemptDto homeworkAttempt);
        int DeleteComment(int id);
        int RecoverComment(int id);
        int RecoverHomework(int id);
        int RecoverHomeworkAttempt(int id);
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        int AddComment(int attemptId, CommentDto comment);
        int UpdateComment(int attemptId, int commentId, CommentDto comment);
        int AddHomeworkTag(int homeworkId,int tagId);
        int DeleteHomeworkTag(int homeworkId, int tagId);
    }
}
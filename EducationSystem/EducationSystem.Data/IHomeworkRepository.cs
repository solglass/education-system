using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IHomeworkRepository
    {
        int AddComment(CommentDto comment);
        int AddComment_Attachment(Comment_AttachmentDto NewObject);
        int AddHomework(HomeworkDto homework);
        int AddHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
        int AddHomeworkAttemptStatus(HomeworkAttemptStatusDto homeworkAttemptStatus);
        int AddHomework_Theme(int homeworkId, int themeId);
        List<CommentDto> Comment_SelectByHomeworkId(int id);
        int DeleteComment(int id);
        void DeleteComment_AttachmentById(int commentId, int attachmentId);
        int DeleteHomework(int id);
        int DeleteHomeworkAttempt(int id);
        int DeleteHomeworkAttemptStatus(int id);
        int DeleteHomework_Theme(int homeworkId, int themeId);
        List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id);
        CommentDto GetCommentById(int id);
        List<CommentDto> GetComments();
        List<CommentDto> GetCommentsByHomeworkAttemptId(int id);
        Comment_AttachmentDto GetComment_AttachmentById(int id);
        List<Comment_AttachmentDto> GetComment_Attachments();
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id);
        List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatuses();
        HomeworkDto GetHomeworkById(int id);
        List<HomeworkDto> GetHomeworksByGroupId(int groupId);
        List<HomeworkDto> GetHomeworksByTagId(int tagId);
        List<HomeworkDto> GetHomeworksByThemeId(int themeId);
        int HardDeleteComment(int id);
        int HardDeleteHomework(int id);
        int HardDeleteHomeworkAttempt(int id);
        int HardDeleteHomeworkAttemptStatus(int id);
        int UpdateComment(CommentDto commentDto);
        int UpdateHomework(HomeworkDto homework);
        int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
    }
}
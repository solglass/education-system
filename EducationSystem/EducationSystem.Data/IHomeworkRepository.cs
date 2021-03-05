﻿using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IHomeworkRepository
    {
        int AddComment(CommentDto comment);
        int AddHomework(HomeworkDto homework);
        int AddHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
        int AddHomework_Theme(int homeworkId, int themeId);
        List<CommentDto> Comment_SelectByHomeworkId(int id);
        int DeleteHomework_Theme(int homeworkId, int themeId);
        int DeleteOrRecoverComment(int id, bool isDeleted);
        int DeleteOrRecoverHomework(int id, bool isDeleted);
        int DeleteOrRecoverHomeworkAttempt(int id, bool isDeleted);
        List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id);
        CommentDto GetCommentById(int id);
        List<CommentDto> GetComments();
        List<CommentDto> GetCommentsByHomeworkAttemptId(int id);
        Comment_AttachmentDto GetComment_AttachmentById(int id);
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id);
        HomeworkDto GetHomeworkById(int id);
        int HardDeleteComment(int id);
        int HardDeleteHomework(int id);
        int HardDeleteHomeworkAttempt(int id);
        List<HomeworkDto> SearchHomeworks(int? groupId, int? themeId, int? tagId);
        int UpdateComment(CommentDto commentDto);
        int UpdateHomework(HomeworkDto homework);
        int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
        int HomeworkTagAdd(HomeworkTagDto Tag);
        int HomeworkTagDelete(int homeworkId, int tagId);
    }
}
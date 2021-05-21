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
        int DeleteHomework_Theme(int homeworkId, int themeId);
        int DeleteOrRecoverComment(int id, bool isDeleted);
        int DeleteOrRecoverHomework(int id, bool isDeleted);
        int DeleteOrRecoverHomeworkAttempt(int id, bool isDeleted);
        CommentDto GetCommentById(int id);
        HomeworkAttemptDto GetHomeworkAttemptById(int id);
        HomeworkDto GetHomeworkById(int id);
        int HardDeleteComment(int id);
        int HardDeleteHomework(int id);
        int HardDeleteHomeworkAttempt(int id);
        List<HomeworkDto> SearchHomeworks(int? courseId = null, int? themeId = null, int? tagId = null);
        List<HomeworkDto> GetHomeworksByGroupId(int groupId);
        List<CommentDto> SearchComments(int? homeworkAttamptId, int? homeworkId);
        int UpdateComment(CommentDto commentDto);
        int UpdateHomework(HomeworkDto homework);
        int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt);
        int HomeworkTagAdd(int homeworkId, int tagId);
        int HomeworkTagDelete(int homeworkId, int tagId);
        int AddHomework_Group(int homeworkId, int groupId);
        int DeleteHomework_Group(int homeworkId, int groupId);
        List<HomeworkDto> GetHomeworks();
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id);
        List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId);
        List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id);
    }
}
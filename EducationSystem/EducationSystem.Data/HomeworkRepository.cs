using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class HomeworkRepository : BaseRepository
    {
        public HomeworkRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<HomeworkDto> GetHomeworks()
        {
            var homework = _connection
                 .Query<HomeworkDto>("dbo.Homework_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homework;
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            var homework = _connection
                .Query<HomeworkDto>("dbo.Homework_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homework;
        }

        public int AddHomework(string description, DateTime StartDate, DateTime DeadlineDate, int GroupID, bool IsOptional)
        {
            var result = _connection
                .Execute("dbo.Homework_Add",
                new
                {
                    description,
                    StartDate,
                    DeadlineDate,
                    GroupID,
                    IsOptional
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateHomework(int id, string description, DateTime StartDate, DateTime DeadlineDate, int GroupID, bool IsOptional, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Homework_Update",
                new
                {
                    id,
                    description,
                    StartDate,
                    DeadlineDate,
                    GroupID,
                    IsOptional,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteHomework(int id)
        {
            var result = _connection
                .Execute("dbo.Homework_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public List<HomeworkAttemptDto> GetHomeworkAttempts()
        {
            var homeworkAttempt = _connection
                .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homeworkAttempt;
        }

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            var homeworkAttempt = _connection
                .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homeworkAttempt;
        }

        public int AddHomeworkAttempt(string comment, int userId, int homeworkAttemptId, int statusId)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Add",
                new { comment, userId, homeworkAttemptId, statusId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateHomeworkAttempt(int id, string comment, int userId, int homeworkId, int statusId)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Update",
                new { id, comment, userId, homeworkId, statusId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteHomeworkAttempt(int id)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Delete",
                new { id },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<CommentDto> GetComments()
        {
            var comment = _connection
                .Query<CommentDto>("dbo.Comment_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return comment;
        }

        public CommentDto GetCommentById(int id)
        {
            var comment = _connection
                .Query<CommentDto>("dbo.Comment_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return comment;
        }

        public List<Homework_ThemeDto> GetHomework_Themes()
        {
            var result = _connection
                 .Query<Homework_ThemeDto>("dbo.Homework_Theme_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }

        public int DeleteHomework_Theme(int id)
        {
            var result = _connection
                .Execute("dbo.Homework_Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatus()
        {
            var result = _connection
                 .Query<HomeworkAttemptStatusDto>("dbo.HomeworkAttemptStatus_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }

        public int DeleteHomeworkAttemptStatus(int id)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttemptStatus_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

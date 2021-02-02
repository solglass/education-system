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


        public class Comment_AttachmentRepository
        {
            private SqlConnection _connection;

            private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
            public Comment_AttachmentRepository()
            {
                _connection = new SqlConnection(_connectionString);
            }

            public List<Comment_AttachmentDto> GetComment_Attachments()
            {
                var data = _connection
                    .Query<Comment_AttachmentDto>("dbo.Comment_Attachment_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                    .ToList();
                return data;

            }


            public Comment_AttachmentDto GetComment_AttachmentById(int id)
            {
                var data = _connection
                    .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }

            public void DeleteComment_AttachmentById(int id)
            {
                var data = _connection
                    .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }

            public Comment_AttachmentDto AddComment_Attachment(Comment_AttachmentDto NewObject)
            {
                var data = _connection
                    .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Add",
                    new
                    {
                        commentId = NewObject.CommentId,
                        attachmentId = NewObject.AttachmentId
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
                return data;

            }

        }
    }
}

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


        //public List<HomeworkAttemptDto> GetHomeworkAttempts()
        //{
        //    var homeworkAttempt = _connection
        //        .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
        //        .ToList();
        //    return homeworkAttempt;
        //}

        public List<HomeworkAttemptDto> GetHomeworkAttempts()
        {
            var hwAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var commentDictionary = new Dictionary<int, CommentDto>();
            var hwAttempts = _connection
                .Query<HomeworkAttemptDto, UserDto, HomeworkAttemptStatusDto, CommentDto, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectAll",
                (hwAttempt, user, hwAttemptStatus, comment) =>
                {
                    if (!hwAttemptDictionary.TryGetValue(hwAttempt.Id, out HomeworkAttemptDto hwAttemptEntry))
                    {
                        hwAttemptEntry =  hwAttempt;
                        hwAttemptEntry.Author = user;
                        hwAttemptEntry.HomeworkAttemptStatus = hwAttemptStatus;
                        hwAttemptEntry.Comments = new List<CommentDto>();
                        hwAttemptDictionary.Add(hwAttemptEntry.Id, hwAttemptEntry);
                    }
                    if (comment != null && !commentDictionary.TryGetValue(hwAttempt.Id, out CommentDto commentEntry))
                    {
                        commentEntry = comment;
                        hwAttemptEntry.Comments.Add(comment);
                        commentDictionary.Add(comment.Id, commentEntry);
                    }
                    return hwAttemptEntry;
                },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return hwAttempts;
        }

        //public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        //{
        //    var homeworkAttempt = _connection
        //        .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
        //        .FirstOrDefault();
        //    return homeworkAttempt;
        //}

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            var commentDictionary = new Dictionary<int, CommentDto>();
            var hwAttemptEntry = new HomeworkAttemptDto();
            var hwAttempt = _connection
                .Query<HomeworkAttemptDto, UserDto, HomeworkAttemptStatusDto, CommentDto, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectById",
                (hwAttempt, user, hwAttemptStatus, comment) =>
                {
                    if (hwAttemptEntry.Id == 0)
                    {
                        hwAttemptEntry = hwAttempt;
                        hwAttemptEntry.Author = user;
                        hwAttemptEntry.HomeworkAttemptStatus = hwAttemptStatus;
                        hwAttemptEntry.Comments = new List<CommentDto>();
                    }
                    if (comment != null && !commentDictionary.TryGetValue(hwAttempt.Id, out CommentDto commentEntry))
                    {
                        commentEntry = comment;
                        hwAttemptEntry.Comments.Add(comment);
                        commentDictionary.Add(comment.Id, commentEntry);
                    }
                    return hwAttemptEntry;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return hwAttempt;
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
    }
}

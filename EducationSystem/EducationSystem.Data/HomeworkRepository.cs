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
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var groupDictionary = new Dictionary<int, GroupDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var HomeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homeworks = _connection
                .Query<HomeworkDto, TagDto, HomeworkAttemptDto, HomeworkAttemptStatusDto, UserDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectById",
                    (homework, tag, homeworkAttempt, homeworkAttemptStatus, user, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Tags = new List<TagDto>();
                            homeworkEntry.Themes = new List<ThemeDto>();
                            homeworkEntry.HomeworkAttempts = new List<HomeworkAttemptDto>();
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                        }

                        if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                        {
                            tagEntry = tag;
                            homeworkEntry.Tags.Add(tagEntry);
                            tagDictionary.Add(tagEntry.Id, tagEntry);
                        }

                        if (homeworkAttempt != null && homeworkAttemptStatus != null && user != null && !HomeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = homeworkAttemptStatus;
                            homeworkEntry.HomeworkAttempts.Add(homeworkAttemptEntry);
                            HomeworkAttemptDictionary.Add(homeworkAttemptEntry.Id, homeworkAttemptEntry);
                        }

                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            homeworkEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return homeworkEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return homeworks;
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var homeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homework = _connection
                .Query<HomeworkDto, TagDto, HomeworkAttemptDto, HomeworkAttemptStatusDto, UserDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectById",
                    (homework, tag, homeworkAttempt, homeworkAttemptStatus, user, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Tags = new List<TagDto>();
                            homeworkEntry.Themes = new List<ThemeDto>();
                            homeworkEntry.HomeworkAttempts = new List<HomeworkAttemptDto>();
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                        }

                        if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                        {
                            tagEntry = tag;
                            homeworkEntry.Tags.Add(tagEntry);
                            tagDictionary.Add(tagEntry.Id, tagEntry);
                        }

                        if (homeworkAttempt != null && homeworkAttemptStatus != null && user != null && !homeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = homeworkAttemptStatus;
                            homeworkEntry.HomeworkAttempts.Add(homeworkAttemptEntry);
                            homeworkAttemptDictionary.Add(homeworkAttemptEntry.Id, homeworkAttemptEntry);
                        }

                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            homeworkEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return homeworkEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homework;
        }

        public int AddHomework(HomeworkDto homework)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Add",
                new
                {

                    description = homework.Description,
                    StartDate = homework.StartDate,
                    DeadlineDate = homework.StartDate,
                    GroupID = homework.GroupId,
                    IsOptional = homework.IsOptional
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }

        public int UpdateHomework(HomeworkDto homework)
        {
            var result = _connection
                .Execute("dbo.Homework_Update",
                new
                {
                    id = homework.Id,
                    description = homework.Description,
                    StartDate = homework.StartDate,
                    DeadlineDate = homework.DeadlineDate,
                    GroupID = homework.GroupId,
                    IsOptional = homework.IsOptional,
                    isDeleted = homework.IsDeleted
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


        public int AddHomework_Theme(int homeworkId, int themeId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Theme_Add",
                new
                {
                    homeworkId,
                    themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
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

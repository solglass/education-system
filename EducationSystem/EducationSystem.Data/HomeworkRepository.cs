using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class HomeworkRepository : BaseRepository, IHomeworkRepository
    {
        public HomeworkRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var groupDictionary = new Dictionary<int, GroupDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var HomeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homeworks = _connection
                .Query<HomeworkDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectById",
                    (homework, group, tag, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Group = group;
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

                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            homeworkEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return homeworkEntry;
                    },
                    new
                    {
                        id
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homeworks;
        }

        public List<HomeworkDto> SearchHomeworks(int? groupId, int? themeId, int? tagId)
        {
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            _connection.Query<HomeworkDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_Search",
                    (homework, group, tag, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Group = group;
                            homeworkEntry.Tags = new List<TagDto>();
                            homeworkEntry.Themes = new List<ThemeDto>();
                            homeworkEntry.HomeworkAttempts = new List<HomeworkAttemptDto>();
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                            tagDictionary.Clear();
                            themeDictionary.Clear();
                        }

                        if (theme != null)
                        {
                            if (!themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                            {
                                themeEntry = theme;
                                homeworkEntry.Themes.Add(themeEntry);
                                themeDictionary.Add(themeEntry.Id, themeEntry);
                            }
                        }
                        if (tag != null)
                        {
                            if (!tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                            {
                                tagEntry = tag;
                                tagDictionary.Add(tagEntry.Id, tagEntry);
                                homeworkEntry.Tags.Add(tagEntry);
                            }
                        }
                        return homeworkEntry;
                    },
                    new
                    {
                        groupId = groupId,
                        themeId = themeId,
                        tagId = tagId
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            var homework = new List<HomeworkDto>();
            homeworkDictionary.AsList().ForEach(r => homework.Add(r.Value));
            return homework;
        }

        public int AddHomework(HomeworkDto homework)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Add",
                new
                {

                    description = homework.Description,
                    startDate = homework.StartDate,
                    deadlineDate = homework.DeadlineDate,
                    groupId = homework.Group.Id,
                    isOptional = homework.IsOptional
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
                    startDate = homework.StartDate,
                    deadlineDate = homework.DeadlineDate,
                    isOptional = homework.IsOptional
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteOrRecoverHomework(int id, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Homework_DeleteOrRecover",
                new
                {
                    id,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int HardDeleteHomework(int id)
        {
            var result = _connection
                .Execute("dbo.Homework_HardDelete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            //var commentDictionary = new Dictionary<int, CommentDto>();
            var hwAttemptEntry = new HomeworkAttemptDto();
            var hwAttempt = _connection
                .Query<HomeworkAttemptDto, UserDto, HomeworkDto, int, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectById",
                (attempt, user, homework, hwAttemptStatus) =>
                {
                    hwAttemptEntry = attempt;
                    hwAttemptEntry.Author = user;
                    hwAttemptEntry.Homework = homework;
                    hwAttemptEntry.HomeworkAttemptStatus = (HomeworkAttemptStatus)hwAttemptStatus;

                    return hwAttemptEntry;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return hwAttempt;
        }

        public int AddHomeworkAttempt(HomeworkAttemptDto homeworkAttempt)
        {
            var result = _connection
                .QuerySingle<int>("dbo.HomeworkAttempt_Add",
                new
                {
                    comment = homeworkAttempt.Comment,
                    userId = homeworkAttempt.Author.Id,
                    homeworkId = homeworkAttempt.Homework.Id,
                    statusId = (int)homeworkAttempt.HomeworkAttemptStatus
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Update",
                 new
                 {
                     id = homeworkAttempt.Id,
                     comment = homeworkAttempt.Comment,
                     author = homeworkAttempt.Author.Id,
                     homework = homeworkAttempt.Homework.Id,
                     homeworkAttemptStatus = (int)homeworkAttempt.HomeworkAttemptStatus
                 },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteOrRecoverHomeworkAttempt(int id, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_DeleteOrRecover",
                new
                {
                    id,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int HardDeleteHomeworkAttempt(int id)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_HardDelete",
                new { id },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int AddComment(CommentDto comment)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Comment_Add",
                new
                {
                    userId = comment.Author.Id,
                    homeworkAttemptId = comment.HomeworkAttempt.Id,
                    message = comment.Message
                },
                commandType: CommandType.StoredProcedure);
            return result;
        }


        public int DeleteOrRecoverComment(int id, bool isDeleted)
        {
            var result = _connection
                 .Execute("dbo.Comment_DeleteOrRecover",
                 new
                 {
                     id,
                     isDeleted
                 },
                 commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int HardDeleteComment(int id)
        {
            var result = _connection
                 .Execute("dbo.Comment_HardDelete",
                 new
                 {
                     id
                 },
                 commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<CommentDto> GetComments()
        {
            var commentDictionary = new Dictionary<int, CommentDto>();
            var userDictionary = new Dictionary<int, UserDto>();
            var homeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();


            var comments = _connection
                .Query<CommentDto, UserDto, HomeworkAttemptDto, int, CommentDto>(
                    "dbo.Homework_SelectById",
                    (comment, user, homeworkAttempt, homeworkAttemptStatus) =>
                    {
                        if (!commentDictionary.TryGetValue(comment.Id, out CommentDto commentEntry))
                        {
                            commentEntry = comment;
                            commentEntry.Author = new UserDto();
                            commentEntry.HomeworkAttempt = new HomeworkAttemptDto();
                            commentDictionary.Add(commentEntry.Id, commentEntry);
                        }

                        if (homeworkAttempt != null && homeworkAttemptStatus != 0 && user != null && !homeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = (HomeworkAttemptStatus)homeworkAttemptStatus;
                            commentEntry.HomeworkAttempt = homeworkAttemptEntry;
                            homeworkAttemptDictionary.Add(homeworkAttemptEntry.Id, homeworkAttemptEntry);
                        }
                        if (user != null && !userDictionary.TryGetValue(user.Id, out UserDto userEntry))
                        {
                            userEntry = user;
                            commentEntry.Author = user;
                            userDictionary.Add(userEntry.Id, userEntry);
                        }
                        return commentEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return comments;


        }

        public CommentDto GetCommentById(int id)
        {
            var commentDictionary = new Dictionary<int, CommentDto>();
            var userDictionary = new Dictionary<int, UserDto>();
            var homeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();


            var comment = _connection
                .Query<CommentDto, UserDto, HomeworkAttemptDto, int, CommentDto>(
                    "dbo.Homework_SelectById",
                    (comment, user, homeworkAttempt, homeworkAttemptStatus) =>
                    {
                        if (!commentDictionary.TryGetValue(comment.Id, out CommentDto commentEntry))
                        {
                            commentEntry = comment;
                            commentEntry.Author = new UserDto();
                            commentEntry.HomeworkAttempt = new HomeworkAttemptDto();
                            commentDictionary.Add(commentEntry.Id, commentEntry);
                        }

                        if (homeworkAttempt != null && homeworkAttemptStatus != 0 && user != null && !homeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = (HomeworkAttemptStatus)homeworkAttemptStatus;
                            commentEntry.HomeworkAttempt = homeworkAttemptEntry;
                            homeworkAttemptDictionary.Add(homeworkAttemptEntry.Id, homeworkAttemptEntry);
                        }
                        if (user != null && !userDictionary.TryGetValue(user.Id, out UserDto userEntry))
                        {
                            userEntry = user;
                            commentEntry.Author = user;
                            userDictionary.Add(userEntry.Id, userEntry);
                        }
                        return commentEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return comment;
        }

        public List<CommentDto> Comment_SelectByHomeworkId(int id)
        {
            var commentDictionary = new Dictionary<int, CommentDto>();
            var result = _connection.Query<CommentDto, UserDto, HomeworkAttemptDto, CommentDto>(
                "dbo.Comment_SelectByHomeworkId",
                (comment, user, attempt) =>
                {
                    if (!commentDictionary.TryGetValue(comment.Id, out CommentDto commentEntry))
                    {
                        commentEntry = comment;
                        commentEntry.Author = user;
                        comment.HomeworkAttempt = attempt;
                        commentDictionary.Add(commentEntry.Id, commentEntry);
                    }
                    return commentEntry;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int UpdateComment(CommentDto commentDto)
        {
            return _connection.Execute(
                "dbo.Comment_Update",
                new { commentDto.Id, commentDto.Message },
                commandType: CommandType.StoredProcedure);
        }


        public int AddHomework_Theme(int homeworkId, int themeId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Theme_Add",
                new
                {
                    homeworkId = homeworkId,
                    themeId = themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteHomework_Theme(int homeworkId, int themeId)
        {
            var result = _connection
                .Execute("dbo.Homework_Theme_Delete",
                new
                {
                    homeworkId = homeworkId,
                    themeId = themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
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

        public void DeleteComment_AttachmentById(int commentId, int attachmentId)
        {
            var data = _connection
                .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Delete", new { commentId, attachmentId }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public int AddComment_Attachment(Comment_AttachmentDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<int>("dbo.Comment_Attachment_Add",
                new
                {
                    commentId = NewObject.CommentId,
                    attachmentId = NewObject.AttachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }

        // todo: dapper logic
        // method doesn't work =(
        public List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id)
        {
            var hwAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var homeworkAttempts = _connection
                .Query<HomeworkAttemptDto, UserDto, int, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectByHomeworkId",
                (attempt, user, homeworkAttemptStatus) =>
                {
                    if (!hwAttemptDictionary.TryGetValue(attempt.Id, out HomeworkAttemptDto attemptEntry))
                    {
                        attemptEntry = attempt;
                        attemptEntry.Author = user;
                        attemptEntry.HomeworkAttemptStatus = (HomeworkAttemptStatus)homeworkAttemptStatus;
                        hwAttemptDictionary.Add(attemptEntry.Id, attemptEntry);
                    }
                    return attemptEntry;
                },
                new {homeworkId = id },
                splitOn: "id", 
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homeworkAttempts;
        }

        public List<CommentDto> GetCommentsByHomeworkAttemptId(int id)
        {
            var commentDictionary = new Dictionary<int, CommentDto>();
            var result = _connection.Query<CommentDto, UserDto, AttachmentDto, int, CommentDto>(
                "dbo.Comment_SelectByHomeworkAttemptId",
                (comment, user, attachment, attachmentType) =>
                {
                    if (!commentDictionary.TryGetValue(comment.Id, out var commentEntry))
                    {
                        commentEntry = comment;
                        commentEntry.Author = user;
                        commentEntry.Attachments = new List<AttachmentDto>();
                        commentDictionary.Add(commentEntry.Id, commentEntry);
                    }
                    if (attachment != null)
                    {
                        attachment.AttachmentType = (AttachmentType)attachmentType;
                        commentEntry.Attachments.Add(attachment);
                    }
                    return commentEntry;
                },
                new { AttemptId = id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }

        public List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id)
        {
            var attachmentDictionary = new Dictionary<int, AttachmentDto>();
            var comments = _connection
                .Query<AttachmentDto, int, AttachmentDto>
                ("dbo.Attachment_SelectByHomeworkAttemptId",
                (attachment, type) =>
                {
                    if (attachmentDictionary.TryGetValue(attachment.Id, out AttachmentDto attachmentEntry))
                    {
                        attachmentEntry = attachment;
                        attachmentEntry.AttachmentType = (AttachmentType)type;
                        attachmentDictionary.Add(attachmentEntry.Id, attachmentEntry);
                    }
                    return attachmentEntry;
                },
                new { id },
                splitOn: "AttachmentType",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return comments;
        }
    }
}

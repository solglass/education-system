using Dapper;
using EducationSystem.Core.Enums;
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

        public List<HomeworkDto> GetHomeworks()
        {
            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homework = _connection
                .Query<HomeworkDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectAll",
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

                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            homeworkEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                        {
                            tagEntry = tag;
                            homeworkEntry.Tags.Add(tagEntry);
                            tagDictionary.Add(tagEntry.Id, tagEntry);
                        }
                        return homeworkEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
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
                    groupId = homework.Group.Id,
                    isOptional = homework.IsOptional
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

        public List<HomeworkAttemptDto> GetHomeworkAttempts()
        {
            var hwAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var hwAttempts = _connection
                .Query<HomeworkAttemptDto, UserDto, HomeworkDto, HomeworkAttemptStatusDto, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectAll",
                (hwAttempt, user, homework, hwAttemptStatus) =>
                {
                    if (!hwAttemptDictionary.TryGetValue(hwAttempt.Id, out HomeworkAttemptDto hwAttemptEntry))
                    {
                        hwAttemptEntry =  hwAttempt;
                        hwAttemptEntry.Author = user;
                        hwAttemptEntry.Homework = homework;
                        hwAttemptEntry.HomeworkAttemptStatus = hwAttemptStatus;
                        hwAttemptDictionary.Add(hwAttemptEntry.Id, hwAttemptEntry);
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
            //var commentDictionary = new Dictionary<int, CommentDto>();
            var hwAttemptEntry = new HomeworkAttemptDto();
            var hwAttempt = _connection
                .Query<HomeworkAttemptDto, UserDto,  HomeworkDto, HomeworkAttemptStatusDto, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectById",
                (hwAttempt, user, homework, hwAttemptStatus) =>
                {
                    if (hwAttemptEntry.Id == 0)
                    {
                        hwAttemptEntry = hwAttempt;
                        hwAttemptEntry.Author = user;
                        hwAttemptEntry.Homework = homework;
                        hwAttemptEntry.HomeworkAttemptStatus = hwAttemptStatus;
                    }
                    
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
                    statusId = homeworkAttempt.HomeworkAttemptStatus.Id
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
                     homeworkAttemptStatus = homeworkAttempt.HomeworkAttemptStatus.Id
                 },
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
                    message = comment.Message,
                    author = comment.Author,
                    attempt = comment.HomeworkAttempt
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteComment(int id)
        {
            var result = _connection
                 .Execute("dbo.Comment_Delete",
                 new
                 {
                     id
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
                .Query<CommentDto, UserDto, HomeworkAttemptDto, HomeworkAttemptStatusDto, CommentDto>(
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

                        if (homeworkAttempt != null && homeworkAttemptStatus != null && user != null && !homeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = homeworkAttemptStatus;
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
                .Query<CommentDto, UserDto, HomeworkAttemptDto, HomeworkAttemptStatusDto, CommentDto>(
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

                        if (homeworkAttempt != null && homeworkAttemptStatus != null && user != null && !homeworkAttemptDictionary.TryGetValue(homeworkAttempt.Id, out HomeworkAttemptDto homeworkAttemptEntry))
                        {
                            homeworkAttemptEntry = homeworkAttempt;
                            homeworkAttemptEntry.Author = user;
                            homeworkAttemptEntry.HomeworkAttemptStatus = homeworkAttemptStatus;
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
        public int AddHomeworkAttemptStatus(HomeworkAttemptStatusDto homeworkAttemptStatus)
        {
            var result = _connection
                .QuerySingle<int>("dbo.HomeworkAttemptStatus_Add",
                new
                {
                    homeworkAttemptStatus.Name
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatuses()
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
        public int HardDeleteHomeworkAttemptStatus(int id)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttemptStatus_HardDelete",
                new
                {
                    id
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

        public void DeleteComment_AttachmentById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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
                .Query<HomeworkAttemptDto, UserDto, HomeworkAttemptStatusDto, HomeworkAttemptDto>(
                "dbo.HomeworkAttempt_SelectByHomeworkId",
                (attempt, user, status) =>
                {
                    if (!hwAttemptDictionary.TryGetValue(attempt.Id, out HomeworkAttemptDto attemptEntry))
                    {
                        attemptEntry = attempt;
                        attemptEntry.Author = user;
                        attemptEntry.HomeworkAttemptStatus = status;
                        hwAttemptDictionary.Add(attemptEntry.Id, attemptEntry);
                    }
                    return attemptEntry;
                },
                new {id },
                splitOn: "id", 
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homeworkAttempts;
        }
        
        public List<CommentDto> GetCommentsByHomeworkAttemptId(int id)
        {
            var comments = _connection
                .Query<CommentDto>("dbo.Comment_SelectByHomeworkAttemptId", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return comments;
        }

        public List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id)
        {
            var attachmentDictionary = new Dictionary<int, AttachmentDto>();
            var comments = _connection
                .Query<AttachmentDto, int, AttachmentDto>
                ("dbo.Attachment_SelectByHomeworkAttemptId",
                (attachment, type)=> 
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

        public List<Homework_ThemeDto> GetHomeworkThemesByThemeId(int id)
        {
            var result = _connection.
               Query<Homework_ThemeDto>("dbo.Homework_Theme_SelectAllByThemeId",
               new { id }, commandType: System.Data.CommandType.StoredProcedure)
               .Distinct()
               .ToList();
            return result;
        }
    }
}

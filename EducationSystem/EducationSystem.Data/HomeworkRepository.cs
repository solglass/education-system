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
            var courseDictionary = new Dictionary<int, CourseDto>();
            
            var groupDictionary = new Dictionary<int, GroupDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var HomeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homeworks = _connection
                .Query<HomeworkDto, CourseDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectById",
                    (homework, course, group, tag, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Course = course;
                            homeworkEntry.Groups = new List<GroupDto>();
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

                        if (group != null && !groupDictionary.TryGetValue(group.Id, out GroupDto groupEntry))
                        {
                            groupEntry = group;
                            groupEntry.GroupStatus = (GroupStatus)groupEntry.GroupStatus;
                            homeworkEntry.Groups.Add(groupEntry);
                            groupDictionary.Add(groupEntry.Id, groupEntry);
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
            var courseDictionary = new Dictionary<int, CourseDto>();

            var groupDictionary = new Dictionary<int, GroupDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var HomeworkAttemptDictionary = new Dictionary<int, HomeworkAttemptDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homeworks = _connection
                .Query<HomeworkDto, CourseDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_SelectAll",
                    (homework, course, group, tag, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Course = course;
                            homeworkEntry.Groups = new List<GroupDto>();
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

                        if (group != null && !groupDictionary.TryGetValue(group.Id, out GroupDto groupEntry))
                        {
                            groupEntry = group;
                            groupEntry.GroupStatus = (GroupStatus)groupEntry.GroupStatus;
                            homeworkEntry.Groups.Add(groupEntry);
                            groupDictionary.Add(groupEntry.Id, groupEntry);
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
                .ToList();
            return homeworks;
        }
        public List<HomeworkDto> SearchHomeworks(int? courseId = null, int? groupId = null, int? themeId = null, int? tagId = null)
        {
            if(courseId == null && groupId == null && themeId == null && tagId == null)
                throw new ArgumentNullException();

            var homeworkDictionary = new Dictionary<int, HomeworkDto>();
            var groupDictionary = new Dictionary<int, GroupDto>();
            var tagDictionary = new Dictionary<int, TagDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();

            var homeworks = _connection.Query<HomeworkDto, CourseDto, GroupDto, TagDto, ThemeDto, HomeworkDto>(
                    "dbo.Homework_Search",
                    (homework, course, group, tag, theme) =>
                    {
                        if (!homeworkDictionary.TryGetValue(homework.Id, out HomeworkDto homeworkEntry))
                        {
                            homeworkEntry = homework;
                            homeworkEntry.Course = course;
                            homeworkEntry.Groups = new List<GroupDto>();
                            homeworkEntry.Tags = new List<TagDto>();
                            homeworkEntry.Themes = new List<ThemeDto>();
                            homeworkEntry.HomeworkAttempts = new List<HomeworkAttemptDto>();
                            homeworkDictionary.Add(homeworkEntry.Id, homeworkEntry);
                            tagDictionary.Clear();
                            themeDictionary.Clear();
                            groupDictionary.Clear();
                        }

                        if (group != null)
                        {
                            if (!groupDictionary.TryGetValue(group.Id, out GroupDto groupEntry))
                            {
                                groupEntry = group;
                                groupEntry.GroupStatus = groupEntry.GroupStatus;
                                homeworkEntry.Groups.Add(groupEntry);
                                groupDictionary.Add(group.Id, group);
                            }
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
                        courseId,
                        groupId,
                        themeId,
                        tagId
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return homeworks;
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
                    courseId = homework.Course.Id,
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
                     statusId = (int)homeworkAttempt.HomeworkAttemptStatus
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

        public List<CommentDto> SearchComments(int? homeworkAttemptId, int? homeworkId)
        {
            if (homeworkAttemptId == null && homeworkId == null)
                throw new ArgumentNullException();

            var commentDictionary = new Dictionary<int, CommentDto>();
            var attachmentDictionary = new Dictionary<int, AttachmentDto>();

            var comments = _connection.Query<CommentDto, HomeworkAttemptDto, UserDto, AttachmentDto, int, CommentDto>(
                    "dbo.Comment_Search",
                    (comment, homeworkAttempt, user, attachment, attachmentType) =>
                    {
                        if (!commentDictionary.TryGetValue(comment.Id, out CommentDto commentEntry))
                        {
                            commentEntry = comment;
                            commentEntry.Author = user;
                            commentEntry.HomeworkAttempt = homeworkAttempt;
                            commentEntry.Attachments = new List<AttachmentDto>();
                            commentDictionary.Add(commentEntry.Id, commentEntry);
                            attachmentDictionary.Clear();
                        }

                        if (attachment != null)
                        {
                            if (!attachmentDictionary.TryGetValue(attachment.Id, out AttachmentDto attachmentEntry))
                            {
                                attachmentEntry = attachment;
                                attachmentEntry.AttachmentType = (AttachmentType)attachmentType;
                                commentEntry.Attachments.Add(attachmentEntry);
                                attachmentDictionary.Add(attachmentEntry.Id, attachmentEntry);
                            }
                        }
                        return commentEntry;
                    },
                    new
                    {
                        homeworkAttemptId,
                        homeworkId
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
            var attachmentDictionary = new Dictionary<int, AttachmentDto>();
            var commentById = _connection
                .Query<CommentDto, HomeworkAttemptDto, UserDto, AttachmentDto, int?, CommentDto>(
                    "dbo.Comment_SelectById",
                    (comment, homeworkAttempt, user, attachment, attachmentType) =>
                    {
                        if (!commentDictionary.TryGetValue(comment.Id, out CommentDto commentEntry))
                        {
                            commentEntry = comment;
                            commentEntry.Author = user;
                            commentEntry.HomeworkAttempt = homeworkAttempt;
                            commentEntry.Attachments = new List<AttachmentDto>();
                            commentDictionary.Add(commentEntry.Id, commentEntry);
                            attachmentDictionary.Clear();
                        }

                        if (attachment != null)
                        {
                            if (!attachmentDictionary.TryGetValue(attachment.Id, out AttachmentDto attachmentEntry))
                            {
                                attachmentEntry = attachment;
                                attachmentEntry.AttachmentType = (AttachmentType)attachmentType;
                                commentEntry.Attachments.Add(attachmentEntry);
                                attachmentDictionary.Add(attachmentEntry.Id, attachmentEntry);
                            }
                        }
                        return commentEntry;
                    },
                    new
                    { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return commentById;
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
        public int AddHomework_Group(int homeworkId, int groupId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Group_Add",
                new
                {
                    homeworkId = homeworkId,
                    groupId = groupId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteHomework_Group(int homeworkId, int groupId)
        {
            var result = _connection
                .Execute("dbo.Homework_Group_Delete",
                new
                {
                    homeworkId = homeworkId,
                    groupId = groupId
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

        public int HomeworkTagAdd(int homeworkId, int tagId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Homework_Tag_Add",
                new 
                { 
                    tagId,
                    homeworkId 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int HomeworkTagDelete(int homeworkId, int tagId)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Delete", 
                 new 
                {
                    homeworkId, 
                    tagId
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id)
        {
            var homeworkAttempt = _connection
           .Query<HomeworkAttemptWithCountDto, int, HomeworkDto, UserDto, HomeworkAttemptWithCountDto>("dbo.HomeworkAttempt_SelectByUserId",
           (homeworkAttempt, homeworkAttemptStatus, homework, author) =>
           {
               homeworkAttempt.HomeworkAttemptStatus = (HomeworkAttemptStatus)homeworkAttemptStatus;
               homeworkAttempt.Homework = homework;
               homeworkAttempt.Author = author;
               homework.Course = new CourseDto();
               return homeworkAttempt;
           },
               new { id }, commandType: System.Data.CommandType.StoredProcedure)
               .ToList();
            return homeworkAttempt;
        }

        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId)
        {
            var homeworkAttempt = _connection
            .Query<HomeworkAttemptWithCountDto, int, HomeworkDto, UserDto, HomeworkAttemptWithCountDto>("dbo.HomeworkAttempt_SelectByGroupIdAndStatusId",
            (homeworkAttempt, homeworkAttemptStatus, homework, author) =>
            {
                homeworkAttempt.HomeworkAttemptStatus = (HomeworkAttemptStatus)homeworkAttemptStatus;
                homeworkAttempt.Homework = homework;
                homeworkAttempt.Author = author;
                homework.Course = new CourseDto();
                return homeworkAttempt;
            },
            new { groupId, statusId },  commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homeworkAttempt;
        }
    }
}


using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EducationSystem.Core.Enums;
using EducationSystem.Core.Config;
using Microsoft.Extensions.Options;

namespace EducationSystem.Data
{
    public class AttachmentRepository : BaseRepository, IAttachmentRepository
    {
        public AttachmentRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public AttachmentDto GetAttachmentById(int id)
        {
            var data = _connection
                .Query<AttachmentDto, int, AttachmentDto>(
                    "dbo.Attachment_SelectById",
                    (attachment, attachmentType) =>
                    {
                        attachment.AttachmentType = (AttachmentType)attachmentType;
                        return attachment;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return data;
        }

        public int ModifyAttachment(AttachmentDto attachmentDto, int id)
        {          
            string path = attachmentDto.Path;
            int attachmentTypeID = (int)attachmentDto.AttachmentType;
            var data = _connection
                .Execute("dbo.Attachment_Update", new { id, path, attachmentTypeID }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }


        public int DeleteAttachmentById(int id)
        {
            var data = _connection
                .Execute("dbo.Attachment_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }


        public int AddAttachment(AttachmentDto attachmentDto)
        {
            var value = _connection
                .QuerySingleOrDefault<int>("dbo.Attachment_Add",
                new
                {
                    path = attachmentDto.Path,
                    attachmentTypeId = (int)attachmentDto.AttachmentType
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return value;

        }



        public int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId)
        {
            int attachmentId = AddAttachment(attachmentDto);

            Comment_AttachmentDto comment_AttachmentDto = new Comment_AttachmentDto
            {
                AttachmentId = attachmentId,
                CommentId = commentId
            };
            AddComment_Attachment(comment_AttachmentDto);
            return attachmentId;

        }

        public int AddAttachmentToHomeworkAttempt(AttachmentDto attachmentDto, int homeworkAttemptId)
        {
            int attachmentId = AddAttachment(attachmentDto);
            HomeworkAttempt_AttachmentDto homework_AttachmentDto = new HomeworkAttempt_AttachmentDto
            {
                AttachmentId = attachmentId,
                HomeworkAttemptId = homeworkAttemptId
            };
            AddHomeworkAttempt_Attachment(homework_AttachmentDto);
            return attachmentId;

        }

        public int AddHomeworkAttempt_Attachment(HomeworkAttempt_AttachmentDto newObject)
        {
            var data = _connection
                .QueryFirst<int>("dbo.HomeworkAttempt_Attachment_Add",
                new
                {
                    HomeworkAttemptId = newObject.HomeworkAttemptId,
                    AttachmentId = newObject.AttachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public int DeleteHomeworkAttemptAttachment(int attachmentId, int homeworkAttemptId)
        {
            int id = attachmentId;
            _connection.Execute(
         "dbo.HomeworkAttempt_Attachment_Delete",
             new
             {
                 homeworkAttemptID = homeworkAttemptId,
                 attachmentID = attachmentId
             },
                 commandType: System.Data.CommandType.StoredProcedure);
            var data = _connection
            .Execute("dbo.Attachment_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
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

        public int DeleteCommentAttachment(int attachmentId, int commentId)
        {
            int id = attachmentId;
            _connection
            .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Delete", new { commentId, attachmentId }, commandType: System.Data.CommandType.StoredProcedure);
            var data = _connection
            .Execute("dbo.Attachment_Delete", new { id}, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

    }
}


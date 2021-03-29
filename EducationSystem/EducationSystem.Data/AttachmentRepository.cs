
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
                    new 
                    {
                        id 
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return data;
        }
        public int UpdateAttachment(AttachmentDto attachmentDto)
        {
            var result = _connection
                .Execute("dbo.Attachment_Update",
                 new
                 {
                     id = attachmentDto.Id,
                     description = attachmentDto.Description,
                     path = attachmentDto.Path,
                     attachmentTypeId = (int)attachmentDto.AttachmentType
                 },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteAttachmentById(int id)
        {
            var data = _connection
                .Execute("dbo.Attachment_Delete",
                new
                { 
                    id 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public int AddAttachment(AttachmentDto attachmentDto)
        {
            var value = _connection
                .QuerySingleOrDefault<int>("dbo.Attachment_Add",
                new
                {
                    Description = attachmentDto.Description,
                    Path = attachmentDto.Path,
                    AttachmentTypeId = (int)attachmentDto.AttachmentType
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return value;
        }

        public List<AttachmentDto> GetAttachmentsByHomeworkAttemptId(int id)
        {
            var result = _connection
                .Query<AttachmentDto, int, AttachmentDto>
                ("dbo.Attachment_SelectByHomeworkAttemptId",
                (attachment, type) =>
                {
                    attachment.AttachmentType = (AttachmentType)type;                        
                    return attachment;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<AttachmentDto> GetAttachmentsByCommentId(int id)
        {
            var result = _connection
                .Query<AttachmentDto, int, AttachmentDto>
                ("dbo.Attachment_SelectByCommentId",
                (attachment, type) =>
                {
                    attachment.AttachmentType = (AttachmentType)type;
                    return attachment;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }

        public int AddAttachmentToHomeworkAttempt(int homeworkAttemptId, int attachmentId)
        {
            var data = _connection
                .QueryFirst<int>("dbo.HomeworkAttempt_Attachment_Add",
                new
                {
                    homeworkAttemptId ,
                    attachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public int DeleteAttachmentFromHomeworkAttempt(int attachmentId, int homeworkAttemptId)
        {
          var result=  _connection.Execute("dbo.HomeworkAttempt_Attachment_Delete",
             new
             {
                  homeworkAttemptId,
                  attachmentId
             },
                 commandType: System.Data.CommandType.StoredProcedure);
           
            return result;
        }

        public int AddAttachmentToComment(int commentId, int attachmentId)
        {
            var data = _connection
                .QuerySingleOrDefault<int>("dbo.Comment_Attachment_Add",
                new
                {
                    commentId ,
                    attachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public int DeleteAttachmentFromComment(int attachmentId, int commentId)
        {
           var result= _connection
            .Execute("dbo.Comment_Attachment_Delete",
            new
            {
                commentId,
                attachmentId 
            },
            commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

    }
}


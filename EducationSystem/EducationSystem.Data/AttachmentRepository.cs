
﻿using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace EducationSystem.Data
{
    public class AttachmentRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public AttachmentRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public AttachmentDto GetAttachmentById(int id)
        {
            var data = _connection
                .Query<AttachmentDto, AttachmentTypeDto, AttachmentDto>(
                    "dbo.Attachment_SelectById",
                    (attachment, attachmentType) =>
                    {                                             
                        attachment.AttachmentType = attachmentType;                       
                        return attachment;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return data;
        }

        public int ModifyAttachment(AttachmentDto attachmentDto)
        {
            int id = attachmentDto.Id;
            string path = attachmentDto.Path;
            int attachmentTypeID = attachmentDto.AttachmentType.Id;
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
                    attachmentTypeId = attachmentDto.AttachmentType.Id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return value;

        }



        public int AddAttachmentToComment(AttachmentDto attachmentDto, int commentId)
        {
            int attachmentId = AddAttachment(attachmentDto);
            HomeworkRepository homeworkrepo = new HomeworkRepository();
            Comment_AttachmentDto comment_AttachmentDto = new Comment_AttachmentDto
            {
                AttachmentId = attachmentId,
                CommentId = commentId
            };
            homeworkrepo.AddComment_Attachment(comment_AttachmentDto);
            return attachmentId;

        }

        public int AddAttachmentToHomeworkAttempt (AttachmentDto attachmentDto, int homeworkAttemptId)
        {
            int attachmentId = AddAttachment(attachmentDto);
            HomeworkAttemptRepository homeworkAttRepo = new HomeworkAttemptRepository();
            HomeworkAttempt_AttachmentDto homework_AttachmentDto = new HomeworkAttempt_AttachmentDto
            {
                AttachmentId = attachmentId,
                HomeworkAttemptId = homeworkAttemptId
            };
            homeworkAttRepo.AddHomeworkAttempt_Attachment(homework_AttachmentDto);
            return attachmentId;

        }



    }
}


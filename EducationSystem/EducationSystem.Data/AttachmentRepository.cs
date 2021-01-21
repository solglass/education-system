using EducationSystem.Data.Models;
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

        public List<AttachmentDto> GetAttachments()
        {
 
            var entry = new AttachmentDto();
            var data = _connection
                .Query<AttachmentDto, AttachmentTypeDto, AttachmentDto>(
                    "dbo.Attachment_SelectAll",
                    (attachment, attachmentType) =>
                    {

                        entry = attachment;
                        entry.AttachmentType = attachmentType;

                        return entry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                    .Distinct()
                    .ToList();
            return data;

        }

        public AttachmentDto GetAttachmentById(int id)
        {

            var entry = new AttachmentDto();
            var data = _connection
                .Query<AttachmentDto, AttachmentTypeDto, AttachmentDto>(
                    "dbo.Attachment_SelectById",
                    (attachment, attachmentType) =>
                    {
                        if (entry.Id == 0)
                        {
                            entry = attachment;
                            entry.AttachmentType = attachmentType;
                        }
                        return entry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return data;
        }

        public AttachmentDto ModifyAttachment(int id, string path, int attachmentTypeID)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentDto>("dbo.Attachment_Update", new { id, path, attachmentTypeID }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }


        public void DeleteAttachmentById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentDto>("dbo.Attachment_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);

        }


        public AttachmentDto AddAttachment(AttachmentDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentDto>("dbo.Attachment_Add",
                new
                {
                    path = NewObject.Path,
                    attachmentTypeId = NewObject.AttachmentType.Id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }
        public List<AttachmentTypeDto> GetAttachmentTypes()
        {
            var data = _connection
                .Query<AttachmentTypeDto>("dbo.AttachmentType_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return data;


        }

        public AttachmentTypeDto GetAttachmentTypeById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public AttachmentTypeDto ModifyAttachmentType(int id, string name)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Update", new { id, name }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public void DeleteAttachmentTypeById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }


        public AttachmentTypeDto AddAttachmentType(AttachmentTypeDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Add",
                new { name = NewObject.Name }, commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }

    }
}

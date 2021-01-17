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
            var data = _connection
                .Query<AttachmentDto>("dbo.Attachment_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return data;


        }

        public AttachmentDto GetAttachmentById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentDto>("dbo.Attachment_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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
                new { name = NewObject.Name, 
                    attachmentTypeId = NewObject.AttachmentTypeID },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }

    }
}

using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace EducationSystem.Data
{
    public class Comment_AttachmentRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public Comment_AttachmentRepository()
        {
            _connection = new SqlConnection(_connectionString);
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

        public Comment_AttachmentDto AddComment_Attachment(Comment_AttachmentDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<Comment_AttachmentDto>("dbo.Comment_Attachment_Add",
                new
                {
                    commentId = NewObject.CommentId,
                    attachmentId = NewObject.AttachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }

    }
}

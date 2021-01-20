using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class CommentRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public CommentRepository()
        {
            _connection = new SqlConnection(_connectionString);
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
    }
}

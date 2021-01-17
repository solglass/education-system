using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class TagRepository
    {

        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public TagRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<TagDto> GetTag()
        {
            var tags = _connection
                .Query<TagDto>("dbo.Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<TagDto> GetTagById(int id)
        {
            var tags = _connection
                .Query<TagDto>("dbo.Tag_SelectById",new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public TagDto UpdateTag(int id,string name)
        {
            var tags = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_Update", new { id,name }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
    }
}

using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class Homework_TagRepository
    {

        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public Homework_TagRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<Homework_TagDto> GetHomework_Tag()
        {
            var tags = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<Homework_TagDto> GetHomework_TagById(int id)
        {
            var tags = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public Homework_TagDto Homework_TagUpdate(int id, int tagId, int materialId)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Update", new { id, tagId, materialId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public void Homework_TagDelete(int id)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public Homework_TagDto Homework_TagAdd(Homework_TagDto Tag)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Add", new { tagId = Tag.TagId, HomeworkId = Tag.Homework_TagId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
    }
}

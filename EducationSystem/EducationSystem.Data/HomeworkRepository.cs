using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class HomeworkRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public HomeworkRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<HomeworkDto> GetHomeworks()
        {
            var homework = _connection
                 .Query<HomeworkDto>("dbo.Homework_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homework;
        }
    }
}

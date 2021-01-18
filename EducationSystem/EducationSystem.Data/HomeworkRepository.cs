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

        public HomeworkDto GetHomeworkById(int id)
        {
            var homework = _connection
                .Query<HomeworkDto>("dbo.Homework_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homework;
        }

        public int AddHomework(string description, DateTime StartDate, DateTime DeadlineDate, int GroupID, bool IsOptional)
        {
            var result = _connection
                .Execute("dbo.Homework_Add",
                new
                {
                    description,
                    StartDate,
                    DeadlineDate,
                    GroupID,
                    IsOptional
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateHomework(int id, string description, DateTime StartDate, DateTime DeadlineDate, int GroupID, bool IsOptional, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Homework_Update",
                new
                {
                    id,
                    description,
                    StartDate,
                    DeadlineDate,
                    GroupID,
                    IsOptional,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteHomework(int id)
        {
            var result = _connection
                .Execute("dbo.Homework_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

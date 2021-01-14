using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EducationSystem.Data
{
    public class HomeworkRepository
    {
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public HomeworkRepository()
        {

        }

        public List<HomeworkDto> GetHomeworks()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand
            {
                CommandText = "dbo.Homework_SelectAll",
                Connection = sqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure
            };

            var reader = sqlCommand.ExecuteReader();

            List<HomeworkDto> result = new List<HomeworkDto>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new HomeworkDto
                    {
                        Id = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        StartDate = reader.GetDateTime(2),
                        DeadlineDate = reader.GetDateTime(3),
                        GroupID = reader.GetInt32(4),
                        IsOptional = reader.GetBoolean(5),
                        IsDeleted = reader.GetBoolean(6)
                    });
                }
            }

            reader.Close();

            sqlConnection.Close();

            return result;
        }
    }
}

using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EducationSystem.Data
{
    public class CourseRepository
    {
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public CourseRepository()
        {

        }

        public List<CourseDto> GetCourses()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand 
            {
                CommandText = "dbo.Course_SelectAll", 
                Connection = sqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure
            };

            var reader = sqlCommand.ExecuteReader();

            List<CourseDto> result = new List<CourseDto>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new CourseDto 
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Duration = reader.GetInt32(3),
                        IsDeleted = reader.GetBoolean(4)
                    });
                }
            }
            reader.Close();

            sqlConnection.Close();
            
            return result;
        }
    }
}

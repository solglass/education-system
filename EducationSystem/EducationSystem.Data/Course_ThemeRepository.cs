using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EducationSystem.Data
{
   public class Course_ThemeRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public Course_ThemeRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public int AddCourse_Theme(int courseId, int themeId)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Add",
                new
                {
                    courseId,
                    themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteCourse_Theme(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

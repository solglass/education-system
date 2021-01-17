using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class CourseRepository
    {

        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public CourseRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<CourseDto> GetCourses()
        {
            var courses = _connection
                .Query<CourseDto>("dbo.Course_SelectAll", commandType:System.Data.CommandType.StoredProcedure)
                .ToList();
            return courses;
        }

        public CourseDto GetCourseById(int id)
        {
            var course = _connection
                .Query<CourseDto>("dbo.Course_SelectById", new { id }, commandType:System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return course;
        }
    }
}

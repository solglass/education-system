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
            var courseDictionary = new Dictionary<int, CourseDto>();

            var courses = _connection
                .Query<CourseDto, ThemeDto, CourseDto>(
                    "dbo.Course_SelectAll",
                    (course, theme) =>
                    {
                        if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                        }

                        courseEntry.Themes.Add(theme);
                        return courseEntry;
                    },
                    splitOn: "Id",
                    commandType:System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return courses;
        }

        public CourseDto GetCourseById(int id)
        {
            var courseEntry = new CourseDto();
            var course = _connection
                .Query<CourseDto, ThemeDto, CourseDto>(
                    "dbo.Course_SelectById", 
                    (course, theme) =>
                    {
                        if (courseEntry.Id == 0) 
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                        }
                        courseEntry.Themes.Add(theme);
                        return courseEntry;
                    },
                    new { id }, 
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return course;
        }

        // should return id of inserted entity, use 'QuerySingle' method
        public int AddCourse(string name, string description, int duration)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Add", 
                new
                {
                  name,
                  description ,
                  duration 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int UpdateCourse(int id, string name, string description, int duration, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {   id,
                    name,
                    description ,
                    duration,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int DeleteCourse(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Delete",
                new
                {
                    id 
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public dynamic GetCourseThemesByCourseId(int courseId)
        {

            string sqlString = @"SELECT t.Id, t.Name FROM [dbo].[Course] c JOIN [dbo].[Course_Theme] ct on c.Id=ct.CourseId JOIN [dbo].[Theme] t on t.Id=ct.ThemeId WHERE c.Id=@courseId";

            var result= _connection
                .Query<ThemeDto>(sqlString,new { courseId }, commandType: System.Data.CommandType.Text)
                .ToList(); 

            return result;
        }
    }
}

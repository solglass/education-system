using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class LessonRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public LessonRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }


        public void AddUnderstandingLevel(string name)
        {
            _connection.Execute(
                "dbo.UnderstandingLevel_Add", 
                new {name}, 
                commandType: CommandType.StoredProcedure);
        }
        public void DeleteUnderstandingLevel(int id)
        {
            _connection.Execute(
                "dbo.UnderstandingLevel_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public void UpdateUnderstandingLevel(int id, string name)
        {
            _connection.Execute(
                "dbo.UnderstandingLevel_Update",
                new { id, name },
                commandType: CommandType.StoredProcedure);
        }
        public List<UnderstandingLevelDto> GetUnderstandingLevels()
        {
            var undersandingLevel = _connection
                .Query<UnderstandingLevelDto>("dbo.UnderstandingLevel_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return undersandingLevel;
        }
        public UnderstandingLevelDto GetUnderstandingLevelById(int id)
        {
            var understandingLevel = _connection
                .Query<UnderstandingLevelDto>("dbo.UnderstandingLevel_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return understandingLevel;
        }
        public void AddAttendance(int lessonId,int userId,byte isAbsent)
        {
            _connection.Execute(
                "dbo.Attendance_Add",
                new { lessonId,userId,isAbsent },
                commandType: CommandType.StoredProcedure);
        }
        public void DeleteAttendance(int id)
        {
            _connection.Execute(
                "dbo.Attendance_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public void UpdateAttendance(int id, int lessonId, int userId, byte isAbsent)
        {
            _connection.Execute(
                "dbo.Attendance_Update",
                new { id, lessonId, userId, isAbsent },
                commandType: CommandType.StoredProcedure);
        }
        public List<AttendanceDto> GetAttendances()
        {
            var attendance = _connection
                .Query<AttendanceDto>("dbo.Attendance_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return attendance;
        }
        public AttendanceDto GetAttendanceById(int id)
        {
            var attendance = _connection
                .Query<AttendanceDto>("dbo.Attendance_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return attendance;
        }

        public void AddLessonTheme(int themeId,int lessonId)
        {
            _connection.Execute(
                "dbo.LessonTheme_Add",
                new { themeId, lessonId },
                commandType: CommandType.StoredProcedure);
        }
        public void DeleteLessonTheme(int id)
        {
            _connection.Execute(
                "dbo.LessonTheme_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public void UpdateLessonTheme(int id, int themeId, int lessonId)
        {
            _connection.Execute(
                "dbo.LessonTheme_Update",
                new { id, themeId, lessonId },
                commandType: CommandType.StoredProcedure);
        }
        public List<LessonThemeDto> GetLessonThemes()
        {
            var lessonTheme = _connection
                .Query<LessonThemeDto>("dbo.LessonTheme_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return lessonTheme;
        }
        public LessonThemeDto GetLessonThemeById(int id)
        {
            var lessonTheme = _connection
                .Query<LessonThemeDto>("dbo.LessonTheme_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return lessonTheme;
        }

    }
}

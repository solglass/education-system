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

        public List<LessonDto> GetLessons()
        {
            return _connection
                .Query<LessonDto>("dbo.Lesson_SelectAll", commandType: CommandType.StoredProcedure)
                .ToList();
        }

        public UnderstandingLevelDto GetLessonById(int id)
        {
            return _connection
                .Query<UnderstandingLevelDto>("dbo.Lesson_SelectByID", new { id }, commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
        }

        public int AddLesson(int groupId, string description, DateTime date)
        {
            return _connection.Execute(
                "dbo.Lesson_Add",
                new {groupId, description, date},
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteLesson(int id)
        {
           return _connection.Execute(
                "dbo.Lesson_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateLesson(int id, int groupId, string description, DateTime date, bool isDeleted)
        {
            return _connection.Execute(
                "dbo.Lesson_Update",
                new {id, groupId, description, date, isDeleted},
                commandType: CommandType.StoredProcedure);
        }

    

        public int AddFeedback(int userdId, string message, int lessonId, int understandingLevelId)
        {
            return _connection.Execute(
                "dbo.Feedback_Add",
                new {userdId, message, lessonId, understandingLevelId},
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateFeedback(int id, int userdId, string message, int lessonId, int understandingLevelId)
        {
            return _connection.Execute(
                "dbo.Feedback_Update",
                new {id, userdId, message, lessonId, understandingLevelId},
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteFeedback(int id)
        {
            return _connection.Execute(
                "dbo.Feedback_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
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

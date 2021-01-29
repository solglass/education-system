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
    public class LessonRepository : BaseRepository
    {
        public LessonRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<LessonDto> GetLessons()
        {
            var lessons = new Dictionary<int, LessonDto>();
            return _connection.Query<LessonDto, ThemeDto, LessonDto>(
                "dbo.Lesson_SelectAll",
                (lesson, theme) =>
                {
                    LessonDto lessonEntry;

                    if (!lessons.TryGetValue(lesson.ID, out lessonEntry))
                    {
                        lessonEntry = lesson;
                        lessonEntry.Themes = new List<ThemeDto>();
                        lessons.Add(lessonEntry.ID, lessonEntry);
                    }

                    lessonEntry.Themes.Add(theme);
                    return lessonEntry;
                },
                splitOn: "OrderDetailID")
            .Distinct()
            .ToList();
        }

        public LessonDto GetLessonById(int id)
        {

            return _connection.Query<LessonDto, ThemeDto, LessonDto>(
                "dbo.Lesson_SelectByID",
                (lesson, theme) =>
                {
                    lesson.Themes = new List<ThemeDto>();
                    lesson.Themes.Add(theme);
                    return lesson;
                },
                new { id },
                splitOn: "OrderDetailID").FirstOrDefault();

        }

        public int AddLesson(LessonDto lesson)
        {
            return _connection.Execute(
                "dbo.Lesson_Add",
                new { lesson.GroupID, lesson.Comment, lesson.Data },
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteLesson(int id)
        {
           return _connection.Execute(
                "dbo.Lesson_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateLesson(LessonDto lesson)
        {
            return _connection.Execute(
                "dbo.Lesson_Update",
                new { lesson.ID, lesson.GroupID, lesson.Comment, lesson.Data, lesson.IsDeleted},
                commandType: CommandType.StoredProcedure);
        }

    

        public int AddFeedback(FeedbackDto feedback)
        {
            return _connection.Execute(
                "dbo.Feedback_Add",
                new { feedback.UserID, feedback.Messege, feedback.LessonID, feedback.UnderstandingLevelID },
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateFeedback(FeedbackDto feedback)
        {
            return _connection.Execute(
                "dbo.Feedback_Update",
                new {feedback.ID, feedback.UserID, feedback.Messege, feedback.LessonID, feedback.UnderstandingLevelID},
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteFeedback(int id)
        {
            return _connection.Execute(
                "dbo.Feedback_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
        }

        public int AddUnderstandingLevel(string name)
        {
            return _connection.Execute(
                "dbo.UnderstandingLevel_Add", 
                new {name}, 
                commandType: CommandType.StoredProcedure);
        }
        public int DeleteUnderstandingLevel(int id)
        {
            return _connection.Execute(
                "dbo.UnderstandingLevel_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateUnderstandingLevel(UnderstandingLevelDto understanding)
        {
            return _connection.Execute(
                "dbo.UnderstandingLevel_Update",
                new { understanding.ID, understanding.Name },
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
        public int AddAttendance(AttendanceDto attendance)
        {
            return _connection.Execute(
                "dbo.Attendance_Add",
                new { attendance.LessonID, attendance.UserID, attendance.IsAbsent },
                commandType: CommandType.StoredProcedure);
        }
        public int DeleteAttendance(int id)
        {
            return _connection.Execute(
                "dbo.Attendance_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateAttendance(AttendanceDto attendance)
        {
            return _connection.Execute(
                "dbo.Attendance_Update",
                new { attendance.ID, attendance.LessonID, attendance.UserID, attendance.IsAbsent },
                commandType: CommandType.StoredProcedure);
        }
        public List<AttendanceDto> GetAttendances()
        {
            return _connection.Query<AttendanceDto, UserDto, AttendanceDto>(
            "dbo.Attendance_SelectAll",
            (attendance, user) =>
            {
                attendance.User = user;
                return attendance;
            },
            splitOn: "Id")
            .Distinct()
            .ToList();
        }
        public AttendanceDto GetAttendanceById(int id)
        {
            return _connection.Query<AttendanceDto, UserDto, AttendanceDto>(
            "dbo.Attendance_SelectById",
            (attendance, user) =>
            {
                attendance.User = user;
                return attendance;
            },
            new { id },
            splitOn: "Id")
                .FirstOrDefault();

        }

        public void AddLessonTheme(LessonThemeDto lessonTheme)
        {
            _connection.Execute(
                "dbo.LessonTheme_Add",
                new { lessonTheme.ThemeID, lessonTheme.LessonID },
                commandType: CommandType.StoredProcedure);
        }
        public void DeleteLessonTheme(int id)
        {
            _connection.Execute(
                "dbo.LessonTheme_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public void UpdateLessonTheme(LessonThemeDto lessonTheme)
        {
            _connection.Execute(
                "dbo.LessonTheme_Update",
                new { lessonTheme.ID, lessonTheme.ThemeID, lessonTheme.LessonID },
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

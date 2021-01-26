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
        public List<LessonDto> GetLessons()
        {
            var lessonDictionary = new Dictionary<int, LessonDto>();
            var lessons = _connection
                .Query<LessonDto, ThemeDto, LessonDto>("dbo.Lesson_SelectAll",
               (lesson, theme) =>
               {

                   if (!lessonDictionary.TryGetValue(lesson.ID, out LessonDto lessonEntry))
                   {
                       lessonEntry = lesson;
                       lessonEntry.Themes = new List<ThemeDto>();
                       lessonDictionary.Add(lessonEntry.ID, lessonEntry);
                   }

                   lessonEntry.Themes.Add(theme);
                   return lessonEntry;
               },
                splitOn: "ID",
                commandType: CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return lessons;
        }

        public LessonDto GetLessonById(int id)
        {
            var lessonEntry = new LessonDto();
            var lesson = _connection
                .Query<LessonDto, ThemeDto, LessonDto>("dbo.Lesson_SelectByID",
               (lesson, theme) =>
               {

                   if (lessonEntry.ID == 0)
                   {
                       lessonEntry = lesson;
                       lessonEntry.Themes = new List<ThemeDto>();
                   }

                   lessonEntry.Themes.Add(theme);
                   return lessonEntry;
               },
                splitOn: "ID",
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();
            return lesson;
        }

        public int AddLesson(LessonDto lessonDto)
        {
            return _connection.Execute(
                "dbo.Lesson_Add",
                new { lessonDto.GroupID, lessonDto.Comment, lessonDto.Data},
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteLesson(int id)
        {
           return _connection.Execute(
                "dbo.Lesson_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateLesson(LessonDto lessonDto)
        {
            return _connection.Execute(
                "dbo.Lesson_Update",
                new {lessonDto.ID, lessonDto.GroupID, lessonDto.Comment, lessonDto.Data, lessonDto.IsDeleted },
                commandType: CommandType.StoredProcedure);
        }

        public List<FeedbackDto> GetFeedbacks()
        {
            return _connection
                .Query<FeedbackDto, LessonDto, UnderstandingLevelDto, UserDto, FeedbackDto>(
                    "dbo.Feedback_SelectAll",
                    (feedback, lesson, understendinglevel, user) =>
                    {
                        feedback.Lesson = lesson;
                        feedback.UnderstandingLevel = understendinglevel;
                        feedback.User = user;
                        return feedback;
                    },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure
                )
                .Distinct()
                .ToList();
        }

        public FeedbackDto GetFeedbackById(int id)
        {
            return _connection
                .Query<FeedbackDto, LessonDto, UnderstandingLevelDto, UserDto, FeedbackDto>(
                    "dbo.Feedback_SelectById",
                    (feedback, lesson, understendinglevel, user) =>
                    {
                        feedback.Lesson = lesson;
                        feedback.UnderstandingLevel = understendinglevel;
                        feedback.User = user;
                        return feedback;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure
                )
                .FirstOrDefault();
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

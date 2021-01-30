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
            return _connection
                .QuerySingleOrDefault<int>(
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


        public int AddFeedback(FeedbackDto feedbackDto)
        {
            return _connection
                .QuerySingleOrDefault<int>(
                "dbo.Feedback_Add",
                new {feedbackDto.UserID, feedbackDto.Messege, feedbackDto.LessonID, feedbackDto.UnderstandingLevelID},
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateFeedback(FeedbackDto feedbackDto)
        {
            return _connection.Execute(
                "dbo.Feedback_Update",
                new {feedbackDto.ID, feedbackDto.UserID, feedbackDto.Messege, feedbackDto.LessonID, feedbackDto.UnderstandingLevelID},
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteFeedback(int id)
        {
            return _connection.Execute(
                "dbo.Feedback_Delete",
                new {id},
                commandType: CommandType.StoredProcedure);
        }

        public int AddUnderstandingLevel(UnderstandingLevelDto understandingLevel)
        {
           return _connection
                .QuerySingleOrDefault<int>(
                "dbo.UnderstandingLevel_Add", 
                new {understandingLevel.Name}, 
                commandType: CommandType.StoredProcedure);
        }
        public int DeleteUnderstandingLevel(int id)
        {
            return _connection.Execute(
                "dbo.UnderstandingLevel_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateUnderstandingLevel(UnderstandingLevelDto understandingLevel)
        {
           return _connection.Execute(
                "dbo.UnderstandingLevel_Update",
                new {understandingLevel.ID, understandingLevel.Name },
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
           return _connection
                .QuerySingleOrDefault<int>(
                "dbo.Attendance_Add",
                new {attendance.LessonID,attendance.UserID,attendance.IsAbsent},
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
                new {attendance.ID,attendance.LessonID,attendance.UserID,attendance.IsAbsent},
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

        public int AddLessonTheme(LessonThemeDto lessonTheme)
        {
           return _connection
                .QuerySingleOrDefault<int>(
                "dbo.LessonTheme_Add",
                new {lessonTheme.ThemeID,lessonTheme.LessonID },
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
                new {lessonTheme.ID,lessonTheme.ThemeID,lessonTheme.LessonID },
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

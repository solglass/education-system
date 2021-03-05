using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class LessonRepository : BaseRepository, ILessonRepository
    {
        public LessonRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }
        public List<LessonDto> GetLessonsByGroupId(int id)
        {
            var lessonDictionary = new Dictionary<int, LessonDto>();
            var lessons = _connection
                .Query<LessonDto, ThemeDto, LessonDto>("dbo.Lesson_SelectByGroupId",
               (lesson, theme) =>
               {

                   if (!lessonDictionary.TryGetValue(lesson.Id, out LessonDto lessonEntry))
                   {
                       lessonEntry = lesson;
                       lessonEntry.Themes = new List<ThemeDto>();
                       lessonDictionary.Add(lessonEntry.Id, lessonEntry);
                   }

                   lessonEntry.Themes.Add(theme);
                   return lessonEntry;
               },
               new { id },
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

                   if (lessonEntry.Id == 0)
                   {
                       lessonEntry = lesson;
                       lessonEntry.Themes = new List<ThemeDto>();
                   }

                   lessonEntry.Themes.Add(theme);
                   return lessonEntry;
               },
               new { id },
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
                new { lessonDto.GroupId, lessonDto.Comment, lessonDto.Date, lessonDto.Themes },
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteOrRecoverLesson(int id, bool isDeleted)
        {
            return _connection.Execute(
                 "dbo.Lesson_DeleteOrRecover",
                 new { id, isDeleted },
                 commandType: CommandType.StoredProcedure);
        }

        public int HardDeleteLesson(int id)
        {
            return _connection.Execute(
                 "dbo.Lesson_HardDelete",
                 new { id },
                 commandType: CommandType.StoredProcedure);
        }

        public int UpdateLesson(LessonDto lessonDto)
        {
            return _connection.Execute(
                "dbo.Lesson_Update",
                new { lessonDto.Id, lessonDto.GroupId, lessonDto.Comment, lessonDto.Date, lessonDto.Themes },
                commandType: CommandType.StoredProcedure);
        }

        public List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId)
        {
            return _connection
                .Query<FeedbackDto, LessonDto, int, UserDto, FeedbackDto>(
                    "dbo.Feedback_Search",
                    (feedback, lesson, understendinglevel, user) =>
                    {
                        feedback.Lesson = lesson;
                        feedback.UnderstandingLevel = (UnderstandingLevel)understendinglevel;
                        feedback.User = user;
                        return feedback;
                    },
                    new { lessonId, groupId, courseId },
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure
                )
                .Distinct()
                .ToList();
        }

        public FeedbackDto GetFeedbackById(int id)
        {
            return _connection
                .Query<FeedbackDto, LessonDto, int, UserDto, FeedbackDto>(
                    "dbo.Feedback_SelectById",
                    (feedback, lesson, understendinglevel, user) =>
                    {
                        feedback.Lesson = lesson;
                        feedback.UnderstandingLevel = (UnderstandingLevel)understendinglevel;
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
                new
                {
                    UserId = feedbackDto.User.Id,
                    feedbackDto.Message,
                    LessonId = feedbackDto.Lesson.Id,
                    UnderstandingLevelId = (int)feedbackDto.UnderstandingLevel
                },
                commandType: CommandType.StoredProcedure);
        }

        public int UpdateFeedback(FeedbackDto feedbackDto)
        {
            return _connection.Execute(
                "dbo.Feedback_Update",
                new { feedbackDto.ID, feedbackDto.Message, feedbackDto.UnderstandingLevelID },
                commandType: CommandType.StoredProcedure);
        }

        public int DeleteFeedback(int id)
        {
            return _connection.Execute(
                "dbo.Feedback_Delete",
                new { id },
                commandType: CommandType.StoredProcedure);
        }


        public int AddAttendance(AttendanceDto attendance)
        {
            return _connection
                 .QuerySingleOrDefault<int>(
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
                new { attendance.Id, attendance.IsAbsent },
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
                 "dbo.Lesson_Theme_Add",
                 new { lessonTheme.ThemeId, lessonTheme.LessonId },
                 commandType: CommandType.StoredProcedure);
        }
        public void DeleteLessonTheme(int lessonId, int themeId)
        {
            _connection.Execute(
                "dbo.Lesson_Theme_Delete",
                new { lessonId, themeId },
                commandType: CommandType.StoredProcedure);
        }



        public LessonThemeDto GetLessonThemeById(int id)
        {
            var lessonTheme = _connection
                .Query<LessonThemeDto>("dbo.Lesson_Theme_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return lessonTheme;
        }

        public List<LessonThemeDto> GetLessonThemesByThemeId(int id)
        {
            var result = _connection.
               Query<LessonThemeDto>("dbo.Lesson_Theme_SelectAllByThemeId",
               new { id }, commandType: System.Data.CommandType.StoredProcedure)
               .Distinct()
               .ToList();
            return result;
        }

        public List<LessonDto> GetLessonsByThemeId(int themeId)
        {
            var result = _connection.
              Query<LessonDto>("dbo.Lesson_SelectByThemeId",
              new { themeId = themeId },
              commandType: System.Data.CommandType.StoredProcedure)
              .Distinct()
              .ToList();
            return result;
        }

        public List<AttendanceReportDto> GetStudentByPercentOfSkip(int percent, int groupId)
        {
            var result = _connection
                .Query<AttendanceReportDto>("dbo.Student_SelectByPercentOfSkip",
                new
                {
                    percent = percent,
                    groupId = groupId
                },
                commandType: System.Data.CommandType.StoredProcedure)
                .Distinct().ToList();
            return result;
        }

    }
}

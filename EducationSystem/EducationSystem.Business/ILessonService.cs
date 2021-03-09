using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ILessonService
    {
        int AddLesson(LessonDto lesson);
        int DeleteAttendance(int id);
        int DeleteFeedback(int id);
        AttendanceDto GetAttendanceById(int id);
        List<AttendanceDto> GetAttendancesByLessonId(int id);
        List<AttendanceDto> GetAttendancesByUserId(int id);
        FeedbackDto GetFeedbackById(int id);
        List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId);
        LessonDto GetLessonById(int id);
        List<LessonDto> GetLessonsByGroupId(int id);
        List<LessonDto> GetLessonsByThemeId(int themeId);
        LessonThemeDto GetLessonThemeById(int id);
        int UpdateAttendance(AttendanceDto attendance);
        List<AttendanceReportDto> GetStudentByPercentOfSkip(int percent, int groupId);
        int DeleteLesson(int id);
        int RecoverLesson(int id);
        int UpdateLesson(LessonDto lesson);
        int AddFeedback(FeedbackDto feedback);
        int UpdateFeedback(FeedbackDto feedback);
        int AddAttendance(AttendanceDto attendance);
        int AddLessonTheme(LessonThemeDto lessonTheme);
        int DeleteLessonTheme(int lessonId, int themeId);
    }
}

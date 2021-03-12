using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface ILessonRepository
    {
        int AddAttendance(AttendanceDto attendance);
        int AddFeedback(FeedbackDto feedbackDto);
        int AddLesson(LessonDto lessonDto);
        int AddLessonTheme(LessonThemeDto lessonTheme);
        int DeleteAttendance(int id);
        int DeleteFeedback(int id);
        int DeleteLessonTheme(int lessonId, int themeId);
        AttendanceDto GetAttendanceById(int id);
        List<AttendanceDto> GetAttendancesByLessonId(int id);
        List<AttendanceDto> GetAttendancesByUserId(int id);
        FeedbackDto GetFeedbackById(int id);
        List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId);
        LessonDto GetLessonById(int id);
        List<LessonDto> GetLessonsByGroupId(int id);
        int DeleteOrRecoverLesson(int id, bool isDeleted);
        List<LessonDto> GetLessonsByThemeId(int themeId);
        List<LessonThemeDto> GetLessonThemesByThemeId(int id);
        int UpdateAttendance(AttendanceDto attendance);
        int UpdateFeedback(FeedbackDto feedbackDto);
        int UpdateLesson(LessonDto lessonDto);
        List<AttendanceReportDto> GetStudentByPercentOfSkip(int percent, int groupId);
    }
}
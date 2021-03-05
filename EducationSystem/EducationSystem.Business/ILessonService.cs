using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ILessonService
    {
        int AddLesson(LessonDto lesson);
        void DeleteAttendance(int id);
        void DeleteFeedback(int id);
        AttendanceDto GetAttendanceById(int id);
        List<AttendanceDto> GetAttendances();
        FeedbackDto GetFeedbackById(int id);
        List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId);
        LessonDto GetLessonById(int id);
        List<LessonDto> GetLessonsByGroupId(int id);
        List<LessonDto> GetLessonsByThemeId(int themeId);
        LessonThemeDto GetLessonThemeById(int id);
        int UpdateAttendance(AttendanceDto attendance);
        object GetStudentByPercentOfSkip(int percent, int groupId);
        int DeleteLesson(int id);
        int RecoverLesson(int id);
        int UpdateLesson(LessonDto lesson);
        int AddFeedback(FeedbackDto feedback);
        int UpdateFeedback(FeedbackDto feedback);
        int AddAttendance(AttendanceDto attendance);
        int AddLessonTheme(LessonThemeDto lessonTheme);
    }
}
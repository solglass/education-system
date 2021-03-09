using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class LessonService : ILessonService
    {
        private ILessonRepository _lessonRepository;
        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        public List<LessonDto> GetLessonsByGroupId(int id)
        {
            return _lessonRepository.GetLessonsByGroupId(id);
        }
        public LessonDto GetLessonById(int id)
        {
            return _lessonRepository.GetLessonById(id);
        }

        public int DeleteLesson(int id)
        {
            bool isDeleted = true;
            return _lessonRepository.DeleteOrRecoverLesson(id, isDeleted);
        }
        public int RecoverLesson(int id)
        {
            bool isDeleted = false;
            return _lessonRepository.DeleteOrRecoverLesson(id, isDeleted);
        }

        public int AddLesson(LessonDto lesson)
        {
            return _lessonRepository.AddLesson(lesson);
        }

        public int UpdateLesson(LessonDto lesson)
        {
            return _lessonRepository.UpdateLesson(lesson);
        }

        public List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId)
        {
            return _lessonRepository.GetFeedbacks(lessonId, groupId, courseId);
        }
        public FeedbackDto GetFeedbackById(int id)
        {
            return _lessonRepository.GetFeedbackById(id);
        }
        public int DeleteFeedback(int id)
        {
            return _lessonRepository.DeleteFeedback(id);
        }

        public int AddFeedback(int lessonId, FeedbackDto feedback)
        {
            feedback.Lesson = new LessonDto { Id = lessonId };
            return _lessonRepository.AddFeedback(feedback);
        }

        public int UpdateFeedback(int lessonId, int feedbackId, FeedbackDto feedback)
        {
            feedback.Lesson = new LessonDto { Id = lessonId };
            feedback.Id = feedbackId;
            return _lessonRepository.UpdateFeedback(feedback);
        }

        public List<AttendanceDto> GetAttendancesByLessonId(int id)
        {
            return _lessonRepository.GetAttendancesByLessonId(id);
        }

        public List<AttendanceDto> GetAttendancesByUserId(int id)
        {
            return _lessonRepository.GetAttendancesByUserId(id);
        }

        public AttendanceDto GetAttendanceById(int id)
        {
            return _lessonRepository.GetAttendanceById(id);
        }
        public int DeleteAttendance(int id)
        {
            return _lessonRepository.DeleteAttendance(id);
        }

        public int AddAttendance(int lessonId, AttendanceDto attendance)
        {
            attendance.Lesson = new LessonDto { Id = lessonId };
            return _lessonRepository.AddAttendance(attendance);
        }

        public int UpdateAttendance(int lessonId, int attendanceId, AttendanceDto attendance)
        {
            attendance.Id = attendanceId;
            attendance.Lesson = new LessonDto { Id = lessonId };
            return _lessonRepository.UpdateAttendance(attendance);
        }

        public List<LessonDto> GetLessonsByThemeId(int themeId)
        {
            return _lessonRepository.GetLessonsByThemeId(themeId);
        }

        public int DeleteLessonTheme(int lessonId, int themeId)
        {
            return _lessonRepository.DeleteLessonTheme(lessonId, themeId);
        }

        public List<AttendanceReportDto> GetStudentByPercentOfSkip (int percent, int groupId)
        {
            return _lessonRepository.GetStudentByPercentOfSkip(percent, groupId);
        }
    }
}

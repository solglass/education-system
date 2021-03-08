﻿using EducationSystem.Data;
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

        public void AddLesson(LessonDto lesson)
        {
            _lessonRepository.AddLesson(lesson);
        }

        /* public UpdateLesson
        */
       public List<FeedbackDto> GetFeedbacks(int? lessonId, int? groupId, int? courseId)
        {
            return _lessonRepository.GetFeedbacks(lessonId, groupId, courseId);
        }
        public FeedbackDto GetFeedbackById(int id)
        {
            return _lessonRepository.GetFeedbackById(id);
        }
        public void DeleteFeedback(int id)
        {
            _lessonRepository.DeleteFeedback(id);
        }        
        public List<AttendanceDto> GetAttendances()
        {
            return _lessonRepository.GetAttendances();
        }
        public AttendanceDto GetAttendanceById(int id)
        {
            return _lessonRepository.GetAttendanceById(id);
        }
        public void DeleteAttendance(int id)
        {
            _lessonRepository.DeleteAttendance(id);
        }
        public LessonThemeDto GetLessonThemeById(int id)
        {
            return _lessonRepository.GetLessonThemeById(id);
        }
        public int UpdateAttendance(AttendanceDto attendance)
        {
            return _lessonRepository.UpdateAttendance(attendance);
        }
        public List<LessonDto> GetLessonsByThemeId(int themeId)
        {
            return _lessonRepository.GetLessonsByThemeId(themeId);
        }
        public List<AttendanceReportDto> GetStudentByPercentOfSkip (int percent, int groupId)
        {
            return _lessonRepository.GetStudentByPercentOfSkip(percent, groupId);
        }
    }
}

using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class LessonService
    {
        private LessonRepository _lessonRepository;
        public LessonService()
        {
            _lessonRepository = new LessonRepository();
        }
        public List<LessonDto> GetLessons()
        {
           return _lessonRepository.GetLessons();
        }
        public LessonDto GetLessonById(int id)
        {
           return _lessonRepository.GetLessonById(id);
        }
        public void DeleteLesson(int id)
        {
            _lessonRepository.DeleteLesson(id);
        }
        public void AddLesson(LessonDto lesson)
        {
            _lessonRepository.AddLesson(lesson);
        }
        /* public UpdateLesson
        */
       public List<FeedbackDto> GetFeedbacks()
        {
            return _lessonRepository.GetFeedbacks();
        }
        public FeedbackDto GetFeedbackById(int id)
        {
            return _lessonRepository.GetFeedbackById(id);
        }
        public void DeleteFeedback(int id)
        {
            _lessonRepository.DeleteFeedback(id);
        }
        /* public AddFeedback
        *  public UpdateFeedback
        */
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
        /* public AddAttendance
        *  public UpdateAttendance
        */
        public List<UnderstandingLevelDto> GetUnderstandingLevels()
        {
            return _lessonRepository.GetUnderstandingLevels();
        }
        public UnderstandingLevelDto GetUnderstandingLevelById(int id)
        {
            return _lessonRepository.GetUnderstandingLevelById(id);
        }
        public void DeleteUnderstandingLevel(int id)
        {
            _lessonRepository.DeleteUnderstandingLevel(id);
        }
        /* public AddUnderstandingLevel
        *  public UpdateUnderstandingLevel
        */
        
        public LessonThemeDto GetLessonThemeById(int id)
        {
            return _lessonRepository.GetLessonThemeById(id);
        }
        public void DeleteLessonTheme(int id)
        {
            _lessonRepository.DeleteLessonTheme(id);
        }

        public int UpdateAttendance(AttendanceDto attendance)
        {
            return _lessonRepository.UpdateAttendance(attendance);
        }
        /* public AddLessonTheme
*  public UpdateLessonTheme
*/
    }
}

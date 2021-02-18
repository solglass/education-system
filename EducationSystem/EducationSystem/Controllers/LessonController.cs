using System;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/lesson/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LessonController : ControllerBase
    {
        private LessonRepository _repo;
        private LessonMapper _lessonMapper;
        private LessonService _lessonService;

        public LessonController()
        {
            _repo = new LessonRepository();
            _lessonMapper = new LessonMapper();
            _lessonService = new LessonService();
        }

        // https://localhost:50221/api/lesson/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLesson([FromBody] LessonInputModel inputModel)
        {
            LessonDto lesson;
            try
            {
                lesson = _lessonMapper.ToDto(inputModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            _repo.AddLesson(lesson);
            return Ok("Урок добавлен");
        }

        // https://localhost:50221/api/lesson/
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public ActionResult GetLessons()
        {
            var result = _lessonService.GetLessons();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult GetLessonById(int id)
        {
            var result = _lessonService.GetLessonById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLesson(int id)
        {
            _lessonService.DeleteLesson(id);
            return Ok("Урок удалён");
        }
        // https://localhost:50221/api/lesson/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult UpdateLesson(int id,[FromBody]LessonDto lessonDto)
        {
            _repo.UpdateLesson(lessonDto);
            return Ok("Урок обновлён");
        }

        // https://localhost:50221/api/feedback/
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public ActionResult GetFeedbacks()
        {
            var result = _lessonService.GetFeedbacks();
            return Ok(result);
        }

        // https://localhost:50221/api/feedback/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult GetFeedbackById(int id)
        {
            var result = _lessonService.GetFeedbackById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/feedback/
        [HttpPost]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult AddNewFeedback(FeedbackDto feedbackDto)
        {
            _repo.AddFeedback(feedbackDto);
            return Ok("Отзыв добавлен");
        }

        // https://localhost:50221/api/feedback/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateFeedback(int id,[FromBody]FeedbackDto feedbackDto)
        {
            _repo.UpdateFeedback(feedbackDto);
            return Ok("Отзыв обновлён");
        }

        // https://localhost:50221/api/feedback/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteFeedback(int id)
        {
            _lessonService.DeleteFeedback(id);
            return Ok("Отзыв удалён");
        }

        // https://localhost:50221/api/understandingLevel/
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult GetUnderstandingLevels()
        {
            var result = _lessonService.GetUnderstandingLevels();
            return Ok(result);
        }

        // https://localhost:50221/api/understandingLevel/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult GetUnderstandingLevelById(int id)
        {
            var result = _lessonService.GetUnderstandingLevelById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/understandingLevel/
        [HttpPost]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult AddNewUnderstandingLevel(UnderstandingLevelDto understandingLevel)
        {
            _repo.AddUnderstandingLevel(understandingLevel);
            return Ok("Сложность добавлена");
        }

        // https://localhost:50221/api/understandingLevel/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateUnderstandingLevel(int id,[FromBody]UnderstandingLevelDto understandingLevelDto)
        {
            _repo.UpdateUnderstandingLevel(understandingLevelDto);
            return Ok("Сложность обновлена");
        }

        // https://localhost:50221/api/UnderstandingLevel/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteUnderstandingLevel(int id)
        {
            _lessonService.DeleteUnderstandingLevel(id);
            return Ok("Сложность удалена");
        }

        // https://localhost:50221/api/attendance/
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendances()
        {
            var result =_lessonService.GetAttendances();
            return Ok(result);
        }

        // https://localhost:50221/api/attendance/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendanceById(int id)
        {
            var result = _lessonService.GetAttendanceById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/attendance/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewAttendance(AttendanceDto attendance)
        {
            _repo.AddAttendance(attendance);
            return Ok("Посещаемость добавлена");
        }

        // https://localhost:50221/api/lesson/attendance/
        [HttpPut("Attendance")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult UpdateAttendance([FromBody]AttendanceUpdateInputModel attendance)
        {            
            return Ok(_lessonService.UpdateAttendance(new AttendanceDto()
            {
                Id = attendance.Id,
                IsAbsent = attendance.IsAbsent
            }));
        }

        // https://localhost:50221/api/attendance/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteAttendance(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Посещаемость удалена");
        }

        // https://localhost:50221/api/lesson-theme/
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public ActionResult GetLessonTheme()
        {
            var result = _lessonService.GetLessonThemes();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult GetLessonThemeById(int id)
        {
            var result = _lessonService.GetLessonThemeById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson-theme/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLessonTheme(LessonThemeDto lessontheme)
        {
            _repo.AddLessonTheme(lessontheme);
            return Ok("Тема урока добавлена");
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult UpdateLessonTheme(int id,[FromBody]LessonThemeDto lessonTheme)
        {
            _repo.UpdateLessonTheme(lessonTheme);
            return Ok("Тема урока обновлена");
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLessonTheme(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Тема урока удалена");
        }
    }
}

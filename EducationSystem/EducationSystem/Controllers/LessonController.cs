using System;
using System.Collections.Generic;
using AutoMapper;
using EducationSystem.API;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
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
        private IMapper _mapper;

        public LessonController(IMapper mapper)
        {
            _repo = new LessonRepository();
            _lessonMapper = new LessonMapper();
            _lessonService = new LessonService();
            _mapper = mapper;
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

        // https://localhost:50221/api/lesson/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult<List<LessonDto>> GetLessons(int id)
        {
            var result = _lessonService.GetLessonsByGroupId(id);
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
        public ActionResult UpdateLesson(int id, [FromBody] LessonDto lessonDto)
        {
            _repo.UpdateLesson(lessonDto);
            return Ok("Урок обновлён");
        }

        // https://localhost:50221/api/feedback/1/2/2
        [HttpGet("feedback/{lessonId}/{groupId}/{courseId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetFeedbacks(int lessonId, int groupId, int courseId)
        {
            var result = _lessonService.GetFeedbacks(lessonId, groupId, courseId);
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
        public ActionResult UpdateFeedback(int id, [FromBody] FeedbackDto feedbackDto)
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
        public ActionResult UpdateUnderstandingLevel(int id, [FromBody] UnderstandingLevelDto understandingLevelDto)
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
            var result = _lessonService.GetAttendances();
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

        // https://localhost:50221/api/lesson/2/attendance/2
        /// <summary>
        /// Update Attendance.
        /// </summary>
        /// <param name="lessonId">Which lesson the attendance belongs to.</param>
        /// <param name="attendanceId">Attendance changed Id</param>
        /// <param name="attendance">From body attendance object with parameter isAbsent</param>
        /// <returns>Updated rows.</returns>
        [HttpPut("{lessonId}/Attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<int> UpdateAttendance(int lessonId, int attendanceId, [FromBody] AttendanceUpdateInputModel attendance)
        {
            return Ok(_lessonService.UpdateAttendance(_mapper.Map<AttendanceDto>(attendance)));
        }

        // https://localhost:50221/api/attendance/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteAttendance(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Посещаемость удалена");
        }
        // https://localhost:50221/api/lesson/theme/3/lessons
        /// <summary>
        /// Get all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="id">The identifier of the theme that we want to see all its lessons.</param>
        /// <returns>The list of lessonOutputModel.</returns>
        [HttpGet("Theme/{id}/lessons")]
        // [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int id)
        {
            var lessons = _mapper.Map<List<LessonOutputModel>>(_lessonService.GetLessonsByThemeId(id));
            return Ok(lessons);

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
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLessonTheme(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Тема урока удалена");
        }

        // https://localhost:44365/api/lesson/attendance/0
        [HttpGet("attendance/{percent}")]
        [AllowAnonymous]
        public ActionResult GetStudentsByPercentOfSkip(int percent)
        {
            return Ok(_lessonService.GetStudentByPercentOfSkip(percent));
        }
    }
}

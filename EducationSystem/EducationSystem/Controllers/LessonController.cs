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

    public class LessonController : ControllerBase
    {
        private ILessonService _lessonService;
        private IMapper _mapper;

        public LessonController(IMapper mapper, ILessonService lessonService)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        // https://localhost:50221/api/lesson/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLesson([FromBody] LessonInputModel inputModel)
        {
          var lessonDto = _mapper.Map<LessonDto>(inputModel);
            _lessonService.AddLesson(lessonDto);
            return Ok("Урок добавлен");
        }

        // https://localhost:50221/api/lesson/group/id
        [HttpGet("group/{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult<List<LessonDto>> GetLessons(int id)
        {
            var result = _lessonService.GetLessonsByGroupId(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/id
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult GetLessonById(int id)
        {
            var result = _lessonService.GetLessonById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLesson(int id)
        {
            var result = _lessonService.DeleteLesson(id);
            if (result == 1)
                return Ok($"Урок #{id} удален!");
            else
                return Problem($"Ошибка! Не удалось удалить урок #{id}!");
        }


        // https://localhost:50221/api/lesson/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult RecoverLesson(int id)
        {
            var result = _lessonService.RecoverLesson(id);
            if (result == 1)
                return Ok($"Урок #{id} восстановлен!");
            else
                return Problem($"Ошибка! Не удалось восстановить урок #{id}!");
        }

        // https://localhost:50221/api/lesson/id
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult UpdateLesson([FromBody] LessonInputModel inputModel)
        {
            var lessonDto = _mapper.Map<LessonDto>(inputModel);
            _lessonService.UpdateLesson(lessonDto);
            return Ok("Урок обновлён");
        }

        // https://localhost:50221/api/lesson/feedback/1/2/2
        [HttpGet("feedback/{lessonId}/{groupId}/{courseId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetFeedbacks(int lessonId, int groupId, int courseId)
        {
            var result = _lessonService.GetFeedbacks(lessonId, groupId, courseId);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/feedback/id
        [HttpGet("feedback/{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult GetFeedbackById(int id)
        {
            var result = _lessonService.GetFeedbackById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/feedback/
        [HttpPost("feedback")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult AddNewFeedback([FromBody] FeedbackInputModel inputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            _lessonService.AddFeedback(feedbackDto);
            return Ok("Отзыв добавлен");
        }

        // https://localhost:50221/api/lesson/feedback/id
        [HttpPut("feedback/{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateFeedback([FromBody] FeedbackInputModel inputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            _lessonService.UpdateFeedback(feedbackDto);
            return Ok("Отзыв обновлён");
        }

        // https://localhost:50221/api/lesson/feedback/id
        [HttpDelete("feedback/{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteFeedback(int id)
        {
            _lessonService.DeleteFeedback(id);
            return Ok("Отзыв удалён");
        }

        // https://localhost:50221/api/lesson/attendance/
        [HttpGet("attendance")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendances()
        {
            var result = _lessonService.GetAttendances();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/attendance/id
        [HttpGet("attendance/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendanceById(int id)
        {
            var result = _lessonService.GetAttendanceById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/attendance/
        [HttpPost("attendance")]
        //[Authorize(Roles = "Админ, Преподаватель")]
        //public ActionResult AddNewAttendance([FromBody] AttendanceInputModel attendance)
        //{
        //    _lessonService.AddAttendance(attendance);
        //    return Ok("Посещаемость добавлена");
        //}
        //TODO : Список посещаемости, должнен создаваться иначе. 

        // https://localhost:50221/api/lesson/2/attendance/2
        
        [HttpPut("{lessonId}/Attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<int> UpdateAttendance(int lessonId, int attendanceId, [FromBody] AttendanceUpdateInputModel attendance)
        {
            var attandanceDto = _mapper.Map<AttendanceDto>(attendance);
            _lessonService.UpdateAttendance(lessonId, attendanceId, attandanceDto);
            return Ok("Посещаемость изменена");
        }

        // https://localhost:50221/api/attendance/3
        [HttpDelete("attendance/{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteAttendance(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Посещаемость удалена");
        }
        // https://localhost:50221/api/lessons/theme/id
        /// <summary>
        /// Get all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="id">The identifier of the theme that we want to see all its lessons.</param>
        /// <returns>The list of lessonOutputModel.</returns>
        [HttpGet("lessons/Theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int id)
        {
            var lessons = _mapper.Map<List<LessonOutputModel>>(_lessonService.GetLessonsByThemeId(id));
            return Ok(lessons);

        }


        // https://localhost:50221/api/lesson-theme/3
        [HttpGet("lesson-theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult GetLessonThemeById(int id)
        {
            var result = _lessonService.GetLessonThemeById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson-theme/
        [HttpPost("lesson-theme")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLessonTheme([FromBody] LessonThemeInputModel inputModel)
        {
            var lessonThemeDto = _mapper.Map<LessonThemeDto>(inputModel);
            _lessonService.AddLessonTheme(lessonThemeDto);
            return Ok("Тема урока добавлена");
        }



        // https://localhost:50221/api/lesson/lesson-theme/id
        [HttpDelete("lesson-theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLessonTheme(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Тема урока удалена");
        }

        // https://localhost:44365/api/lesson/percent-of-skip/0/by-group/3
        [HttpGet("percent-of-skip/{percent}/by-group/{groupId}")]
        [AllowAnonymous]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetStudentsByPercentOfSkip(int percent, int groupId)
        {
            return Ok(_mapper.Map<List<AttendanceReportOutputModel>>(_lessonService.GetStudentByPercentOfSkip(percent, groupId)));
            //return Ok(_lessonService.GetStudentByPercentOfSkip(percent, groupId));
        }
    }
}

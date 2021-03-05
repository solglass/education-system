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
    // https://localhost:44365/api/lesson/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LessonController : ControllerBase
    {
        private ILessonService _lessonService;
        private IMapper _mapper;

        public LessonController(IMapper mapper, ILessonService lessonService)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        // https://localhost:44365/api/lesson/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult AddNewLesson([FromBody] LessonInputModel inputModel)
        {
            var lessonDto = _mapper.Map<LessonDto>(inputModel);
            var result = _lessonService.AddLesson(lessonDto);
            return Ok($"Урок #{result} добавлен");
        }

        // https://localhost:44365/api/lesson/
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult GetLessons(int id)
        {
            var lessonDtos = _lessonService.GetLessonsByGroupId(id);
            var lessonsList = _mapper.Map<LessonOutputModel>(lessonDtos);
            return Ok(lessonsList);
        }

        // https://localhost:44365/api/lesson/id
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult GetLessonById(int id)
        {
            var lessonDto = _lessonService.GetLessonById(id);
            var lessonModel = _mapper.Map<LessonOutputModel>(lessonDto);
            return Ok(lessonModel);
        }

        // https://localhost:44365/api/lesson/id
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


        // https://localhost:44365/api/lesson/id/recovery
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

        // https://localhost:44365/api/lesson/id
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult UpdateLesson(int lessonId, [FromBody] LessonInputModel lesson)
        {
            var lessonDto = _mapper.Map<LessonDto>(lesson);
            lessonDto.Id = lessonId;
            var result = _lessonService.UpdateLesson(lessonDto);
            return Ok($"Урок #{result} обновлён");
        }

        // https://localhost:44365/api/lesson/feedback
        [HttpGet("feedback")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult GetFeedbacks([FromBody] FeedbackSearchInputModel inputModel)
        {
            var feedbackDtos = _lessonService.GetFeedbacks(inputModel.LessonID, inputModel.GroupID, inputModel.CourseID);
            var feedbackList = _mapper.Map<LessonOutputModel>(feedbackDtos);
            return Ok(feedbackList);
        }

        // https://localhost:44365/api/lesson/feedback/3
        [HttpGet("feedback/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult GetFeedbackById(int id)
        {
            var feedbackDto = _lessonService.GetFeedbackById(id);
            var result = _mapper.Map<LessonOutputModel>(feedbackDto);
            return Ok(result);
        }

        // https://localhost:44365/api/lesson/id/feedback/
        [HttpPost("{id}/feedback")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult AddNewFeedback(FeedbackInputModel inputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            var result = _lessonService.AddFeedback(feedbackDto);
            return Ok($"Отзыв #{result} добавлен");
        }

        // https://localhost:44365/api/lesson/5/feedback/5
        [HttpPut("{id}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateFeedback(int feedbackId, [FromBody] FeedbackInputModel feedbackInputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(feedbackInputModel);
            feedbackDto.ID = feedbackId;
            var result = _lessonService.UpdateFeedback(feedbackDto);
            return Ok($"Отзыв #{result} обновлён");
        }

        // https://localhost:44365/api/lesson/3/feedback/3
        [HttpDelete("{id}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteFeedback(int id)
        {
            _lessonService.DeleteFeedback(id);
            return Ok("Отзыв удалён");
        }

        // https://localhost:44365/api/lesson/5/attendance/
        [HttpGet("{id}/attendance")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendances()
        {
            var attendanceDtos = _lessonService.GetAttendances();
            var listAttendances = _mapper.Map<AttendanceReportOutputModel>(attendanceDtos);
            return Ok(listAttendances);
        }

        // https://localhost:44365/api/lesson/5/attendance/3
        [HttpGet("{id}/attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendanceById(int id)
        {
            var attendanceDto = _lessonService.GetAttendanceById(id);
            var result = _mapper.Map<AttendanceReportOutputModel>(attendanceDto);
            return Ok(result);
        }

        // https://localhost:44365/api/lesson/5/attendance
        [HttpPost("{id}/attendance")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewAttendance(AttendanceInputModel inputModel)
        {
            var attendanceDto = _mapper.Map<AttendanceDto>(inputModel);
            var result = _lessonService.AddAttendance(attendanceDto);
            return Ok($"Посещаемость #{result} добавлен");
        }

        // https://localhost:44365/api/lesson/2/attendance/2

        [HttpPut("{id}/Attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<int> UpdateAttendance(int attendanceId, [FromBody] AttendanceInputModel attendanceInputModel)
        {
            var attendanceDto = _mapper.Map<AttendanceDto>(attendanceInputModel);
            attendanceDto.Id = attendanceId;
            var result = _lessonService.UpdateAttendance(attendanceDto);
            return Ok($"Посещаемость #{result} обновлёна");
        }

        // https://localhost:44365/api//lesson/attendance/3
        [HttpDelete("attendance/{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteAttendance(int id)
        {
            _lessonService.DeleteAttendance(id);
            return Ok("Посещаемость удалена");
        }

        // https://localhost:44365/api/lesson/lessons-by-theme/3/
        /// <summary>
        /// Get all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="id">The identifier of the theme that we want to see all its lessons.</param>
        /// <returns>The list of lessonOutputModel.</returns>
        [HttpGet("lessons-by-theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int id)
        {
            var lessons = _mapper.Map<List<LessonOutputModel>>(_lessonService.GetLessonsByThemeId(id));
            return Ok(lessons);
        }


        // https://localhost:44365/api/lesson/lesson-theme/3
        [HttpGet("lesson-theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult GetLessonThemeById(int id)
        {
            var lessonThemeDto = _lessonService.GetLessonThemeById(id);
            var result = _mapper.Map<LessonThemeOutputModel>(lessonThemeDto);
            return Ok(result);
        }

        // https://localhost:44365/api/lesson/theme/
        [HttpPost("{theme")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLessonTheme(LessonThemeInputModel lessonThemeModel)
        {
            var lessonThemeDto = _mapper.Map<LessonThemeDto>(lessonThemeModel);
            var result = _lessonService.AddLessonTheme(lessonThemeDto);
            return Ok($"Тема урока #{result} добавлен");
        }

        // https://localhost:44365/api/lesson/theme/3
        [HttpDelete("theme/{themeId}")]
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
        }
    }
}

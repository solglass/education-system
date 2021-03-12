using System.Collections.Generic;
using AutoMapper;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/lesson/
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

        // https://localhost:50221/api/lesson/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult<LessonOutputModel> AddNewLesson([FromBody] LessonInputModel inputModel)
        {
            var lessonDto = _mapper.Map<LessonDto>(inputModel);
            var result = _mapper.Map < LessonOutputModel >(_lessonService.GetLessonById(_lessonService.AddLesson(lessonDto)));

            return Ok(result);
        }

        // https://localhost:50221/api/lesson/
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult<List<LessonOutputModel>> GetLessons(int id)
        {
            var lessonDtos = _lessonService.GetLessonsByGroupId(id);
            var lessonsList = _mapper.Map<List<LessonOutputModel>>(lessonDtos);
            return Ok(lessonsList);
        }

        // https://localhost:50221/api/lesson/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент")]
        public ActionResult<LessonOutputModel> GetLessonById(int id)
        {
            var lessonDto = _lessonService.GetLessonById(id);
            var lessonModel = _mapper.Map<LessonOutputModel>(lessonDto);
            return Ok(lessonModel);
        }

        // https://localhost:50221/api/lesson/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<LessonOutputModel> DeleteLesson(int id)
        {
           _lessonService.DeleteLesson(id);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(id));
            return Ok(result);
        }


        // https://localhost:50221/api/lesson/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<LessonOutputModel> RecoverLesson(int id)
        {
             _lessonService.RecoverLesson(id);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(id));
            return Ok(result);
        }


        // https://localhost:50221/api/lesson/5
        [HttpPut("{lessonId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<LessonOutputModel> UpdateLesson(int lessonId, [FromBody] LessonInputModel lesson)
        {
            var lessonDto = _mapper.Map<LessonDto>(lesson);
            lessonDto.Id = lessonId;
           _lessonService.UpdateLesson(lessonDto);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(lessonId));
            return Ok(result);
        }
        
        // https://localhost:50221/api/lesson/5/feedback
        [HttpGet("{lessonId}/feedback")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<FeedbackOutputModel>> GetFeedbacks(int lessonId, [FromBody] FeedbackSearchInputModel inputModel)
        {
            var feedbackDtos = _lessonService.GetFeedbacks(inputModel.LessonId, inputModel.GroupId, inputModel.CourseId);
            var feedbackList = _mapper.Map<List<FeedbackOutputModel>>(feedbackDtos);
            return Ok(feedbackList);
        }
        
        // https://localhost:50221/api/lesson/5/feedback/3
        [HttpGet("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист, Преподаватель")]
        public ActionResult<FeedbackOutputModel> GetFeedbackById(int lessonId, int feedbackId)
        {
            var feedbackDto = _lessonService.GetFeedbackById(feedbackId);
            var result = _mapper.Map<FeedbackOutputModel>(feedbackDto);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/id/feedback/
        [HttpPost("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<FeedbackOutputModel> AddNewFeedback(int lessonId, FeedbackInputModel inputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(_lessonService.AddFeedback(lessonId, feedbackDto)));
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/5/feedback/5
        [HttpPut("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateFeedback(int lessonId, int feedbackId, [FromBody] FeedbackInputModel feedbackInputModel)
        {
            var feedbackDto = _mapper.Map<FeedbackDto>(feedbackInputModel);
            feedbackDto.Id = feedbackId;
            _lessonService.UpdateFeedback(lessonId, feedbackId, feedbackDto);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(feedbackId));
            return Ok(result);
        }

        // https://localhost:44365/api/lesson/3/feedback/3
        [HttpDelete("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteFeedback(int lessonId, int feedbackId)
        {
            _lessonService.DeleteFeedback(feedbackId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // https://localhost:50221/api/lesson/5/attendance/
        [HttpGet("{id}/attendance")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult<List<AttendanceOutputModel>> GetAttendancesByLessonId(int lessonId)
        {
            var attendanceDtos = _lessonService.GetAttendancesByLessonId(lessonId);
            var listAttendances = _mapper.Map<List<AttendanceOutputModel>>(attendanceDtos);
            return Ok(listAttendances);
        }

        // https://localhost:50221/api/lesson/5/attendance/3
        [HttpGet("{id}/attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult<AttendanceOutputModel> GetAttendanceById(int lessonId, int attendanceId)
        {
            var attendanceDto = _lessonService.GetAttendanceById(attendanceId);
            var result = _mapper.Map<AttendanceOutputModel>(attendanceDto);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/5/attendance
        [HttpPost("{id}/attendance")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult <AttendanceOutputModel> AddNewAttendance(int lessonId, [FromBody] AttendanceInputModel inputModel)
        {
            var attendanceDto = _mapper.Map<AttendanceDto>(inputModel);
            var result = _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(_lessonService.AddAttendance(lessonId, attendanceDto)));
            return Ok(result);
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
        public ActionResult<AttendanceOutputModel> UpdateAttendance(int lessonId, int attendanceId, [FromBody] AttendanceInputModel attendanceInputModel)
        {
            var attendanceDto = _mapper.Map<AttendanceDto>(attendanceInputModel);
            _lessonService.UpdateAttendance(lessonId, attendanceId, attendanceDto);
            var result = _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(attendanceId));
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/iD/
        [HttpDelete("{lessonId}/attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteAttendance(int lessonId, int attendanceId)
        {
            _lessonService.DeleteAttendance(attendanceId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // https://localhost:50221/api/lesson/by-theme/14
        /// <summary>
        /// Get all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="id">The identifier of the theme that we want to see all its lessons.</param>
        /// <returns>The list of lessonOutputModel.</returns>
        [HttpGet("by-theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int themeId)
        {
            var lessons = _mapper.Map<List<LessonOutputModel>>(_lessonService.GetLessonsByThemeId(themeId));
            return Ok(lessons);
        }

        // https://localhost:50221/api/lesson/45/theme/54
        [HttpPost("{lessonId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLessonTheme(int lessonId, int themeId)
        {
            var result = _lessonService.AddLessonTheme(lessonId, themeId);
            return StatusCode(StatusCodes.Status201Created);
        }

        // https://localhost:50221/api/lesson/45/theme/54
        [HttpDelete("{lessonId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLessonTheme(int lessonId, int themeId)
        {
             _lessonService.DeleteLessonTheme(lessonId, themeId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}

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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/lesson/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
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
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<LessonOutputModel> UpdateLesson(int lessonId, [FromBody] LessonInputModel lesson)
        {
            var lessonDto = _mapper.Map<LessonDto>(lesson);
            lessonDto.Id = lessonId;
           _lessonService.UpdateLesson(lessonDto);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(lessonId));
            return Ok(result);
        }
        
        // https://localhost:50221/api/lesson/feedback
        [HttpGet("feedback")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<FeedbackOutputModel>> GetFeedbacks([FromBody] FeedbackSearchInputModel inputModel)
        {
            var feedbackDtos = _lessonService.GetFeedbacks(inputModel.LessonID, inputModel.GroupID, inputModel.CourseID);
            var feedbackList = _mapper.Map<List<FeedbackOutputModel>>(feedbackDtos);
            return Ok(feedbackList);
        }
        
        // https://localhost:50221/api/lesson/feedback/3
        [HttpGet("feedback/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<FeedbackOutputModel> GetFeedbackById(int id)
        {
            var feedbackDto = _lessonService.GetFeedbackById(id);
            var result = _mapper.Map<FeedbackOutputModel>(feedbackDto);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/id/feedback/
        [HttpPost("{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<FeedbackOutputModel> AddNewFeedback(int lessonId, FeedbackInputModel inputModel)
        {
            inputModel.LessonId = lessonId;
            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(_lessonService.AddFeedback(feedbackDto)));
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/5/feedback/5
        [HttpPut("{id}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<FeedbackOutputModel> UpdateFeedback(int feedbackId, int lessonId, [FromBody] FeedbackInputModel feedbackInputModel)
        {
            feedbackInputModel.LessonId = lessonId;
            var feedbackDto = _mapper.Map<FeedbackDto>(feedbackInputModel);
            feedbackDto.Id = feedbackId;
             _lessonService.UpdateFeedback(feedbackDto);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(feedbackId));
            return Ok(result);
        }

        // https://localhost:44365/api/lesson/3/feedback/3
        [HttpDelete("{id}/feedback/{feedbackId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<int> DeleteFeedback(int id)
        {
             _lessonService.DeleteFeedback(id);
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

        // https://localhost:50221/attendance-by-user/userId
        [HttpGet("attendance-by-user")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult<List<AttendanceOutputModel>> GetAttendancesByUserId(int userId)
        {
            var attendanceDtos = _lessonService.GetAttendancesByUserId(userId);
            var listAttendances = _mapper.Map<List<AttendanceOutputModel>>(attendanceDtos);
            return Ok(listAttendances);
        }

        // https://localhost:50221/api/lesson/5/attendance/3
        [HttpGet("{id}/attendance/{attendanceId}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult<AttendanceOutputModel> GetAttendanceById(int lessonId, int attendanceId)
        {
            var attendanceDto = _lessonService.GetAttendanceById(attendanceId);
            attendanceDto.Lesson = new LessonDto { Id = lessonId };
            var result = _mapper.Map<AttendanceOutputModel>(attendanceDto);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/5/attendance
        [HttpPost("{id}/attendance")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<AttendanceOutputModel> AddNewAttendance(AttendanceInputModel inputModel)
        {
            var attendanceDto = _mapper.Map<AttendanceDto>(inputModel);
            var result =  _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(_lessonService.AddAttendance(attendanceDto)));
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
            attendanceDto.Id = attendanceId;
            attendanceDto.Lesson = new LessonDto { Id = lessonId };
            _lessonService.UpdateAttendance(attendanceDto);
            var result = _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(attendanceId));
            return Ok(result);
        }

        // https://localhost:50221/api/lessoniD/
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult <int> DeleteAttendance(int id)
        {
            _lessonService.DeleteAttendance(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // https://localhost:50221/api/lesson/theme/3/lessons
        /// <summary>
        /// Get all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="id">The identifier of the theme that we want to see all its lessons.</param>
        /// <returns>The list of lessonOutputModel.</returns>
        [HttpGet("Theme/{id}/lessons")]
        // [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int themeId)
        {
            var lessons = _mapper.Map<List<LessonOutputModel>>(_lessonService.GetLessonsByThemeId(themeId));
            return Ok(lessons);
        }


        // https://localhost:50221/api/lesson/id/lesson-theme/
        [HttpPost("{id}/lesson-theme")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult AddNewLessonTheme(int lessonId, LessonThemeInputModel lessonThemeModel)
        {
            lessonThemeModel.LessonId = lessonId;
            var lessonThemeDto = _mapper.Map<LessonThemeDto>(lessonThemeModel);
            _lessonService.AddLessonTheme(lessonThemeDto);

            return StatusCode(StatusCodes.Status201Created);
        }

        // https://localhost:50221/api/lesson-theme/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteLessonTheme(int lessonId, int themeId)
        {
             _lessonService.DeleteLessonTheme(lessonId, themeId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // https://localhost:44365/api/lesson/percent-of-skip/0/by-group/3
        [HttpGet("percent-of-skip/{percent}/by-group/{groupId}")]
        [AllowAnonymous]
        //[Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult <List<AttendanceReportOutputModel>> GetStudentsByPercentOfSkip(int percent, int groupId)
        {
            return Ok(_mapper.Map<List<AttendanceReportOutputModel>>(_lessonService.GetStudentByPercentOfSkip(percent, groupId)));
        }
    }
}

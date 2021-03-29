using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EducationSystem.API.Controllers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Core.CustomExceptions;
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
        private IGroupService _groupService;
        private ICourseService _courseService;
        private IMapper _mapper;

        public LessonController(IMapper mapper, ILessonService lessonService, IGroupService groupService, ICourseService courseService)
        {
            _lessonService = lessonService;
            _groupService = groupService;
            _courseService = courseService;
            _mapper = mapper;


        }
        /// <summary>
        /// Adds new lesson
        /// </summary>
        /// <param name="inputModel">Input model with all the properties for the new lesson</param>
        /// <returns>Output model of the added lesson</returns>
        [ProducesResponseType(typeof(LessonOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/
        [HttpPost]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<LessonOutputModel> AddNewLesson([FromBody] LessonInputModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var lessonDto = _mapper.Map<LessonDto>(inputModel);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(_lessonService.AddLesson(lessonDto)));


            return Ok(result);
        }
        /// <summary>
        /// Gets all lessons of the Group
        /// </summary>
        /// <param name="groupId">Group id </param>
        /// <returns>List of Output models of the found Lessons </returns>
        [ProducesResponseType(typeof(List<LessonOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/by-group/45
        [HttpGet("by-group/{groupId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByGroupId(int groupId)
        {

            var _userGroups = this.SupplyUserGroupsList( _groupService);
            var group = _groupService.GetGroupById(groupId);
            if (group == null)
                return NotFound($"Group {groupId} was not found");

            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(groupId)))
                return Forbid($"User is not in group {groupId}");

            var lessonDtos = _lessonService.GetLessonsByGroupId(groupId);
            var lessonsList = _mapper.Map<List<LessonOutputModel>>(lessonDtos);

            return Ok(lessonsList);
        }

        /// <summary>
        /// Gets lesson by id 
        /// </summary>
        /// <param name="lessonId"> Id of the lesson</param>
        /// <returns>Output model of the found Lesson </returns>
        [ProducesResponseType(typeof(LessonOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/3
        [HttpGet("{lessonId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Студент")]
        public ActionResult<LessonOutputModel> GetLessonById(int lessonId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);

            var lessonDto = _lessonService.GetLessonById(lessonId);

            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");

            var lessonModel = _mapper.Map<LessonOutputModel>(lessonDto);


            return Ok(lessonModel);
        }
        /// <summary>
        /// Deletes lesson (soft-delete)
        /// </summary>
        /// <param name="lessonId"> Id of the lesson</param>
        /// <returns>Output model of the soft-deleted Lesson</returns>
        [ProducesResponseType(typeof(LessonOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/34
        [HttpDelete("{lessonId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<LessonOutputModel> DeleteLesson(int lessonId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            _lessonService.DeleteLesson(lessonId);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(lessonId));
            return Ok(result);
        }

        /// <summary>
        /// Recovers deleted lesson
        /// </summary>
        /// <param name="lessonId"> Id of the lesson</param>
        /// <returns>Output model of the recovered Lesson</returns>
        [ProducesResponseType(typeof(LessonOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/44/recovery
        [HttpPut("{lessonId}/recovery")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<LessonOutputModel> RecoverLesson(int lessonId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            _lessonService.RecoverLesson(lessonId);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(lessonId));
            return Ok(result);
        }

        /// <summary>
        /// Updates lesson's properties
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="inputModel">Input model with all the properties for the new lesson</param>
        /// <returns>Output model of the updated Lesson</returns>
        [ProducesResponseType(typeof(LessonOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/5
        [HttpPut("{lessonId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<LessonOutputModel> UpdateLesson(int lessonId, [FromBody] LessonInputModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            lessonDto = _mapper.Map<LessonDto>(inputModel);
            lessonDto.Id = lessonId;
            _lessonService.UpdateLesson(lessonDto);
            var result = _mapper.Map<LessonOutputModel>(_lessonService.GetLessonById(lessonId));
            return Ok(result);
        }

        /// <summary>
        /// Gets all feedbacks for the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="inputModel">Input model with all the properties of the feedback search input model</param>
        /// <returns>List of feedback output models for the lesson</returns>
        [ProducesResponseType(typeof(List<FeedbackOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/5/feedback
        [HttpGet("{lessonId}/feedback")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<List<FeedbackOutputModel>> GetFeedbacks(int lessonId, [FromBody] FeedbackSearchInputModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            var groupDto = _groupService.GetGroupById((int)inputModel.GroupId);
            if (groupDto == null)
                return NotFound($"Group {groupDto} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            if (lessonDto.Group.Id != (int)inputModel.GroupId)
                return BadRequest($"Lesson with id {lessonId} does not belong to the group with id {(int)inputModel.GroupId}");
            var feedbackDtos = _lessonService.GetFeedbacks(inputModel.LessonId, inputModel.GroupId, inputModel.CourseId);
            var feedbackList = _mapper.Map<List<FeedbackOutputModel>>(feedbackDtos);
            return Ok(feedbackList);
        }

        /// <summary>
        /// Gets feedback by id and lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="feedbackId">Id of the feedback</param>
        /// <returns>Output model of the found feedback</returns>
        [ProducesResponseType(typeof(FeedbackOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // https://localhost:50221/api/lesson/5/feedback/3
        [HttpGet("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Администратор, Менеджер, Методист, Преподаватель")]
        public ActionResult<FeedbackOutputModel> GetFeedbackById(int lessonId, int feedbackId)
        {
            var lessonDto = _lessonService.GetLessonById(lessonId);
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var feedbackDto = _lessonService.GetFeedbackById(feedbackId);
            if (feedbackDto == null)
                return NotFound($"Feedback {feedbackId} was not found");
            if (feedbackDto.Lesson.Id != lessonId)
                return BadRequest($"Feedback with id {feedbackId} does not belong to the lesson with id {lessonId}");

            var result = _mapper.Map<FeedbackOutputModel>(feedbackDto);
            return Ok(result);
        }


        /// <summary>
        /// Adds new feedback to the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="inputModel">>Input model with all the properties for the new feedback</param>
        /// <returns>Output model of the created feedback</returns>
        [ProducesResponseType(typeof(FeedbackOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/id/feedback/
        [HttpPost("{lessonId}/feedback")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult<FeedbackOutputModel> AddNewFeedback(int lessonId, FeedbackInputModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");

            var feedbackDto = _mapper.Map<FeedbackDto>(inputModel);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(_lessonService.AddFeedback(lessonId, feedbackDto)));
            return Ok(result);
        }

        /// <summary>
        /// Updates feedback for the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="feedbackId">Id of the feedback</param>
        /// <param name="feedbackInputModel">Input model with all the properties for the feedback to update</param>
        /// <returns>Output model of the updated feedback</returns>
        [ProducesResponseType(typeof(FeedbackOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/5/feedback/5
        [HttpPut("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult<FeedbackOutputModel> UpdateFeedback(int lessonId, int feedbackId, [FromBody] FeedbackInputModel feedbackInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var _userId = Convert.ToInt32(User.FindFirst("id").Value);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var feedbackDto = _lessonService.GetFeedbackById(feedbackId);
            if (feedbackDto == null)
                return NotFound($"Feedback {feedbackId} was not found");
            if (feedbackDto.Lesson.Id != lessonId)
                return BadRequest($"Feedback with id {feedbackId} does not belong to the lesson with id {lessonId}");
            if (feedbackDto.User.Id != _userId && !(User.IsInRole("Администратор")))
                return Forbid($"User cannot update feedback {feedbackDto.Id}");


            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            feedbackDto = _mapper.Map<FeedbackDto>(feedbackInputModel);
            feedbackDto.Id = feedbackId;
            _lessonService.UpdateFeedback(lessonId, feedbackId, feedbackDto);
            var result = _mapper.Map<FeedbackOutputModel>(_lessonService.GetFeedbackById(feedbackId));
            return Ok(result);
        }

        /// <summary>
        /// Deletes feedback for the lesson (hard-delete)
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="feedbackId">Id of the feedback</param>
        /// <returns>Status code 204 (no content) </returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // https://localhost:44365/api/lesson/3/feedback/3
        [HttpDelete("{lessonId}/feedback/{feedbackId}")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult DeleteFeedback(int lessonId, int feedbackId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var _userId = Convert.ToInt32(User.FindFirst("id").Value);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var feedbackDto = _lessonService.GetFeedbackById(feedbackId);
            if (feedbackDto == null)
                return NotFound($"Feedback {feedbackId} was not found");
            if (feedbackDto.Lesson.Id != lessonId)
                return BadRequest($"Feedback with id {feedbackId} does not belong to the lesson with id {lessonId}");
            if (feedbackDto.User.Id != _userId && !(User.IsInRole("Администратор")))
                return Forbid($"User cannot delete feedback {feedbackDto.Id}");

            _lessonService.DeleteFeedback(feedbackId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Gets attendences for the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <returns>List of attendences output models for the lesson</returns>
        [ProducesResponseType(typeof(List<AttendanceOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/5/attendance/
        [HttpGet("{lessonId}/attendance")]
        [Authorize(Roles = "Администратор, Преподаватель, Менеджер")]
        public ActionResult<List<AttendanceOutputModel>> GetAttendancesByLessonId(int lessonId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");

            var attendanceDtos = _lessonService.GetAttendancesByLessonId(lessonId);
            var listAttendances = _mapper.Map<List<AttendanceOutputModel>>(attendanceDtos);
            return Ok(listAttendances);
        }

        /// <summary>
        /// Gets attendence for the lesson by id
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="attendanceId">Id of attendance</param>
        /// <returns>Attendence output model for the lesson</returns>
        [ProducesResponseType(typeof(AttendanceOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // https://localhost:50221/api/lesson/5/attendance/3
        [HttpGet("{lessonId}/attendance/{attendanceId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Менеджер")]
        public ActionResult<AttendanceOutputModel> GetAttendanceById(int lessonId, int attendanceId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var attendanceDto = _lessonService.GetAttendanceById(attendanceId);
            if (attendanceDto == null)
                return NotFound($"Attendence {attendanceId} was not found");
            if (attendanceDto.Lesson.Id != lessonId)
                return BadRequest($"Attendence with id {attendanceId} does not belong to the lesson with id {lessonId}");
            var result = _mapper.Map<AttendanceOutputModel>(attendanceDto);
            return Ok(result);
        }

        /// <summary>
        /// Adds attendence for the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="inputModel">Input model with all the properties for the attendence</param>
        /// <returns>Added attendence output model </returns>
        [ProducesResponseType(typeof(AttendanceOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        // https://localhost:50221/api/lesson/5/attendance
        [HttpPost("{lessonId}/attendance")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<AttendanceOutputModel> AddNewAttendance(int lessonId, [FromBody] AttendanceInputModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");

            var attendanceDto = _mapper.Map<AttendanceDto>(inputModel);
            var result = _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(_lessonService.AddAttendance(lessonId, attendanceDto)));
            return Ok(result);
        }


        /// <summary>
        /// Updates Attendance.
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="attendanceId">Id of Attendance </param>
        /// <param name="attendanceInputModel">Input model with all the properties for the attendence</param>
        /// <returns>Updated attendence output model</returns>
        // https://localhost:50221/api/lesson/2/attendance/2
        [ProducesResponseType(typeof(AttendanceOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{lessonId}/Attendance/{attendanceId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]

        public ActionResult<AttendanceOutputModel> UpdateAttendance(int lessonId, int attendanceId, [FromBody] AttendanceInputModel attendanceInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var _userId = Convert.ToInt32(User.FindFirst("id").Value);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var attendanceDto = _lessonService.GetAttendanceById(attendanceId);
            if (attendanceDto == null)
                return NotFound($"Attendence {attendanceId} was not found");
            if (attendanceDto.Lesson.Id != lessonId)
                return BadRequest($"Attendence with id {attendanceId} does not belong to the lesson with id {lessonId}");
            if (attendanceDto.User.Id != _userId && !(User.IsInRole("Администратор")))
                return Forbid($"User cannot update attendence {attendanceDto.Id}");

            attendanceDto = _mapper.Map<AttendanceDto>(attendanceInputModel);
            _lessonService.UpdateAttendance(lessonId, attendanceId, attendanceDto);
            var result = _mapper.Map<AttendanceOutputModel>(_lessonService.GetAttendanceById(attendanceId));
            return Ok(result);
        }

        /// <summary>
        /// Deletes Attendance (hard-delete).
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="attendanceId">Id of Attendance </param>
        /// <returns>Status code 204 (no content)</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // https://localhost:50221/api/lesson/Id/
        [HttpDelete("{lessonId}/attendance/{attendanceId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult DeleteAttendance(int lessonId, int attendanceId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var _userId = Convert.ToInt32(User.FindFirst("id").Value);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var attendanceDto = _lessonService.GetAttendanceById(attendanceId);
            if (attendanceDto == null)
                return NotFound($"Attendence {attendanceId} was not found");
            if (attendanceDto.Lesson.Id != lessonId)
                return BadRequest($"Attendence with id {attendanceId} does not belong to the lesson with id {lessonId}");

            if (attendanceDto.User.Id != _userId && !(User.IsInRole("Администратор")))
                return Forbid($"User cannot delete  attendence {attendanceDto.Id}");

            _lessonService.DeleteAttendance(attendanceId);
            return StatusCode(StatusCodes.Status204NoContent);
        }


        /// <summary>
        /// Gets all lessons that belong to one theme and are not deleted.
        /// </summary>
        /// <param name="themeId">Id of the theme</param>
        /// <returns>The list of Lesson output models</returns>
        // https://localhost:50221/api/lesson/by-theme/14
        [ProducesResponseType(typeof(List<LessonOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("by-theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<List<LessonOutputModel>> GetLessonsByThemeId(int themeId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);


            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto == null)
                return NotFound($"Theme {themeId} was not found");

            var lessonDtos = _lessonService.GetLessonsByThemeId(themeId);
            lessonDtos.ForEach(lesson =>
            {
                if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lesson.Group.Id)))
                    lessonDtos.Remove(lesson);
            });

            var lessons = _mapper.Map<List<LessonOutputModel>>(lessonDtos);
            return Ok(lessons);
        }

        /// <summary>
        /// Adds new theme for the lesson
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="themeId">Id of the theme</param>
        /// <returns>Status code 201 (created)</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // https://localhost:50221/api/lesson/45/theme/54
        [HttpPost("{lessonId}/theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult AddNewLessonTheme(int lessonId, int themeId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto == null)
                return NotFound($"Theme {themeId} was not found");

            var result = _lessonService.AddLessonTheme(lessonId, themeId);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Deletes theme of the lesson (hard-delete).
        /// </summary>
        /// <param name="lessonId">Id of the lesson</param>
        /// <param name="themeId">Id of the theme</param>
        /// <returns>Status code 204 (no content)</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // https://localhost:50221/api/lesson/45/theme/54
        [HttpDelete("{lessonId}/theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult DeleteLessonTheme(int lessonId, int themeId)
        {
            var _userGroups = this.SupplyUserGroupsList(_groupService);
            var lessonDto = _lessonService.GetLessonById(lessonId);
            if (lessonDto == null)
                return NotFound($"Lesson {lessonId} was not found");
            if (!(User.IsInRole("Администратор")) && !(_userGroups.Contains(lessonDto.Group.Id)))
                return Forbid($"User is not in group {lessonDto.Group.Id}");
            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto == null)
                return NotFound($"Theme {themeId} was not found");
            var themeFound = lessonDto.Themes.FirstOrDefault(theme => theme.Id == themeDto.Id);
            if (themeFound == null)
                return BadRequest($"Theme {themeId} with id {themeId} does not belong to the lesson with id {lessonId}");

            _lessonService.DeleteLessonTheme(lessonId, themeId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}

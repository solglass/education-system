using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EducationSystem.Core.CustomExceptions;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.Controllers
{
    // https://localhost:44365/api/group/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private IGroupService _service;
        private ICourseService _courseService;
        private ILessonService _lessonService;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupService groupService, 
            ICourseService courseService, ILessonService lessonService)
        {
            _service = groupService;
            _courseService = courseService;
            _lessonService = lessonService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the groups with their courses and groupStatuses
        /// </summary>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroups()
        {
            var groups = _service.GetGroups();
            var result = _mapper.Map<List<GroupOutputModel>>(groups);

            return Ok(result);
        }

        /// <summary>
        /// Gets only one group by its id with its course and groupStatus
        /// </summary>
        /// <param name="id"> is used to find necessary group</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/3
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист, Преподаватель, Тьютор, Студент")]
        public ActionResult<GroupOutputModel> GetGroupById(int id)
        {
            var group = _service.GetGroupById(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(group);
            return Ok(result);
        }

        /// <summary>
        /// Gets all the groups with their courses and groupStatuses
        /// </summary>
        /// <param name="themeId"> is used to find necessary groups by theme-id</param>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/3
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<GroupOutputModel>> GetGroupByThemeId(int themeId)
        {
            var group = _service.GetGroupByThemeId(themeId);
            List<GroupOutputModel> result = _mapper.Map<List<GroupOutputModel>>(group);
            return Ok(result);
        }

        /// <summary>
        /// Gets all the groups that do not have a tutor with their courses and groupStatuses
        /// </summary>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/without-tutors
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("without-tutors")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroupsWithoutTutors()
        {
            var result = _mapper.Map<List<GroupOutputModel>>(_service.GetGroupsWithoutTutors());
            return Ok(result);
        }

        /// <summary>
        /// Gets all the groups  with their courses, groupStatuses and list their themes
        /// </summary>
        /// <param name="id"> is used to find necessary groups by id</param>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/2/programs
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{id}/programs")]
        [Authorize(Roles = "Админ, Менеджер, Методист, Преподаватель")]
        public ActionResult<GroupOutputModel> GetGroupProgramsByGroupId(int id)
        {
            var program = _service.GetGroupProgramsByGroupId(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(program);
            return Ok(result);
        }

        /// <summary>
        /// Creates Course
        /// </summary>
        /// <param name="group"> is used to get all the information about new group that is necessary to create it</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<GroupOutputModel> AddNewGroup([FromBody] GroupInputModel group)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var addedGroupId = _service.AddGroup(_mapper.Map<GroupDto>(group));
            var result = _mapper.Map<GroupOutputModel>(_service.GetGroupById(addedGroupId));
            return Ok(result);
        }

        /// <summary>
        /// Updates group
        /// </summary>
        /// /// <param name="id"> is used to find the group user wants to update</param>
        /// <param name="group"> is used to provide new information about selected group</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/2
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<GroupOutputModel> UpdateGroupInfo(int id, [FromBody] GroupInputModel group)
        {
          if (_service.GetGroupById(id) == null)
          {
            return Ok("Ошибка! Отсутствует группа с введенным id!");
          }
          var groupDto = _mapper.Map<GroupDto>(group);
          _service.UpdateGroup(groupDto);
          var result = _mapper.Map<GroupOutputModel>(_service.GetGroupById(id));
          return Ok(result);
        }

        /// <summary>
        /// Deletes group
        /// </summary>
        /// /// <param name="id"> is used to find the group user wants to delete</param>
        /// <returns>Returns the GroupOutputModel</returns>
        // https://localhost:44365/api/group/2
        [ProducesResponseType(typeof(GroupOutputModel), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<GroupOutputModel> DeleteGroup(int id)
        {

            _service.DeleteGroup(id);
            var deleteResult = _mapper.Map<GroupOutputModel>(_service.GetGroupById(id));
            return Ok(deleteResult);
        }

        /// <summary>
        /// Creates the connection between one group and one material
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with material</param>
        /// <param name="materialId"> is used to find the material which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        // https://localhost:XXXXX/api/group/id/material/id
        [HttpPost("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            _service.AddGroup_Material(groupId, materialId);
            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Deletes the connection between one group and one material
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with material</param>
        /// <param name="materialId"> is used to find the material which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // https://localhost:XXXXX/api/group/id/material/id
        [HttpDelete("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteGroup_Material(int groupId, int materialId)
        {
            var result = _service.DeleteGroup_Material(groupId, materialId);
            if (result > 0)
                return NoContent();
            else
                return Problem("Ошибка!");
        }

        /// <summary>
        /// Deletes the connection between one group and one teacher
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with teacher</param>
        /// <param name="userId"> is used to find the teacher which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // https://localhost:XXXXX/api/group/id/teacher/id
        [HttpDelete("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteTeacherGroup(int groupId, int userId)
        {
            _service.DeleteTeacherGroup(userId, groupId);
            return NoContent();
        }

        /// <summary>
        /// Creates the connection between one group and one teacher
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with teacher</param>
        /// <param name="userId"> is used to find the teacher which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        // https://localhost:XXXXX/api/group/id/teacher/id
        [HttpPost("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTeacherGroup(int groupId, int userId)
        {
            _service.AddTeacherGroup(groupId, userId);
            return StatusCode(StatusCodes.Status201Created);
        }


        /// <summary>
        /// Deletes the connection between one group and one student
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with student</param>
        /// <param name="userId"> is used to find the student which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // https://localhost:XXXXX/api/group/id/student/id
        [HttpDelete("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteStudentGroup(int groupId, int userId)
        {
            _service.DeleteStudentGroup(userId, groupId);
            return NoContent();
        }

        /// <summary>
        /// Creates the connection between one group and one student
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with student</param>
        /// <param name="userId"> is used to find the student which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        // https://localhost:XXXXX/api/group/id/student/id
        [HttpPost("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddStudentGroup(int groupId, int userId, StudentGroupOutputModel studentGroupInputModel)
        {
            var studentGroupDto = _mapper.Map<StudentGroupDto>(studentGroupInputModel);
            var id = _service.AddStudentGroup(groupId, userId, studentGroupDto);
            var outputModel = _mapper.Map<StudentGroupOutputModel>(_service.GetStudentGroupById(id));
            return Ok(outputModel);
        }

        /// <summary>
        /// Deletes the connection between one group and one tutor
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to to break the connection with tutor</param>
        /// <param name="userId"> is used to find the tutor which one user wants to break the connection with group</param>
        /// <returns>Returns NoContent result</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        // https://localhost:XXXXX/api/group/id/tutor/id
        [HttpDelete("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteTutorGroups(int groupId, int userId)
        {
            _service.DeleteTutorGroup(userId, groupId);
            return NoContent();
        }


        /// <summary>
        /// Creates the connection between one group and one tutor
        /// </summary>
        /// <param name="groupId"> is used to find the group which one user wants to connect with tutor</param>
        /// <param name="userId"> is used to find the teacher which one user wants to connect with group</param>
        /// <returns>Returns Created result</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        // https://localhost:XXXXX/api/group/id/tutor/id
        [HttpPost("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTutorToGroup(int groupId, int userId)
        {
            _service.AddTutorToGroup(userId, groupId);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Gets all the reports
        /// </summary>
        /// <returns>Returns the list of GroupReportOutputModel</returns>
        [ProducesResponseType(typeof(List<GroupReportOutputModel>), StatusCodes.Status200OK)]
        // https://localhost:XXXXX/api/group/report
        [HttpGet("report")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор, Методист")]
        public ActionResult<List<GroupReportOutputModel>> GetReport()
        {
            List<GroupReportOutputModel> report ;

            try
            {
                var reportDto = _service.GenerateReport();
                report = _mapper.Map<List<GroupReportOutputModel>>(reportDto);
      }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(report);
        }

        /// <summary>
        /// Gets a list of themes not covered by the group
        /// </summary>
        /// <param name="id"> is used to find necessary themes by groupId</param>
        /// <returns>Returns the list of ThemeOutputModel</returns>
        [ProducesResponseType(typeof(List<ThemeOutputModel>), StatusCodes.Status200OK)]
        // https://localhost:50221/api/group/2/uncovered-themes
        [HttpGet("{id}/uncovered-themes")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<List<ThemeOutputModel>> GetUncoveredThemesByGroupId(int id)
        {
            var result = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetUncoveredThemesByGroupId(id));
            return Ok(result);
        }

        /// <summary>
        /// Gets only one students which have percent of skipped lessons more than param percent
        /// </summary>
        /// <param name="groupId"> is used to find necessary students by groupId</param>
        /// <param name="percent"> is used to find necessary students which have percent of skipped lessons more than this param</param>
        /// <returns>Returns the lust of AttendanceReportOutputModel</returns>
        [ProducesResponseType(typeof(List<AttendanceReportOutputModel>), StatusCodes.Status200OK)]
        // https://localhost:44365/api/group/3/percent-of-skip/0
        [HttpGet("{groupId}/percent-of-skip/{percent}")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult<List<AttendanceReportOutputModel>> GetStudentsByPercentOfSkip(int groupId, int percent)
        {
            return Ok(_mapper.Map<List<AttendanceReportOutputModel>>(_lessonService.GetStudentByPercentOfSkip(percent, groupId)));
        }
    }
    
}





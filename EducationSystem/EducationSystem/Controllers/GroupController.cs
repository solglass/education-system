﻿using EducationSystem.Data;
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
        // https://localhost:44365/api/group/
        [HttpGet]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<List<GroupOutputModel>> GetGroups()
        {
            var groups = _service.GetGroups();
            var result = _mapper.Map<List<GroupOutputModel>>(groups);

            return Ok(result);
        }
        // https://localhost:44365/api/group/3
        [HttpGet("{id}")]
        public ActionResult<GroupOutputModel> GetGroupById(int id)
        {
            var group = _service.GetGroupById(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(group);
            return Ok(result);
        }
        // https://localhost:44365/api/group/3
        [HttpGet("by-theme/{Id}")]
        public ActionResult<List<GroupOutputModel>> GetGroupByThemeId(int id)
        {
            var group = _service.GetGroupByThemeId(id);
            List<GroupOutputModel> result = _mapper.Map<List<GroupOutputModel>>(group);
            return Ok(result);
        }
        // https://localhost:44365/api/group/without-tutors
        [HttpGet("without-tutors")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroupsWithoutTutors()
        {
            var result = _mapper.Map<List<GroupOutputModel>>(_service.GetGroupsWithoutTutors());
            return Ok(result);
        }
        // https://localhost:44365/api/group/2/programs
        [HttpGet("{Id}/programs")]        
        public ActionResult<GroupOutputModel> GetGroupProgramsByGroupId(int id)
        {
            var program = _service.GetGroupProgramsByGroupId(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(program);
            return Ok(result);
        }
        // https://localhost:44365/api/group/
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
        // https://localhost:44365/api/group/2
        [HttpPut("{id}")]
        //[Authorize(Roles = "Админ, Менеджер")]
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
        // https://localhost:44365/api/group/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteGroup(int id)
        {
            _service.DeleteGroup(id);
            return NoContent();
        }

        [HttpPost("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            _service.AddGroup_Material(groupId, materialId);
            return StatusCode(StatusCodes.Status201Created);
        }

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

        [HttpDelete("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteTeacherGroup(int groupId, int userId)
        {
            _service.DeleteTeacherGroup(userId, groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTeacherGroup(int groupId, int userId)
        {
            _service.AddTeacherGroup(groupId, userId);
            return StatusCode(StatusCodes.Status201Created);
        }      


        [HttpDelete("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteStudentGroup(int groupId, int userId)
        {
            _service.DeleteStudentGroup(userId, groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddStudentGroup(int groupId, int userId, StudentGroupOutputModel studentGroupInputModel)
        {
            var studentGroupDto = _mapper.Map<StudentGroupDto>(studentGroupInputModel);
            var id = _service.AddStudentGroup(groupId, userId, studentGroupDto);
            var outputModel = _mapper.Map<StudentGroupOutputModel>(_service.GetStudentGroupById(id));
            return Ok(outputModel);
        }

        [HttpDelete("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult DeleteTutorGroups(int groupId, int userId)
        {
            _service.DeleteTutorGroup(userId, groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult AddTutorToGroup(int groupId, int userId)
        {
            _service.AddTutorToGroup(userId, groupId);
            return StatusCode(StatusCodes.Status201Created);
        }

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

        [HttpGet("uncovered-themes")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<List<ThemeOutputModel>> GetUncoveredThemesByGroupId(int id)
        {
            var result = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetUncoveredThemesByGroupId(id));
            return Ok(result);
        }

        // https://localhost:44365/api/group/3/percent-of-skip/0
        [HttpGet("{groupId}/percent-of-skip/{percent}")]
        [AllowAnonymous]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetStudentsByPercentOfSkip(int groupId, int percent)
        {
            return Ok(_mapper.Map<List<AttendanceReportOutputModel>>(_lessonService.GetStudentByPercentOfSkip(percent, groupId)));
        }
    }
    
}





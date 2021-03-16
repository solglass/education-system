﻿
using EducationSystem.API.Models;
using EducationSystem.API.Mappers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private IGroupRepository _repo;
        private IGroupService _service;
        private ICourseService _courseService;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupRepository groupRepository, IGroupService groupService, ICourseService courseService)
        {
            
            _repo = groupRepository;
            _service = groupService;
            _courseService = courseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the groups with their courses and groupStatuses
        /// </summary>
        /// <returns>Returns the list of GroupOutputModels</returns>
        // https://localhost:44365/api/group/
        [ProducesResponseType(typeof(List<GroupOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "Админ, Менеджер")]
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
          var changedRows = _service.UpdateGroup(groupDto);
          var result = _mapper.Map<GroupOutputModel>(_service.GetGroupById(id));
          return Ok(result);
        }
        // https://localhost:44365/api/group/2
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteGroup(int id)
        {
            var deleteRows = _service.DeleteGroup(id);
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
            var deletedGroup = _repo.DeleteTeacherGroup(userId, groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTeacherGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddTeacherGroup(new TeacherGroupDto { GroupID = groupId, UserID = userId});
            return StatusCode(StatusCodes.Status201Created);
        }      


        [HttpDelete("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteStudentGroup(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteStudentGroupById(userId, groupId);
            return NoContent();
        }

        [HttpPost("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddStudentGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddStudentGroup(new StudentGroupDto { GroupID = groupId, UserID = userId });
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult DeleteTutorGroupsByIds(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteTutorGroupsByIds(userId, groupId);
            return NoContent();
    }

        [HttpPost("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult AddTutorToGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddTutorToGroup(new TutorGroupDto { GroupID = groupId, UserID = userId});
            return StatusCode(StatusCodes.Status201Created);
    }

        [HttpGet("report")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор, Методист")]
        public ActionResult<List<GroupReportOutputModel>> GetReport()
        {
            List<GroupReportOutputModel> report ;

            try
            {
                var reportDto = _repo.GenerateReport();
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
    }
    
}





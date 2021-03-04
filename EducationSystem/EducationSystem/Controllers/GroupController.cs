
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

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/group/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class GroupController : ControllerBase
    {

        private IGroupRepository _repo;
        private GroupMapper _groupMapper;
        private IGroupService _service;
        private ICourseService _courseService;
        private GroupReportMapper _reportMapper;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupRepository groupRepository, IGroupService groupService, ICourseService courseService)
        {
            
            _repo = groupRepository;
            _groupMapper = new GroupMapper();
            _service = groupService;
            _courseService = courseService;
            _reportMapper = new GroupReportMapper();
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Админ, Менеджер")]
        public ActionResult GetGroups()
        {
            List<GroupOutputModel> result = _groupMapper.FromDtos(_service.GetGroups());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            GroupOutputModel result = _groupMapper.FromDto(_service.GetGroupById(id));
            return Ok(result);
        }

        [HttpGet("by-theme/{Id}")]
        public ActionResult<List<GroupOutputModel>> GetGroupByThemeId(int id)
        {
            List<GroupOutputModel> result = _groupMapper.FromDtos(_service.GetGroupByThemeId(id));
            return Ok(result);
        }

        [HttpGet("without-tutors")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<GroupOutputModel>> GetGroupsWithoutTutors()
        {
            var result = _mapper.Map<List<GroupOutputModel>>(_service.GetGroupsWithoutTutors());
            return Ok(result);
        }

        [HttpGet("{Id}/programs")]        
        public ActionResult GetGroupProgramsByGroupId(int id)
        {
            GroupOutputModel result = _groupMapper.FromDto(_service.GetGroupProgramsByGroupId(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddNewGroup([FromBody] GroupInputModel group)
        {
            var groupDto = _groupMapper.ToDto(group);
            var result = _service.AddGroup(groupDto);
            return Ok($"Группа #{result} добавлена");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult UpdateGroupInfo(int id, [FromBody] GroupInputModel group)
        {
            if (_service.GetGroupById(id) == null)
            {
                return Ok("Ошибка! Отсутствует группа с введенным id!");
            }
            var groupDto = _groupMapper.ToDto(group);
            _service.UpdateGroup(groupDto);
            return Ok("Изменения внесены!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteGroup(int id)
        {
            _service.DeleteGroup(id);
            return Ok("Группа удалена");
        }

        [HttpPost("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            _service.AddGroup_Material(groupId, materialId);
            return Ok("Добавлено");
        }

        [HttpDelete("{groupId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteGroup_Material(int groupId, int materialId)
        {
            var result = _service.DeleteGroup_Material(groupId, materialId);
            if (result > 0)
                return Ok("Удалено");
            else
                return Problem("Ошибка!");
        }

        [HttpDelete("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteTeacherGroup(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteTeacherGroup(userId, groupId);
            return Ok(deletedGroup);
        }

        [HttpPost("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTeacherGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddTeacherGroup(new TeacherGroupDto { GroupID = groupId, UserID = userId});
            return Ok(addGroup);
        }      


        [HttpDelete("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteStudentGroup(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteStudentGroupById(userId, groupId);
            return Ok(deletedGroup);
        }

        [HttpPost("{groupId}/student/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddStudentGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddStudentGroup(new StudentGroupDto { GroupID = groupId, UserID = userId });
            return Ok(addGroup);
        }

        [HttpGet("by-tutor/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Тьютор")]
        public ActionResult GetTutorGroupById(int id)
        {
            var group = _repo.GetTutorGroupById(id);
            return Ok(group);
        }


        [HttpDelete("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult DeleteTutorGroupsByIds(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteTutorGroupsByIds(userId, groupId);
            return Ok(deletedGroup);
        }

        [HttpPost("{groupId}/tutor/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult AddTutorToGroup(int groupId, int userId)
        {
            var addGroup = _repo.AddTutorToGroup(new TutorGroupDto { GroupID = groupId, UserID = userId});
            return Ok(addGroup);
        }

        [HttpGet("report")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор, Методист")]
        public ActionResult GetReport()
        {
            List<GroupReportOutputModel> report ;

            try
            {
                report = _reportMapper.FromDtos(_repo.GenerateReport());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(report);
        }

        [HttpGet("uncovered-themes")]
       // [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult GetUncoveredThemesByGroupId(int id)
        {
            var result = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetUncoveredThemesByGroupId(id));
                 return Ok(result);
        }
    }
    
}





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

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/group/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private IGroupRepository _repo;
        private IGroupService _service;
        private ICourseService _courseService;
        private ILessonService _lessonService;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupRepository groupRepository, IGroupService groupService, 
            ICourseService courseService, ILessonService lessonService)
        {
            _repo = groupRepository;
            _service = groupService;
            _courseService = courseService;
            _lessonService = lessonService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult GetGroups()
        {
            var groups = _service.GetGroups();
            List<GroupOutputModel> result = _mapper.Map<List<GroupOutputModel>>(groups);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            var group = _service.GetGroupById(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(group);
            return Ok(result);
        }

        [HttpGet("by-theme/{Id}")]
        public ActionResult<List<GroupOutputModel>> GetGroupByThemeId(int id)
        {
            var group = _service.GetGroupByThemeId(id);
            List<GroupOutputModel> result = _mapper.Map<List<GroupOutputModel>>(group);
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
            var program = _service.GetGroupProgramsByGroupId(id);
            GroupOutputModel result = _mapper.Map<GroupOutputModel>(program);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddNewGroup([FromBody] GroupInputModel group)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var groupDto = _mapper.Map<GroupDto>(group);
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
            var groupDto = _mapper.Map<GroupDto>(group);
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
            var addGroup = _repo.AddTeacherGroup(groupId,userId);
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
        public ActionResult AddStudentGroup(StudentGroupInputModel studentGroupInputModel)
        {
            var studentGroupDto=_mapper.Map<StudentGroupDto>(studentGroupInputModel);
            var addGroup = _repo.AddStudentGroup(new StudentGroupDto { User=studentGroupDto.User, Group=studentGroupDto.Group });
            return Ok(addGroup);
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
            var addGroup = _repo.AddTutorToGroup(groupId,userId);
            return Ok(addGroup);
        }

        [HttpGet("report")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор, Методист")]
        public ActionResult GetReport()
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
        public ActionResult GetUncoveredThemesByGroupId(int id)
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





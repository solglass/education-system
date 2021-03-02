
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
    [Authorize]
    public class GroupController : ControllerBase
    {

        private IGroupRepository _repo;
        private GroupMapper _groupMapper;
        private IGroupService _service;
        private ThemeMapper _themeMapper;
        private ICourseService _courseService;
        private GroupReportMapper _reportMapper;
        private IMapper _mapper;
        public GroupController(IMapper mapper, IGroupRepository groupRepository, IGroupService groupService, ICourseService courseService)
        {
            
            _repo = groupRepository;
            _groupMapper = new GroupMapper();
            _service = groupService;
            _courseService = courseService;
            _themeMapper = new ThemeMapper();
            _reportMapper = new GroupReportMapper();
            _mapper = mapper;
        }

        // https://localhost:44365/api/group/
        [HttpGet]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult GetGroups()
        {
            List<GroupOutputModel> result = _groupMapper.FromDtos(_service.GetGroups());
            return Ok(result);
        }

        // https://localhost:44365/api/group/3
        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            GroupOutputModel result = _groupMapper.FromDto(_service.GetGroupById(id));
            return Ok(result);
        }
        // https://localhost:44365/api/group/by-theme/3
        [HttpGet("by-theme//{Id}")]
        public ActionResult<List<GroupOutputModel>> GetGroupByThemeId(int id)
        {
            List<GroupOutputModel> result = _groupMapper.FromDtos(_service.GetGroupByThemeId(id));
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

        // https://localhost:44365/api/group/3/programs
        [HttpGet("{Id}/programs")]        
        public ActionResult GetGroupProgramsByGroupId(int id)
        {
            GroupOutputModel result = _groupMapper.FromDto(_service.GetGroupProgramsByGroupId(id));
            return Ok(result);
        }

        // https://localhost:44365/api/group
        [HttpPost]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddNewGroup([FromBody] GroupInputModel newGroup)
        {
            var groupDto = _groupMapper.ToDto(newGroup);
            var result = _service.AddGroup(groupDto);
            return Ok($"Группа #{result} добавлена");
        }

        // https://localhost:44365/api/group/3
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

        // https://localhost:44365/api/group/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteGroup(int id)
        {
            _service.DeleteGroup(id);
            return Ok("Группа удалена");
        }

        // https://localhost:44365/api/group/group-material/
        [HttpPost()]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            _service.AddGroup_Material(groupId, materialId);
            return Ok("Добавлено");
        }

        // https://localhost:44365/api/group/3/material/8
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

        // https://localhost:50221/api/group/teacher-group/1
        [HttpGet("teacher-group/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель")]
        public ActionResult GetTeacherGroupById(int id)
        {
            var group = _repo.GetTeacherGroupById(id);
            return Ok(group);
        }
        // https://localhost:50221/api/group/2/teacher/3
        [HttpDelete("{groupId}/teacher/{userId}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteTeacherGroup(int groupId, int userId)
        {
            var deletedGroup = _repo.DeleteTeacherGroup(userId, groupId);
            return Ok(deletedGroup);
        }
        // https://localhost:50221/api/group/teacher-group
        [HttpPost("teacher-group")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddTeacherGroup(TeacherGroupDto teacherGroup)
        {
            var addGroup = _repo.AddTeacherGroup(teacherGroup);
            return Ok(addGroup);
        }      
        [HttpGet("student-group/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetStudentGroupById(int id)
        {
            var group = _repo.GetStudentGroupById(id);
            return Ok(group);
        }
        // https://localhost:50221/api/group/student-group/1
        [HttpDelete("student-group{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteStudentGroup(int userId, int groupId)
        {
            var deletedGroup = _repo.DeleteStudentGroupById(userId, groupId);
            return Ok(deletedGroup);
        }
        // https://localhost:50221/api/group/student-group
        [HttpPost("student-group")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult AddStudentGroup(StudentGroupDto studentGroup)
        {
            var addGroup = _repo.AddStudentGroup(studentGroup);
            return Ok(addGroup);
        }
        // 2DO: Change stored procedures
        // it must take id of user
        // https://localhost:50221/api/group/tutor-groups
        [HttpGet("tutor-groups")]
        [Authorize(Roles = "Админ, Менеджер, Тьютор")]
        public ActionResult GetTutorGroups()
        {
            var groups = _repo.GetTutorGroups();
            return Ok(groups);
        }
        // https://localhost:50221/api/group/tutor-groups/1
        [HttpGet("tutor-groups/{id}")]
        [Authorize(Roles = "Админ, Менеджер, Тьютор")]
        public ActionResult GetTutorGroupById(int id)
        {
            var group = _repo.GetTutorGroupById(id);
            return Ok(group);
        }
        // https://localhost:50221/api/group/tutor-groups/1
        [HttpDelete("tutor-groups/{userId}/{groupId}")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult DeleteTutorGroupsByIds(int userId, int groupId)
        {
            var deletedGroup = _repo.DeleteTutorGroupsByIds(userId, groupId);
            return Ok(deletedGroup);
        }
        // https://localhost:50221/api/group/tutor-groups
        [HttpPost("tutor-groups")]
        [Authorize(Roles = "Админ, Менеджер")]

        public ActionResult AddTutorToGroup(TutorGroupDto tutorGroup)
        {
            var addGroup = _repo.AddTutorToGroup(tutorGroup);
            return Ok(addGroup);
        }

        // https://localhost:44365/api/group/report
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
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult GetUncoveredThemesByGroupId(int id)
        {
            var result = _themeMapper.FromDtos(_courseService.GetUncoveredThemesByGroupId(id));
                 return Ok(result);
        }
    }
    
}





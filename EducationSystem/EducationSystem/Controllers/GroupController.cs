
using EducationSystem.API.Models;
using EducationSystem.API.Mappers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Models.InputModels;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/group/
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private GroupRepository _repo;
        private GroupMapper _groupMapper;

        public GroupController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new GroupRepository();
            _groupMapper = new GroupMapper();
        }

        // https://localhost:44365/api/group/
        [HttpGet]
        public ActionResult GetGroups()
        {
            List<GroupOutputModel> result = _groupMapper.FromDtos(_repo.GetGroups());
            return Ok(result);
        }

        // https://localhost:44365/api/group/3
        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            GroupOutputModel result = _groupMapper.FromDto(_repo.GetGroupById(id));
            return Ok(result);
        }

        // https://localhost:44365/api/group
        [HttpPost]
        public ActionResult AddNewGroup([FromBody] GroupInputModel newGroup)
        {
            var groupDto = _groupMapper.ToDto(newGroup);
            var result = _repo.AddGroup(groupDto);
            return Ok($"Группа #{result} добавлена");
        }

        // https://localhost:44365/api/group/3
        [HttpPut("{id}")]
        public ActionResult UpdateGroupInfo(int id, [FromBody] GroupInputModel group)
        {
            if (_repo.GetGroupById(id) == null)
            {
                return Ok("Ошибка! Отсутствует группа с введенным id!");
            }
            var groupDto = _groupMapper.ToDto(group);
            _repo.UpdateGroup(groupDto);
            return Ok("Изменения внесены!");
        }

        // https://localhost:44365/api/group/3
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(int id)
        {
            _repo.DeleteGroup(id);
            return Ok("Группа удалена");
        }

        // https://localhost:44365/api/group/group-material/
        [HttpPost()]
        public ActionResult AddGroup_Material(int groupId, int materialId)
        {
            _repo.AddGroup_Material(groupId, materialId);
            return Ok("Добавлено");
        }

        // https://localhost:44365/api/group/3/material/8
        [HttpDelete("{groupId}/material/{materialId}")]    
        public ActionResult DeleteGroup_Material(int groupId, int materialId)
        {
            var result = _repo.DeleteGroup_Material(groupId, materialId);
            if (result > 0)
                return Ok("Удалено");
            else
                return Problem("Ошибка!");
        }

        // https://localhost:44365/api/group/group-status/name
        [HttpPost("group-status/name")]
        public ActionResult AddGroupStatus(string name)
        {
            _repo.AddGroupStatus(name);
            return Ok("Cтатус обновлен");
        }

        // https://localhost:44365/api/group/group-status
        [HttpGet("group-status")]
        public ActionResult GetGroupStatus()
        {
            var groupStatuses = _repo.GetGroupStatus();
            return Ok(groupStatuses);
        }

        // https://localhost:44365/api/group/group-status/3
        [HttpGet("group-status/{id}")]
        public dynamic GetGroupStatus(int id)
        {
            var groupStatus = _repo.GetGroupStatusById(id);
            return Ok(groupStatus);
        }

        // https://localhost:44365/api/group/group-status/3
        [HttpPut("group-status/{id}")]
        public ActionResult UpdateGroupStatus(int id, [FromBody] GroupStatusDto groupStatus)
        {
            _repo.UpdateGroupStatus(groupStatus);
            return Ok("success");
        }

        // https://localhost:50221/api/group/group-status/3
        [HttpDelete("group-status/{id}")]
        public ActionResult DeleteGroupStatus(int id)
        {
            _repo.DeleteGroupStatus(id);
            return Ok("success");
        }
        // https://localhost:50221/api/group/teacher-groups
        [HttpGet("teacher-groups")]
        public ActionResult GetTeacherGroups()
        {
            var groups = _repo.GetTeacherGroups();
            return Ok(groups);
        }
        // https://localhost:50221/api/group/teacher-group/1
        [HttpGet("teacher-group/{id}")]
        public ActionResult GetTeacherGroupById(int id)
        {
            var group = _repo.GetTeacherGroupById(id);
            return Ok(group);
        }
        // https://localhost:50221/api/group/teacher-group/2
        [HttpDelete("teacher-group/{id}")]
        public ActionResult DeleteTeacherGroup(int id)
        {
            var deletedGroup = _repo.DeleteTeacherGroup(id);
            return Ok(deletedGroup);
        }
        // https://localhost:50221/api/group/teacher-group
        [HttpPost("teacher-group")]
        public ActionResult AddTeacherGroup(TeacherGroupDto teacherGroup)
        {
            var addGroup = _repo.AddTeacherGroup(teacherGroup);
            return Ok(addGroup);
        }
        // https://localhost:50221/api/group/student-groups
        [HttpGet("student-groups")]
        public ActionResult GetStudentGroups()
        {
            var groups = _repo.GetStudentGroups();
            return Ok(groups);
        }
        // https://localhost:50221/api/group/student-group/1
        [HttpGet("student-group/{id}")]
        public ActionResult GetStudentGroupById(int id)
        {
            var group = _repo.GetStudentGroupById(id);
            return Ok(group);
        }
        // https://localhost:50221/api/group/student-group/1
        [HttpDelete("student-group{id}")]
        public ActionResult DeleteStudentGroup(int id)
        {
            var deletedGroup = _repo.DeleteStudentGroupById(id);
            return Ok(deletedGroup);
        }
        // https://localhost:50221/api/group/student-group
        [HttpPost("student-group")]
        public ActionResult AddStudentGroup(StudentGroupDto studentGroup)
        {
            var addGroup = _repo.AddStudentGroup(studentGroup);
            return Ok(addGroup);
        }
    }
    
}





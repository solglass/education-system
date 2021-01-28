using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Models;
using EducationSystem.API.Mappers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;

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

        // https://localhost:50221/api/group/
        [HttpGet]
        public ActionResult GetGroups()
        {
            var result = _repo.GetGroups();
            return Ok(result);
        }

        // https://localhost:50221/api/group/3
        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            var result = _repo.GetGroupById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/group
        [HttpPost]
        public ActionResult AddNewGroup([FromBody] GroupInputModel newGroup)
        {
            var groupDto = _groupMapper.ToDto(newGroup);
            var result = _repo.AddGroup(groupDto);
            return Ok($"Группа #{result} добавлена");
        }

         // https://localhost:50221/api/group/3
         [HttpPut("{id}")]
        public ActionResult UpdateGroupInfo(int id, [FromBody] GroupInputModel group)
        {
            // check if entity exists and then...
            var groupDto = _groupMapper.ToDto(group);
            _repo.UpdateGroup(groupDto);             
            return Ok("success");
        }

        // https://localhost:50221/api/group/3
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(int id)
        {
            _repo.DeleteGroup(id);
            return Ok("success");
        }

        // https://localhost:50221/api/group/group-material/new-group-material
        [HttpPost("new-group-material")]
        public ActionResult AddGroup_Material(int GroupID, int MaterialID)
        {
            _repo.AddGroup_Material(GroupID, MaterialID);
            return Ok("success");
        }

        // https://localhost:50221/api/group/group-material/3
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup_Material(int id)
        {
            _repo.DeleteGroup_Material(id);
            return Ok("success");
        }

        // https://localhost:50221/api/group/group-status/name
        [HttpPost("group-status/name")]
        public ActionResult AddGroupStatus(string name)
        {
            _repo.AddGroupStatus(name);
            return Ok("Cтатус обновлен");
        }

        // https://localhost:50221/api/group/group-status
        [HttpGet("group-status}")]
        public ActionResult GetGroupStatus()
        {
            var groupStatuses = _repo.GetGroupStatus();
            return Ok(groupStatuses);
        }

        // https://localhost:50221/api/group/group-status/3
        [HttpGet("group-status/{id}")]
        public dynamic GetGroupStatus(int id)
        {
            var groupStatus = _repo.GetGroupStatusById(id);
            return Ok(groupStatus);
        }

        // https://localhost:50221/api/group/group-status/3
        [HttpPut("group-status/{id}")]
        public ActionResult UpdateGroupStatus(int id, [FromBody] GroupStatusDto groupStatus)
        {
            _repo.UpdateGroupStatus(groupStatus);
            return Ok("success");
        }

        // https://localhost:50221/api/group/group-status/3
        [HttpDelete("group - status /{id}")]
        public ActionResult DeleteGroupStatus(int id)
        {
            _repo.DeleteGroupStatus(id);
            return Ok("success");
        }





    }
    
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/group/
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private GroupRepository _repo;

        public GroupController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new GroupRepository();
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
        public dynamic GetGroupById(int id)
        {
            var result = _repo.GetGroupById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/group/new-group
        [HttpPost("new-group")]
        public ActionResult AddNewGroup(GroupDto newGroup)
        {
            _repo.AddGroup(newGroup);
            return Ok("Группа добавлена");
        }

         // https://localhost:50221/api/group/3
         [HttpPut("{id}")]
         public ActionResult UpdateGroupInfo(GroupDto group)
         {
             _repo.UpdateGroup(group);
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
    }
}





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
    // https://localhost:50221/api/groupStatus/
    [ApiController]
    [Route("api/[controller]")]
    public class GroupStatusController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private GroupStatusRepository _repo;

        public GroupStatusController(ILogger<WeatherForecastController> logger)
        {
            _logger =logger;
            _repo = new GroupStatusRepository();
        }
       
        // https://localhost:50221/api/groupStatus/register*/
        [HttpPost("register")]
        public ActionResult Register(string Name,[FromBody] dynamic groupStatus)
        {
            _repo.AddGroupStatus(Name);
            return Ok("Cтатус обновлен");
        }

        /* 
         [HttpPut("change-password")]
         public ActionResult ChangePassword(string oldPassword, string newPassword)
         {
             // _repo.ChangePassword(oldPassword, newPassword)
             return Ok("success");
         }*/

        // https://localhost:50221/api/user
        [HttpGet]
        public ActionResult GetGroupStatus()
        {
            var payments = _repo.GetGroupStatus();
            return Ok(payments);
        }
        
        // https://localhost:50221/api/groupstatus/42
        [HttpGet("{id}")]
        public dynamic GetGroupStatus(int id)
        {
            var groupStatus = _repo.GetGroupStatusById(id);
            return Ok(groupStatus);
        }

        // https://localhost:50221/api/groupstatus/42
        [HttpPut("{id}")]
        public ActionResult UpdateGroupStatus(int id, string Name, [FromBody] dynamic data)
        {
            _repo.UpdateGroupStatus(id,Name);
            return Ok("success");
        }

        // https://localhost:50221/api/groupstatus/42
        [HttpDelete("{id}")]
        public ActionResult DeleteGroupStatus(int id)
        {
            _repo.DeleteGroupStatus(id);
            return Ok("success");
        }
    }
}

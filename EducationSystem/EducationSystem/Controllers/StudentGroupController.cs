using EducationSystem.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private StudentGroupRepository _repo;
        public StudentGroupController()
        {
            _repo = new StudentGroupRepository();        
        }

        [HttpGet]
        public ActionResult GetStudentGroups()
        {
            var groups = _repo.GetStudentGroups();
            return Ok(groups);
        }
    }
}

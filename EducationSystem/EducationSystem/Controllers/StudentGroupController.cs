using EducationSystem.Data;
using EducationSystem.Data.Models;
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

        [HttpGet("{id}")]
        public ActionResult GetStudentGroupById(int id)
        {
            var group = _repo.GetStudentGroupById(id);
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudentGroup(int id)
        {
            var deletedGroup = _repo.DeleteStudentGroupById(id);
            return Ok(deletedGroup);
        }
        [HttpPost]
        public ActionResult AddStudentGroup(StudentGroupDto studentGroup)
        {
            var addGroup = _repo.AddStudentGroup(studentGroup);
            return Ok(addGroup);
        }
    }
}

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
    public class TeacherGroupController : ControllerBase
    {
        private TeacherGroupRepository _repo;
        public TeacherGroupController()
        {
            _repo = new TeacherGroupRepository();        
        }

        [HttpGet]
        public ActionResult GetTeacherGroups()
        {
            var groups = _repo.GetTeacherGroups();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public ActionResult GetTeacherGroupById(int id)
        {
            var group = _repo.GetTeacherGroupById(id);
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTeacherGroup(int id)
        {
            var deletedGroup = _repo.DeleteTeacherGroup(id);
            return Ok(deletedGroup);
        }
        [HttpPost]
        public ActionResult AddStudentGroup(TeacherGroupDto teacherGroup)
        {
            var addGroup = _repo.AddTeacherGroup(teacherGroup);
            return Ok(addGroup);
        }
    }
}

using EducationSystem.Controllers;
using EducationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    // https://localhost:50221/api/user/
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private CourseRepository _repo;

        public CourseController()
        {
            _repo = new CourseRepository();
        }

        [HttpGet("Course")]
        public ActionResult GetCourse()
        {
            var courses = _repo.GetCourses();
            return Ok(courses);
        }

       

    }
}

using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
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

        [HttpGet]
        public ActionResult GetCourses()
        {
            var courses = _repo.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public ActionResult GetCourse(int id)
        {
            var course = _repo.GetCourseById(id);
            return Ok(course);
        }

        [HttpPost("create-course")]
        public ActionResult CreateCourse(CourseDto course)
        {
            var result = _repo.AddCourse(course);
            return Ok($"Курс №{result} добавлен!");
        }

        [HttpPut("update-course-info")]
        public ActionResult UpdateCourseInfo(CourseDto course)
        {
            var result = _repo.AddCourse(course);
            return Ok("Курс обновлен!");
        }

        [HttpDelete("remove-course")]
        public ActionResult RemoveCourseInfo(int id)
        {
            var result = _repo.DeleteCourse(id);
            return Ok("Курс удален!");
        }
    }
}

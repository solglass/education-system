using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
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
    // https://localhost:50221/api/course/
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private CourseRepository _repo;
        private CourseMapper _courseMapper;
        public CourseController()
        {
            _repo = new CourseRepository();
            _courseMapper = new CourseMapper();
        }

        [HttpGet]
        public ActionResult GetCourses()            //rewrite using CourseMapper FromDto
        {
            var courses = _repo.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public ActionResult GetCourse(int id)       //rewrite using CourseMapper FromDto
        {
            var course = _repo.GetCourseById(id);
            return Ok(course);
        }

        [HttpPost]
        public ActionResult CreateCourse([FromBody] CourseInputModel course)
        {
            CourseDto courseDto = _courseMapper.ToDto(course);
            var result = _repo.AddCourse(courseDto);
            return Ok($"Курс №{result} добавлен!");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCourseInfo(int id, [FromBody] CourseInputModel course)
        {
            CourseDto courseDto = _courseMapper.ToDto(course);
            var result = _repo.AddCourse(courseDto);
            return Ok("Курс обновлен!");
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveCourseInfo(int id)
        {
            var result = _repo.DeleteCourse(id);
            return Ok("Курс удален!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpPost("{courseId}/theme/{themeId}")]
        public ActionResult AddThemeToCourse(int courseId, int themeId)
        {
            var result = _repo.AddCourse_Theme(courseId, themeId);
            if (result > 0) 
                return Ok(result);
            else 
                return Problem("о-ё-ё-й");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpDelete("{courseId}/theme/{themeId}")]
        public ActionResult RemoveThemeFromCourse(int courseId, int themeId)
        {
            var result = _repo.DeleteCourse_Theme(courseId, themeId);
            if (result > 0)
                return Ok(result);
            else
                return Problem("о-ё-ё-й");
        }
    }
}

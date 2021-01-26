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
    // https://localhost:50221/api/lesson/
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private LessonRepository _repo;

        public LessonController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new LessonRepository();
        }

        // https://localhost:50221/api/lesson/
        [HttpPost]
        public ActionResult AddNewLesson(LessonDto lessonDto)
        {
            _repo.AddLesson(lessonDto);
            return Ok("Add success");
        }

        // https://localhost:50221/api/lesson/
        [HttpGet]
        public ActionResult GetLesson()
        {
            var result = _repo.GetLessons();
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpGet("{id}")]
        public ActionResult GetLessonById(int id)
        {
            var result = _repo.GetLessonById(id);
            return Ok(result);
        }

        // https://localhost:50221/api/lesson/3
        [HttpDelete("{id}")]
        public ActionResult DeleteLesson(int id)
        {
            _repo.DeleteLesson(id);
            return Ok("success");
        }
        // https://localhost:50221/api/lesson/update
        [HttpPost("update")]
        public ActionResult UpdateLesson([FromBody]LessonDto lessonDto)
        {
            _repo.UpdateLesson(lessonDto);
            return Ok("Lesson update");
        }
    }
}

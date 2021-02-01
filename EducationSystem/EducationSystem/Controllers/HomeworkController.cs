using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
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
    // https://localhost:44365/api/homework
    [ApiController]
    [Route("api/[controller]")]
    public class HomeworkController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private HomeworkRepository _repo;
        private HomeworkAttemptMapper _homeworkAttemptMapper;

        public HomeworkController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new HomeworkRepository();
            _homeworkAttemptMapper = new HomeworkAttemptMapper();
        }


        // https://localhost:44365/api/homework
        [HttpPost]
        public ActionResult Register([FromBody] HomeworkDto homework)
        {
            _repo.AddHomework(homework);
            return Ok("success");
        }

        // https://localhost:44365/api/homework
        [HttpGet]
        public ActionResult GetHomeworks()
        {
            var results = _repo.GetHomeworks();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/42
        [HttpGet("{id}")]
        public dynamic GetHomeworkById(int id)
        {
            var results = _repo.GetHomeworkById(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/42
        [HttpPut("{id}")]
        public ActionResult UpdateHomework(int id, [FromBody] HomeworkDto homework)
        {
            _repo.UpdateHomework(homework);
            return Ok("success");
        }

        // https://localhost:44365/api/homework/42
        [HttpDelete("homeworkAttempts/{id}")]
        public ActionResult DeleteHomework(int id)
        {
            _repo.DeleteHomework(id);
            return Ok("success");
        }



        // https://localhost:44365/api/homework/homeworkAttempts
        [HttpPost]
        public ActionResult Create([FromBody] HomeworkAttemptInputModel inputModel)
        {
            HomeworkAttemptDto attempt = _homeworkAttemptMapper.ToDto(inputModel);
            _repo.AddHomeworkAttempt(attempt);
            return Ok("Задание принято на проверку");
        }


        // https://localhost:44365/api/homework/homeworkAttempts
        [HttpGet("homeworkAttempts")]
        public ActionResult GetHomeworkAttempts()
        {
            var results = _repo.GetHomeworkAttempts();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpGet("homeworkAttempts/{id}")]
        public dynamic GetHomeworkAttemptsById(int id)
        {
            var results = _repo.GetHomeworkAttemptById(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpPut("homeworkAttempts/{id}")]
        public ActionResult UpdateHomeworkAttempt(int id, [FromBody] HomeworkAttemptDto homeworkAttempt)
        {
            _repo.UpdateHomeworkAttempt(homeworkAttempt);
            return Ok("success");
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpGet("homeworkAttempts/{id}")]
        public ActionResult DeleteHomeworkAttempt(int id)
        {
            var results = _repo.DeleteHomeworkAttempt(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/comments
        [HttpGet("comments")]
        public ActionResult GetComments()
        {
            var results = _repo.GetComments();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/comments/42
        [HttpGet("comments/{id}")]
        public dynamic GetCommentById(int id)
        {
            var results = _repo.GetCommentById(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homework_Theme/42
        [HttpGet("homeworkTheme/{id}")]
        public dynamic DeleteHomework_Theme(int id)
        {
            var results = _repo.DeleteHomework_Theme(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/HomeworkAttemptStatus
        [HttpGet("HomeworkAttemptStatus")]
        public ActionResult GetHomeworkAttemptStatus()
        {
            var results = _repo.GetHomeworkAttemptStatus();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/HomeworkAttemptStatus/42
        [HttpGet("HomeworkAttemptStatus/{id}")]
        public dynamic DeleteHomeworkAttemptStatus(int id)
        {
            var results = _repo.DeleteHomeworkAttemptStatus(id);
            return Ok(results);
        }
    }
}

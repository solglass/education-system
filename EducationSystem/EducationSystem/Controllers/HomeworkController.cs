using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class HomeworkController : ControllerBase
    {


        private HomeworkRepository _repo;
        private HomeworkMapper _homeworkMapper;
        private HomeworkAttemptMapper _homeworkAttemptMapper;

        public HomeworkController()
        {
            _repo = new HomeworkRepository();
            _homeworkMapper = new HomeworkMapper();
            _homeworkAttemptMapper = new HomeworkAttemptMapper();
        }


        // https://localhost:44365/api/homework
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomework([FromBody] HomeworkDto homework)
        {
            _repo.AddHomework(homework);
            return Ok("Задание добавлено");
        }

        // https://localhost:44365/api/homework
        [HttpGet]
        [Authorize(Roles = "Админ")]
        public ActionResult GetHomeworks()
        {
            var results = _repo.GetHomeworks();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkById(int id)
        {
            var results = _repo.GetHomeworkById(id);
            return Ok(results);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptsByHomeworkId(int id)
        {
            var outputModel = _homeworkAttemptMapper.FromDtos(_repo.GetHomeworkAttemptsByHomeworkId(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/homework/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult UpdateHomework(int id, [FromBody] HomeworkDto homework)
        {
            _repo.UpdateHomework(homework);
            return Ok("success");
        }

        // https://localhost:44365/api/homework/42
        [HttpDelete("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteHomework(int id)
        {
            _repo.DeleteHomework(id);
            return Ok("success");
        }



        // https://localhost:44365/api/homework/homeworkAttempts
        [HttpPost]
        [Authorize(Roles = "Студент")]
        public ActionResult CreateAttempt([FromBody] HomeworkAttemptInputModel inputModel)
        {
            HomeworkAttemptDto attempt = _homeworkAttemptMapper.ToDto(inputModel);
            _repo.AddHomeworkAttempt(attempt);
            return Ok("Задание отправлено на проверку");
        }


        // https://localhost:44365/api/homework/homeworkAttempts
        [HttpGet("homeworkAttempts")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetHomeworkAttempts()
        {
            var results = _repo.GetHomeworkAttempts();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpGet("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptById(int id)
        {
            var results = _repo.GetHomeworkAttemptById(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpPut("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateHomeworkAttempt(int id, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            HomeworkAttemptDto attempt = _homeworkAttemptMapper.ToDto(inputModel);
            _repo.UpdateHomeworkAttempt(attempt);
            return Ok("Изменения сохранены");
        }

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpDelete("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteHomeworkAttempt(int id)
        {
            _repo.DeleteHomeworkAttempt(id);
            return Ok("Задание удалено");
        }

        //https://localhost:44365/api/homework/comment
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult AddComment([FromBody] CommentDto comment)
        {
            _repo.AddComment(comment);
            return Ok("Комментарий добавлен!");
        }

        // https://localhost:44365/api/homework/comments
        [HttpGet("comments")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetComments()
        {
            var results = _repo.GetComments();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/comments/42
        [HttpGet("comments/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetCommentById(int id)
        {
            var results = _repo.GetCommentById(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/homework_Theme/42
        [HttpDelete("homeworkTheme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomework_Theme(int id)
        {
            var results = _repo.DeleteHomework_Theme(id);
            return Ok(results);
        }

        // https://localhost:44365/api/homework/HomeworkAttemptStatus
        [HttpGet("HomeworkAttemptStatus")]
        [Authorize(Roles = "Админ")]
        public ActionResult GetHomeworkAttemptStatuses()
        {
            var results = _repo.GetHomeworkAttemptStatuses();
            return Ok(results);
        }

        // https://localhost:44365/api/homework/HomeworkAttemptStatus/42
        [HttpDelete("HomeworkAttemptStatus/{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteHomeworkAttemptStatus(int id)
        {
            var results = _repo.DeleteHomeworkAttemptStatus(id);
            return Ok(results);
        }
    }

}
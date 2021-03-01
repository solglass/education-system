using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
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


        private IHomeworkRepository _repo;
        private HomeworkMapper _homeworkMapper;
        private HomeworkAttemptMapper _homeworkAttemptMapper;
        private IHomeworkService _homeworkService;
        private IMapper _mapper;

        public HomeworkController(IMapper mapper, IHomeworkRepository homeworkRepository, IHomeworkService homeworkService)
        {
            _repo = homeworkRepository;
            _homeworkMapper = new HomeworkMapper();
            _homeworkAttemptMapper = new HomeworkAttemptMapper();
            _homeworkService = homeworkService;
            _mapper = mapper;
        }

        // https://localhost:44365/api/homework
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomework([FromBody] HomeworkDto homework)
        {
            _repo.AddHomework(homework);
            return Ok("Задание добавлено");
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

        // https://localhost:44365/api/homework/by-group/2
        [HttpGet("by-group/{groupId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByGroupId(int groupId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByGroupId(groupId));
                      
            return Ok(result);
        }

        // https://localhost:44365/api/homework/by-tag/2
        [HttpGet("by-tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByTagId(int tagId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByTagId(tagId));
            
            return Ok(result);
        }

        // https://localhost:44365/api/homework/by-theme/2
        [HttpGet("by-theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByThemeId(int themeId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByThemeId(themeId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult UpdateHomework(int id, [FromBody] HomeworkDto homework)
        {
            _repo.UpdateHomework(homework);
            return Ok("success");
        }

    

        // https://localhost:44365/api/homework/homeworkAttempts
        [HttpPost]
        [Authorize(Roles = "Студент")]
        public ActionResult CreateAttempt([FromBody] HomeworkAttemptInputModel inputModel)
        {
            int result = _homeworkService.AddHomeworkAttempt(_homeworkAttemptMapper.ToDto(inputModel));
            return Ok("Задание отправлено на проверку");
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
            _homeworkService.UpdateHomeworkAttempt(_homeworkAttemptMapper.ToDto(inputModel));
            return Ok("Изменения сохранены");
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

        // https://localhost:44365/api/homework/3/theme/1
        [HttpDelete("homework/{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomework_Theme(int homeworkId, int themeId)
        {
            var results = _homeworkService.DeleteHomework_Theme(homeworkId, themeId);
            return Ok(results);
        }

        
        // https://localhost:44365/api/homeworkAttempt/3/attachment/1
        [HttpDelete("homeworkAttempt/{homeworkAttemptId}/attachment/{attachmentId}")]
        public ActionResult DeleteHomeworkAttemptAttachment(int homeworkAttemptId, int attachmentId)
        {
            var results = _homeworkService.DeleteHomeworkAttemptAttachment(homeworkAttemptId,attachmentId);
            return Ok(results);
        }
       
        // https://localhost:44365/api/homework/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomework(int id)
        {
            var result = _homeworkService.DeleteHomework(id);
            if (result == 1)
                return Ok($"Домашняя работа #{id} удалена!");
            else
                return Problem($"Ошибка! Не удалось удалить домашнюю работу #{id}!");
        }


        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpDelete("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult DeleteHomeworkAttempt(int id)
        {
            var result = _homeworkService.DeleteHomeworkAttempt(id);
            if (result == 1)
                return Ok($"Решение #{id} удалено!");
            else
                return Problem($"Ошибка! Не удалось удалить решение #{id}!");
        }

        // https://localhost:44365/api/homework/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult RecoverHomework(int id)
        {
            var result = _homeworkService.RecoverHomework(id);
            if (result == 1)
                return Ok($"Домашняя работа #{id} восстановлена!");
            else
                return Problem($"Ошибка! Не удалось восстановить домашнюю работу #{id}!");
        }

        // https://localhost:44365/api/homework/homeworkAttempts/id/recovery
        [HttpPut("homeworkAttempts/{id}/recovery")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult RecoverHomeworkAttempt(int id)
        {
            var result = _homeworkService.RecoverHomeworkAttempt(id);
            if (result == 1)
                return Ok($"Решение #{id} восстановлено!");
            else
                return Problem($"Ошибка! Не удалось восстановить решение #{id}!");
        }
      
        // https://localhost:44365/api/comments/id
        [HttpDelete("comments/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteComment(int id)
        {
            var result = _homeworkService.DeleteComment(id);
            if (result == 1)
                return Ok($"Комментарий #{id} удален!");
            else
                return Problem($"Ошибка! Не удалось удалить комментарий #{id}!");
        }

        // https://localhost:44365/api/comments/id/recovery
        [HttpPut("comments/{id}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult RecoverComment(int id)
        {
            var result = _homeworkService.RecoverComment(id);
            if (result == 1)
                return Ok($"Комментарий #{id} восстановлен!");
            else
                return Problem($"Ошибка! Не удалось восстановить комментарий #{id}!");
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpPost("homework/{homeworkId}/theme/{themeId}")]
        public ActionResult AddHomeworkTheme(int homeworkId, int themeId)
        {
            var results = _homeworkService.AddHomework_Theme(homeworkId, themeId); 
            return Ok(results);
        }
        [HttpGet("homework/attempts/by-user/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptsByUserId(int id)
        {
          var outputModel = _homeworkAttemptMapper.FromDtos(_homeworkService.GetHomeworkAttemptsByUserId(id));
          return Ok(outputModel);
        }
        
        [HttpGet("homework/attempts/by-grop/{statusId}/{groupId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptByStatusIdAndGroupId(int statusId, int groupId)
        {
          var outputModel = _homeworkAttemptMapper.FromDtos(_homeworkService.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId));
          return Ok(outputModel);
        }
    }

}
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


        private HomeworkRepository _repo;
        private HomeworkMapper _homeworkMapper;
        private HomeworkAttemptMapper _homeworkAttemptMapper;
        private HomeworkService _homeworkService;
        private IMapper _mapper;

        public HomeworkController(IMapper mapper)
        {
            _repo = new HomeworkRepository();
            _homeworkMapper = new HomeworkMapper();
            _homeworkAttemptMapper = new HomeworkAttemptMapper();
            _homeworkService = new HomeworkService();
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

        // https://localhost:44365/api/homework/group/2
        [HttpGet("group/{groupId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkOutputModel>> GetHomewroksByGroupId(int groupId)
        {
            var result = new List<HomeworkOutputModel>();
            var dtos = _homeworkService.GetHomeworksByGroupId(groupId);
            foreach(var dto in dtos)
            {
                result.Add(_mapper.Map<HomeworkOutputModel>(dto));
            }                
            return Ok(result);
        }

        // https://localhost:44365/api/homework/tag/2
        [HttpGet("tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkOutputModel>> GetHomewroksByTagId(int tagId)
        {
            var result = new List<HomeworkOutputModel>();
            var dtos = _homeworkService.GetHomeworksByTagId(tagId);
            foreach (var dto in dtos)
            {
                result.Add(_mapper.Map<HomeworkOutputModel>(dto));
            }
            return Ok(result);
        }

        // https://localhost:44365/api/homework/theme/2
        [HttpGet("theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkOutputModel>> GetHomewroksByThemeId(int themeId)
        {
            var result = new List<HomeworkOutputModel>();
            var dtos = _homeworkService.GetHomeworksByThemeId(themeId);
            foreach (var dto in dtos)
            {
                result.Add(_mapper.Map<HomeworkOutputModel>(dto));
            }
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

        // https://localhost:44365/api/homework/homeworkAttempts/42
        [HttpDelete("homeworkAttempts/{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteHomeworkAttempt(int id)
        {
            _homeworkService.DeleteHomeworkAttempt(id);
            return Ok("Решение удалено");
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

        // https://localhost:44365/api/homeworkAttempt/3/attachment/1
        [HttpDelete("homeworkAttempt/{homeworkAttemptId}/attachment/{attachmentId}")]
        public ActionResult DeleteHomeworkAttemptAttachment(int homeworkAttemptId, int attachmentId)
        {
            var results = _homeworkService.DeleteHomeworkAttemptAttachment(homeworkAttemptId,attachmentId);
            return Ok(results);
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
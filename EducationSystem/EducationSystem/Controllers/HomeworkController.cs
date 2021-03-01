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
        public ActionResult AddHomework([FromBody] HomeworkInputModel homework)
        {
            var result = _homeworkService.AddHomework(_mapper.Map<HomeworkDto>(homework));
            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkById(int id)
        {
            var results = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(id));
            return Ok(results);
        }

        //todo: doesn't work. Error in GetCommentsByHomeworkAttemptId
        // https://localhost:44365/api/homework/2/attempts
        [HttpGet("{id}/attempts")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptsByHomeworkId(int id)
        {
            var result = _homeworkAttemptMapper.FromDtos(_homeworkService.GetHomeworkAttemptsByHomeworkId(id));
            return Ok(result);
        }

        // https://localhost:44365/api/homework/by-group/2
        [HttpGet("by-group/{groupId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkOutputModel>> GetHomewroksByGroupId(int groupId)
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

        //todo: Automapper doesn't work
        // https://localhost:44365/api/homework/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult UpdateHomework(int id, [FromBody] HomeworkUpdateInputModel homework)
        {
            var dto = _mapper.Map<HomeworkDto>(homework);
            dto.Id = id;
            var result = _homeworkService.UpdateHomework(dto);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult DeleteHomework(int id)
        {
            var result = _homeworkService.DeleteHomework(id);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/2/attempt
        [HttpPost("{homeworkId}/attempt")]
        [Authorize(Roles = "Студент")]
        public ActionResult CreateAttempt(int homeworkId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _homeworkAttemptMapper.ToDto(inputModel);
            dto.Homework = new HomeworkDto { Id = homeworkId };
            var result = _homeworkService.AddHomeworkAttempt(dto);

            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/2
        [HttpGet("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetHomeworkAttemptById(int attemptId)
        {
            var results = _mapper.Map<HomeworkAttemptOutputModel>( _homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(results);
        }


        // https://localhost:44365/api/homework/attempt/2
        [HttpPut("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult UpdateHomeworkAttempt(int attemptId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            dto.Id = attemptId;
            
            return Ok(_homeworkService.UpdateHomeworkAttempt(dto));
        }

        // https://localhost:44365/api/homework/attempt/2
        [HttpDelete("attempt/{attemptId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteHomeworkAttempt(int attemptId)
        {
            var result = _homeworkService.DeleteHomeworkAttempt(attemptId);

            return Ok(result);
        }

        //https://localhost:44365/api/homework/attempt/2/comment
        [HttpPost("attempt/{attemptId}/comment")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult AddComment(int attemptId, [FromBody] CommentInputModel comment)
        {
            var dto = _mapper.Map<CommentDto>(comment);
            dto.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            var result = _homeworkService.AddComment(_mapper.Map<CommentDto>(comment));

            return Ok(result);
        }

        // how check attemptId?
        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpGet("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult GetCommentById(int attemptId, int commentId)
        {
            var results = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(results);
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpDelete("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomework_Theme(int homeworkId, int themeId)
        {
            var results = _homeworkService.DeleteHomework_Theme(homeworkId, themeId);

            return Ok(results);
        }
        
        
        // https://localhost:44365/api/homework/attempt/3/attachment/1
        [HttpDelete("attempt/{attemptId}/attachment/{attachmentId}")]
        public ActionResult DeleteHomeworkAttemptAttachment(int attemptId, int attachmentId)
        {
            var results = _homeworkService.DeleteHomeworkAttemptAttachment(attemptId, attachmentId);

            return Ok(results);
        }
        

        // https://localhost:44365/api/homework/2/recovery
        [HttpPut("{homeworkId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult RecoverHomework(int homeworkId)
        {
            var result = _homeworkService.RecoverHomework(homeworkId);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/3/recovery
        [HttpPut("attempt/{attemptId}/recovery")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult RecoverHomeworkAttempt(int attemptId)
        {
            var result = _homeworkService.RecoverHomeworkAttempt(attemptId);

            return Ok(result);
        }


        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpDelete("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteComment(int attemptId, int commentId)
        {
            var result = _homeworkService.DeleteComment(commentId);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/2/comment/2/recovery
        [HttpPut("attempt/{attemptId}/comment/{commentId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult RecoverComment(int attemptId, int commentId)
        {
            var result = _homeworkService.RecoverComment(commentId);

            return Ok(result);
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
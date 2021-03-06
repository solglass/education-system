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
        private IHomeworkService _homeworkService;
        private IMapper _mapper;

        public HomeworkController(IMapper mapper, IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
            _mapper = mapper;
        }


        // https://localhost:44365/api/homework
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]        
        public ActionResult<int> AddHomework([FromBody] HomeworkInputModel homework)
        {
            var result = _homeworkService.AddHomework(_mapper.Map<HomeworkDto>(homework));
            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkOutputModel> GetHomeworkById(int id)
        {
            var results = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(id));
            return Ok(results);
        }

        //todo: doesn't work. Error in GetCommentsByHomeworkAttemptId
        // https://localhost:44365/api/homework/2/attempts
        [HttpGet("{id}/attempts")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptOutputModel>> GetHomeworkAttemptsByHomeworkId(int id)
        {
            var result = _mapper.Map<List<HomeworkAttemptOutputModel>>(_homeworkService.GetHomeworkAttemptsByHomeworkId(id));
            return Ok(result);
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

        //todo: Automapper doesn't work
        // https://localhost:44365/api/homework/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> UpdateHomework(int id, [FromBody] HomeworkUpdateInputModel homework)
        {
            var dto = _mapper.Map<HomeworkDto>(homework);
            dto.Id = id;
            var result = _homeworkService.UpdateHomework(dto);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<int> DeleteHomework(int id)
        {
            var result = _homeworkService.DeleteHomework(id);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/2/attempt
        [HttpPost("{homeworkId}/attempt")]
        [Authorize(Roles = "Студент")]
        public ActionResult<int> CreateAttempt(int homeworkId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            dto.Homework = new HomeworkDto { Id = homeworkId };
            var result = _homeworkService.AddHomeworkAttempt(dto);

            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/2
        [HttpGet("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> GetHomeworkAttemptById(int attemptId)
        {
            var results = _mapper.Map<HomeworkAttemptOutputModel>( _homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(results);
        }


        // https://localhost:44365/api/homework/attempt/2
        [HttpPut("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<int> UpdateHomeworkAttempt(int attemptId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            dto.Id = attemptId;
            
            return Ok(_homeworkService.UpdateHomeworkAttempt(dto));
        }

        // https://localhost:44365/api/homework/attempt/2
        [HttpDelete("attempt/{attemptId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult<int> DeleteHomeworkAttempt(int attemptId)
        {
            var result = _homeworkService.DeleteHomeworkAttempt(attemptId);

            return Ok(result);
        }

        //https://localhost:44365/api/homework/attempt/2/comment
        [HttpPost("attempt/{attemptId}/comment")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<int> AddComment(int attemptId, [FromBody] CommentInputModel comment)
        {
            var dto = _mapper.Map<CommentDto>(comment);
            dto.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            var result = _homeworkService.AddComment(dto);

            return Ok(result);
        }

        //todo: Update attachments in service
        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpPut("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<int> UpdateComment(int attemptId, int commentId, [FromBody] CommentUpdateInputModel comment)
        {
            var dto = _mapper.Map<CommentDto>(comment);
            dto.Id = commentId;
            dto.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            var result = _homeworkService.UpdateComment(dto);

            return Ok(result);
        }

        // how check attemptId?
        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpGet("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> GetCommentById(int attemptId, int commentId)
        {
            var results = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(results);
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpDelete("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> DeleteHomeworkTheme(int homeworkId, int themeId)
        {
            var results = _homeworkService.DeleteHomework_Theme(homeworkId, themeId);

            return Ok(results);
        }
        
        

        // https://localhost:44365/api/homework/2/recovery
        [HttpPut("{homeworkId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> RecoverHomework(int homeworkId)
        {
            var result = _homeworkService.RecoverHomework(homeworkId);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/3/recovery
        [HttpPut("attempt/{attemptId}/recovery")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<int> RecoverHomeworkAttempt(int attemptId)
        {
            var result = _homeworkService.RecoverHomeworkAttempt(attemptId);

            return Ok(result);
        }


        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpDelete("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> DeleteComment(int attemptId, int commentId)
        {
            var result = _homeworkService.DeleteComment(commentId);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/2/comment/2/recovery
        [HttpPut("attempt/{attemptId}/comment/{commentId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> RecoverComment(int attemptId, int commentId)
        {
            var result = _homeworkService.RecoverComment(commentId);

            return Ok(result);
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpPost("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> AddHomeworkTheme(int homeworkId, int themeId)
        {
            var results = _homeworkService.AddHomework_Theme(homeworkId, themeId); 

            return Ok(results);
        }

        // https://localhost:44365/api/homework/3/tag/1
        [HttpPost("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> AddHomeworkTag(int homeworkId, int tagId)
        {
            var results = _homeworkService.AddHomeworkTag(new HomeworkTagDto() { HomeworkId = homeworkId, TagId = tagId});

            return Ok(results);
        }

        // https://localhost:44365/api/homework/3/tag/1
        [HttpDelete("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<int> DeleteHomeworkTag(int homeworkId, int tagId)
        {
            var results = _homeworkService.DeleteHomeworkTag(homeworkId, tagId);

            return Ok(results);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-user/2
        [HttpGet("attempt/by-user/{userId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptsByUserId(int userId)
        {
          var results = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByUserId(userId));

          return Ok(results);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-group/2/by-status/1
        [HttpGet("attempt/by-group/{groupId}/by-status/{statusId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptByStatusIdAndGroupId(int statusId, int groupId)
        {
          var results = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId));

          return Ok(results);
        }    
    }

}
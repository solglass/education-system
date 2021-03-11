using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<HomeworkOutputModel> AddHomework([FromBody] HomeworkInputModel homework)
        {
            var addedHomeworkId = _homeworkService.AddHomework(_mapper.Map<HomeworkDto>(homework));
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(addedHomeworkId));
            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkOutputModel> GetHomeworkById(int id)
        {
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(id));
            return Ok(result);
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
        public ActionResult<HomeworkOutputModel> UpdateHomework(int id, [FromBody] HomeworkUpdateInputModel homework)
        {
            var dto = _mapper.Map<HomeworkDto>(homework);
            dto.Id = id;
            var changedRows = _homeworkService.UpdateHomework(dto);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(id));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<HomeworkOutputModel> DeleteHomework(int id)
        {
            var deletedRows = _homeworkService.DeleteHomework(id);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(id));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/2/attempt
        [HttpPost("{homeworkId}/attempt")]
        [Authorize(Roles = "Студент")]
        public ActionResult<HomeworkAttemptOutputModel> CreateAttempt(int homeworkId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            dto.Homework = new HomeworkDto { Id = homeworkId };
            var addedAttemptId = _homeworkService.AddHomeworkAttempt(dto);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(addedAttemptId));

            return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/2
        [HttpGet("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> GetHomeworkAttemptById(int attemptId)
        {
            var result = _mapper.Map<HomeworkAttemptOutputModel>( _homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }


        // https://localhost:44365/api/homework/attempt/2
        [HttpPut("attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> UpdateHomeworkAttempt(int attemptId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            dto.Id = attemptId;
            var changedRows = _homeworkService.UpdateHomeworkAttempt(dto);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/2
        [HttpDelete("attempt/{attemptId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult<HomeworkAttemptOutputModel> DeleteHomeworkAttempt(int attemptId)
        {
            var deletedRows = _homeworkService.DeleteHomeworkAttempt(attemptId);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }

        //https://localhost:44365/api/homework/attempt/2/comment
        [HttpPost("attempt/{attemptId}/comment")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> AddComment(int attemptId, [FromBody] CommentInputModel comment)
        {
            var dto = _mapper.Map<CommentDto>(comment);
            dto.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            var addedCommentId = _homeworkService.AddComment(dto);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(addedCommentId));

            return Ok(result);
        }

        //todo: Update attachments in service
        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpPut("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> UpdateComment(int attemptId, int commentId, [FromBody] CommentUpdateInputModel comment)
        {
            var dto = _mapper.Map<CommentDto>(comment);
            dto.Id = commentId;
            dto.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            var changedRows = _homeworkService.UpdateComment(dto);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }

        // how check attemptId?
        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpGet("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> GetCommentById(int attemptId, int commentId)
        {
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpDelete("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTheme(int homeworkId, int themeId)
        {
            _homeworkService.DeleteHomework_Theme(homeworkId, themeId);

            return NoContent();
        }
        
        

        // https://localhost:44365/api/homework/2/recovery
        [HttpPut("{homeworkId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<HomeworkOutputModel> RecoverHomework(int homeworkId)
        {
            var recoveredRows = _homeworkService.RecoverHomework(homeworkId);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(homeworkId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/3/recovery
        [HttpPut("attempt/{attemptId}/recovery")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> RecoverHomeworkAttempt(int attemptId)
        {
            var recoveredRows = _homeworkService.RecoverHomeworkAttempt(attemptId);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }


        //https://localhost:44365/api/homework/attempt/2/comment/2
        [HttpDelete("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> DeleteComment(int attemptId, int commentId)
        {
            var deletedRows = _homeworkService.DeleteComment(commentId);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/attempt/2/comment/2/recovery
        [HttpPut("attempt/{attemptId}/comment/{commentId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> RecoverComment(int attemptId, int commentId)
        {
            var recoveredRows = _homeworkService.RecoverComment(commentId);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }

        // https://localhost:44365/api/homework/3/theme/1
        [HttpPost("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTheme(int homeworkId, int themeId)
        {
            _homeworkService.AddHomework_Theme(homeworkId, themeId); 

            return StatusCode(StatusCodes.Status201Created);
        }

        // https://localhost:44365/api/homework/3/tag/1
        [HttpPost("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTag(int homeworkId, int tagId)
        {
            _homeworkService.AddHomeworkTag(homeworkId, tagId);

            return StatusCode(StatusCodes.Status201Created);
        }

        // https://localhost:44365/api/homework/3/tag/1
        [HttpDelete("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTag(int homeworkId, int tagId)
        {
            _homeworkService.DeleteHomeworkTag(homeworkId, tagId);

            return NoContent();
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-user/2
        [HttpGet("attempt/by-user/{userId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptsByUserId(int userId)
        {
          var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByUserId(userId));

          return Ok(result);
        }

        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-group/2/by-status/1
        [HttpGet("attempt/by-group/{groupId}/by-status/{statusId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptByStatusIdAndGroupId(int statusId, int groupId)
        {
          var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId));

          return Ok(result);
        }    
    }

}
using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Controllers;
using EducationSystem.Core.CustomExceptions;
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

        /// <summary>
        /// Add Homework
        /// </summary>
        /// <param name="homework">Data about the extracted entity</param>
        /// <returns>Returns HomeworkOutPutModel</returns>
        // https://localhost:44365/api/homework
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]        
        public ActionResult<HomeworkOutputModel> AddHomework([FromBody] HomeworkInputModel homework)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var addedHomeworkId = _homeworkService.AddHomework(_mapper.Map<HomeworkDto>(homework));
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(addedHomeworkId));
            return Ok(result);
        }

        /// <summary>
        /// Get one Homework by id
        /// </summary>
        /// <param name="homeworkId">Id Homework</param>
        /// <returns>Return one searchable HomeworkOutputModel </returns>
        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/42
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{homeworkId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkOutputModel> GetHomeworkById(int homeworkId)
        {
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(homeworkId));
            return Ok(result);
        }

        /// <summary>
        /// Get list of HomeworkAttempts
        /// </summary>
        /// <param name="homeworkId">Id Homework for searchable attempts</param>
        /// <returns>Return list HomeworkAttemptOutputModel</returns>
        // https://localhost:44365/api/homework/2/attempts
        [ProducesResponseType(typeof(List<HomeworkAttemptOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("{homeworkId}/attempts")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptOutputModel>> GetHomeworkAttemptsByHomeworkId(int homeworkId)
        {
            var result = _mapper.Map<List<HomeworkAttemptOutputModel>>(_homeworkService.GetHomeworkAttemptsByHomeworkId(homeworkId));
            return Ok(result);
        }
        /// <summary>
        /// Get Homework by Group
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>List HomeworkSearchOutputModel for Group</returns>
        // https://localhost:44365/api/homework/by-group/2
        [ProducesResponseType(typeof(List<HomeworkSearchOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-group/{groupId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByGroupId(int groupId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByGroupId(groupId));
            
            return Ok(result);
        }
        /// <summary>
        /// Get Homeworks by Tag
        /// </summary>
        /// <param name="tagId">Tag Id</param>
        /// <returns>Return list HomeworkSearchOutputModel which has this Tag</returns>
        // https://localhost:44365/api/homework/by-tag/2
        [ProducesResponseType(typeof(List<HomeworkSearchOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByTagId(int tagId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByTagId(tagId));
            
            return Ok(result);
        }
        /// <summary>
        /// Get Homeworks by Theme
        /// </summary>
        /// <param name="themeId">Theme Id</param>
        /// <returns>Return list HomeworkSearchOutputModel which has this Theme</returns>
        // https://localhost:44365/api/homework/by-theme/2
        [ProducesResponseType(typeof(List<HomeworkSearchOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("by-theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomewroksByThemeId(int themeId)
        {
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(_homeworkService.GetHomeworksByThemeId(themeId));

            return Ok(result);
        }
        /// <summary>
        /// Update Homework
        /// </summary>
        /// <param name="homeworkId">Id Homework which we want to update</param>
        /// <param name="homework">Params which we want update</param>
        /// <returns>Returrn updated Homework</returns>
        // https://localhost:44365/api/homework/42
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult UpdateHomework(int homeworkId, [FromBody] HomeworkUpdateInputModel homework)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var dto = _mapper.Map<HomeworkDto>(homework);
            var changedRows = _homeworkService.UpdateHomework(homeworkId, dto);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(homeworkId));

            return Ok(result);
        }
        /// <summary>
        /// Delete homework
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>Return deleted homework</returns>
        // https://localhost:44365/api/homework/42
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [HttpDelete("{homeworkId}")]
        [Authorize(Roles = "Админ, Преподаватель")]
        public ActionResult<HomeworkOutputModel> DeleteHomework(int homeworkId)
        {
            var deletedRows = _homeworkService.DeleteHomework(homeworkId);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(homeworkId));

            return Ok(result);
        }
        /// <summary>
        /// Create Attempt
        /// </summary>
        /// <param name="homeworkId">Homework id</param>
        /// <param name="inputModel">Params for Attempt</param>
        /// <returns>Return added Attempt</returns>
        // https://localhost:44365/api/homework/2/attempt
        [ProducesResponseType(typeof(HomeworkAttemptOutputModel), StatusCodes.Status200OK)]
        [HttpPost("{homeworkId}/attempt")]
        [Authorize(Roles = "Студент")]
        public ActionResult<HomeworkAttemptOutputModel> CreateAttempt(int homeworkId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            var addedAttemptId = _homeworkService.AddHomeworkAttempt(homeworkId, dto);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(addedAttemptId));

            return Ok(result);
        }
        /// <summary>
        /// Get homework attempt
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>
        /// <returns>Return searchable attempt</returns>
        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/2
        [ProducesResponseType(typeof(HomeworkAttemptOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> GetHomeworkAttemptById(int homeworkId, int attemptId)
        {
            var result = _mapper.Map<HomeworkAttemptOutputModel>( _homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }
        /// <summary>
        /// Update HomeworkAttempt
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>
        /// <param name="inputModel">Params for update</param>
        /// <returns>Return updated HomeworkAttempt</returns>
        // https://localhost:44365/api/homework/2/attempt/2
        [ProducesResponseType(typeof(HomeworkAttemptOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> UpdateHomeworkAttempt(int homeworkId, int attemptId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var dto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            var changedRows = _homeworkService.UpdateHomeworkAttempt(attemptId, dto);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));
            return Ok(result);
        }
        /// <summary>
        /// Delete HomeworkAttempt
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>
        /// <returns>Return deleted HomeworkAttempt</returns>
        // https://localhost:44365/api/homework/2/attempt/2
        [ProducesResponseType(typeof(HomeworkAttemptOutputModel), StatusCodes.Status200OK)]
        [HttpDelete("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Админ")]
        public ActionResult<HomeworkAttemptOutputModel> DeleteHomeworkAttempt(int homeworkId, int attemptId)
        {
            var deletedRows = _homeworkService.DeleteHomeworkAttempt(attemptId);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }
        /// <summary>
        /// Add comment
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>
        /// <param name="comment">Comment</param>
        /// <returns>Return added comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [HttpPost("{homeworkId}/attempt/{attemptId}/comment")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> AddComment(int homeworkId, int attemptId, [FromBody] CommentInputModel comment)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var dto = _mapper.Map<CommentDto>(comment);
            var addedCommentId = _homeworkService.AddComment(attemptId, dto);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(addedCommentId));

            return Ok(result);
        }
        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>      
        /// <param name="commentId">Comment Id</param>
        /// <param name="comment">Params for update</param>
        /// <returns>Return updated comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment/2
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> UpdateComment(int homeworkId, int attemptId, int commentId, [FromBody] CommentUpdateInputModel comment)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var dto = _mapper.Map<CommentDto>(comment);
            var changedRows = _homeworkService.UpdateComment(attemptId, commentId, dto);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }
        /// <summary>
        /// Get comment by Id
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>      
        /// <param name="commentId">Comment Id</param>
        /// <returns>Return searchable comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment/2
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [HttpGet("{homeworkId}/attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> GetCommentById(int homeworkId, int attemptId, int commentId)
        {
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }
        /// <summary>
        /// Delete homework theme
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="themeId">Theme Id</param>
        /// <returns>Status code no content</returns>
        // https://localhost:44365/api/homework/3/theme/1
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTheme(int homeworkId, int themeId)
        {
            _homeworkService.DeleteHomework_Theme(homeworkId, themeId);

            return NoContent();
        }
        /// <summary>
        /// Recover homework
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <returns>Return recovered homework</returns>
        // https://localhost:44365/api/homework/2/recovery
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<HomeworkOutputModel> RecoverHomework(int homeworkId)
        {
            var recoveredRows = _homeworkService.RecoverHomework(homeworkId);
            var result = _mapper.Map<HomeworkOutputModel>(_homeworkService.GetHomeworkById(homeworkId));

            return Ok(result);
        }
        /// <summary>
        /// Recover homework attempt
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>
        /// <returns>Return recovered homework attempt</returns>
        // https://localhost:44365/api/homework/2/attempt/3/recovery
        [ProducesResponseType(typeof(HomeworkAttemptOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/recovery")]
        [Authorize(Roles = "Админ, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> RecoverHomeworkAttempt(int homeworkId, int attemptId)
        {
            var recoveredRows = _homeworkService.RecoverHomeworkAttempt(attemptId);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

            return Ok(result);
        }
        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>      
        /// <param name="commentId">Comment Id</param>
        /// <returns>Return deleted comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment/2
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [HttpDelete("{homeworkId}/attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> DeleteComment(int homeworkId, int attemptId, int commentId)
        {
            var deletedRows = _homeworkService.DeleteComment(commentId);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));
            return Ok(result);
        }
        /// <summary>
        /// Recover comment
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="attemptId">Attempt Id</param>      
        /// <param name="commentId">Comment Id</param>
        /// <returns>Return recovered comment</returns>
        // https://localhost:44365/api/homework/2/attempt/2/comment/2/recovery
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/comment/{commentId}/recovery")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> RecoverComment(int homeworkId, int attemptId, int commentId)
        {
            var recoveredRows = _homeworkService.RecoverComment(commentId);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));
            return Ok(result);
        }
        /// <summary>
        /// Add homework theme
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="themeId">Theme Id</param>
        /// <returns>Return status code 201</returns>
        // https://localhost:44365/api/homework/3/theme/1
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTheme(int homeworkId, int themeId)
        {
            _homeworkService.AddHomework_Theme(homeworkId, themeId); 

            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Add homework tag
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="tagId">Tag Id</param>
        /// <returns>Return status code 201</returns>
        // https://localhost:44365/api/homework/3/tag/1
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTag(int homeworkId, int tagId)
        {
            _homeworkService.AddHomeworkTag(homeworkId, tagId);

            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Delete homework tag
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="tagId">Tag Id</param>
        /// <returns>Return no content response</returns>
        // https://localhost:44365/api/homework/3/tag/1
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTag(int homeworkId, int tagId)
        {
            _homeworkService.DeleteHomeworkTag(homeworkId, tagId);

            return NoContent();
        }
        /// <summary> 
        /// Get homeworkAttempt by user Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Return searchable homeworkAttempt</returns>
        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-user/2
        [ProducesResponseType(typeof(List<HomeworkAttemptWithCountOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("attempt/by-user/{userId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptsByUserId(int userId)
        {
          var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByUserId(userId));

          return Ok(result);
        }
        /// <summary>
        /// Get homeworkAttempt by status Id and group Id
        /// </summary>
        /// <param name="statusId"></param>
        /// <param name="groupId"></param>
        /// <returns>Return searchable homeworkAttempt</returns>
        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/attempt/by-group/2/by-status/1
        [ProducesResponseType(typeof(List<HomeworkAttemptWithCountOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("attempt/by-group/{groupId}/by-status/{statusId}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptByStatusIdAndGroupId(int statusId, int groupId)
        {
          var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(_homeworkService.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId));

          return Ok(result);
        }    
    }

}
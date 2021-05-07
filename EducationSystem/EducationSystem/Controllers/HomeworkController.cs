using AutoMapper;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Core.CustomExceptions;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/homework
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HomeworkController : ControllerBase
    {
        private IHomeworkService _homeworkService;
        private IUserService _userService;
        private IGroupService _groupService;
        private ITagService _tagService;
        private ICourseService _courseService;
        private IMapper _mapper;

        public HomeworkController(IMapper mapper,
            IHomeworkService homeworkService,
            IUserService userService,
            IGroupService groupService,
            ITagService tagService,
            ICourseService courseService)
        {
            _homeworkService = homeworkService;
            _userService = userService;
            _groupService = groupService;
            _tagService = tagService;
            _courseService = courseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add Homework
        /// </summary>
        /// <param name="homework">Data about the extracted entity</param>
        /// <returns>Returns HomeworkOutPutModel</returns>
        // https://localhost:44365/api/homework
        [ProducesResponseType(typeof(HomeworkOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Методист")]
        public ActionResult<HomeworkOutputModel> AddHomework([FromBody] HomeworkInputModel homework)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var userGroup = this.SupplyUserGroupsList(_groupService);

            if (!User.IsInRole("Администратор") && !homework.GroupIds.Any(h => userGroup.Contains(h)))
            {
                return Forbid($"User is not in group {homework.GroupIds}");
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{homeworkId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<HomeworkOutputModel> GetHomeworkById(int homeworkId)
        {
            var dto = _homeworkService.GetHomeworkById(homeworkId);
            if (dto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !dto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {dto.Groups.Select(h => h.Id).ToList()}");
            }

            var result = _mapper.Map<HomeworkOutputModel>(dto);
            return Ok(result);
        }

        /// <summary>
        /// Get all Homeworks
        /// </summary>
        /// <returns>Return all HomeworkOutputModels </returns>
        //todo: the model is not filled in.
        // https://localhost:44365/api/homework/42
        [ProducesResponseType(typeof(List<HomeworkOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Authorize(Roles = "Администратор, Методист")]
        public ActionResult<List<HomeworkOutputModel>> GetHomeworks()
        {
            var dtos= _homeworkService.GetHomeworks();
            var result = _mapper.Map<List<HomeworkOutputModel>>(dtos);
            return Ok(result);
        }

        /// <summary>
        /// Get list of HomeworkAttempts
        /// </summary>
        /// <param name="homeworkId">Id Homework for searchable attempts</param>
        /// <returns>Return list HomeworkAttemptOutputModel</returns>
        // https://localhost:44365/api/homework/2/attempts
        [ProducesResponseType(typeof(List<HomeworkAttemptOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{homeworkId}/attempts")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptOutputModel>> GetHomeworkAttemptsByHomeworkId(int homeworkId)
        {
            var dto = _homeworkService.GetHomeworkById(homeworkId);
            if (dto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !dto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {dto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("by-group/{groupId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomeworksByGroupId(int groupId)
        {
            var groupDto = _groupService.GetGroupById(groupId);
            if (groupDto is null)
                return NotFound($"Group with id {groupId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(groupId))
            {
                return Forbid($"User is not in group {groupId}");
            }

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("by-tag/{tagId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomeworksByTagId(int tagId)
        {
            var tagDto = _tagService.GetTagById(tagId);
            if (tagDto is null)
                return NotFound($"Tag with id {tagId} is not found");

            var homeworkDtos = _homeworkService.GetHomeworksByTagId(tagId);
            var availableHomeworks = GetAvailableHomeworks(homeworkDtos);
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(availableHomeworks);

            return Ok(result);
        }

        /// <summary>
        /// Get Homeworks by Theme
        /// </summary>
        /// <param name="themeId">Theme Id</param>
        /// <returns>Return list HomeworkSearchOutputModel which has this Theme</returns>
        // https://localhost:44365/api/homework/by-theme/2
        [ProducesResponseType(typeof(List<HomeworkSearchOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("by-theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkSearchOutputModel>> GetHomeworksByThemeId(int themeId)
        {
            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto is null)
                return NotFound($"Theme with id {themeId} is not found");

            var homeworkDtos = _homeworkService.GetHomeworksByThemeId(themeId);
            var availableHomeworks = GetAvailableHomeworks(homeworkDtos);
            var result = _mapper.Map<List<HomeworkSearchOutputModel>>(availableHomeworks);

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{homeworkId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<HomeworkOutputModel> UpdateHomework(int homeworkId, [FromBody] HomeworkUpdateInputModel homework)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult<HomeworkOutputModel> DeleteHomework(int homeworkId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{homeworkId}/attempt")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> CreateAttempt(int homeworkId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            var attemptDto = _mapper.Map<HomeworkAttemptDto>(inputModel);
            var addedAttemptId = _homeworkService.AddHomeworkAttempt(homeworkId, attemptDto);
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> GetHomeworkAttemptById(int homeworkId, int attemptId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Студент") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> UpdateHomeworkAttempt(int homeworkId, int attemptId, [FromBody] HomeworkAttemptInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}/attempt/{attemptId}")]
        [Authorize(Roles = "Администратор")]
        public ActionResult<HomeworkAttemptOutputModel> DeleteHomeworkAttempt(int homeworkId, int attemptId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{homeworkId}/attempt/{attemptId}/comment")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> AddComment(int homeworkId, int attemptId, [FromBody] CommentInputModel comment)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Студент") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> UpdateComment(int homeworkId, int attemptId, int commentId, [FromBody] CommentUpdateInputModel comment)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var commentDto = _homeworkService.GetCommentById(commentId);
            if (commentDto is null)
                return NotFound($"Comment with id {commentId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Студент") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {!homeworkDto.Groups.Any(h => userGroup.Contains(h.Id))}");
            }

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

            var dto = _mapper.Map<CommentDto>(comment);
            var changedRows = _homeworkService.UpdateComment(attemptId, commentId, dto);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}/attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> DeleteComment(int homeworkId, int attemptId, int commentId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var commentDto = _homeworkService.GetCommentById(commentId);
            if (commentDto is null)
                return NotFound($"Comment with id {commentId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/comment/{commentId}/recovery")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<CommentOutputModel> RecoverComment(int homeworkId, int attemptId, int commentId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var commentDto = _homeworkService.GetCommentById(commentId);
            if (commentDto is null)
                return NotFound($"Comment with id {commentId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            var recoveredRows = _homeworkService.RecoverComment(commentId);
            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));
            return Ok(result);
        }
        /// <summary>
        /// Get comment by HomeworkAttemptId
        /// </summary>
        /// <param name="attemptId">Attempt Id</param>      
        /// <param name="commentId">Comment Id</param>
        /// <returns>Return searchable comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment/2
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("attempt/{attemptId}/comment/{commentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> GetCommentsByHomeworkAttemptId(int attemptId, int commentId)
        {
            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            var homeworkDto = _homeworkService.GetHomeworkById(attemptDto.Homework.Id);

            var commentDto = _homeworkService.GetCommentById(commentId);
            if (commentDto is null)
                return NotFound($"Comment with id {commentId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Студент") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

            var result = _mapper.Map<CommentOutputModel>(_homeworkService.GetCommentById(commentId));

            return Ok(result);
        }
        /// <summary>
        /// Get comment by HomeworkId
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>   
        /// <param name="commentId">Comment Id</param>
        /// <returns>Return searchable comment</returns>
        //https://localhost:44365/api/homework/2/attempt/2/comment/2
        [ProducesResponseType(typeof(CommentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{homeworkId}/comment/{commentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<CommentOutputModel> GetCommentsByHomeworkId(int homeworkId, int commentId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var commentDto = _homeworkService.GetCommentById(commentId);
            if (commentDto is null)
                return NotFound($"Comment with id {commentId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Студент") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTheme(int homeworkId, int themeId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto is null)
                return NotFound($"Theme with id {themeId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{homeworkId}/recovery")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult<HomeworkOutputModel> RecoverHomework(int homeworkId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{homeworkId}/attempt/{attemptId}/recovery")]
        [Authorize(Roles = "Администратор, Студент")]
        public ActionResult<HomeworkAttemptOutputModel> RecoverHomeworkAttempt(int homeworkId, int attemptId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var attemptDto = _homeworkService.GetHomeworkAttemptById(attemptId);
            if (attemptDto is null)
                return NotFound($"Attempt with id {attemptId} is not found");

            if (User.IsInRole("Студент") && attemptDto.Author.Id != Convert.ToInt32(User.FindFirst("id").Value))
                return Forbid($"User is not author attempt {attemptId}");

            var recoveredRows = _homeworkService.RecoverHomeworkAttempt(attemptId);
            var result = _mapper.Map<HomeworkAttemptOutputModel>(_homeworkService.GetHomeworkAttemptById(attemptId));

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{homeworkId}/theme/{themeId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTheme(int homeworkId, int themeId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var themeDto = _courseService.GetThemeById(themeId);
            if (themeDto is null)
                return NotFound($"Theme with id {themeId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            _homeworkService.AddHomework_Theme(homeworkId, themeId);

            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Add homework group
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="groupId">Group Id</param>
        /// <returns>Return status code 201</returns>
        // https://localhost:44365/api/homework/3/tag/1
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{homeworkId}/group/{groupId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult AddHomeworkGroup(int homeworkId, int groupId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var groupDto = _groupService.GetGroupById(groupId);
            if (groupDto is null)
                return NotFound($"Tag with id {groupId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            _homeworkService.AddHomeworkGroup(homeworkId, groupId);

            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Delete homework group
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="groupId">Group Id</param>
        /// <returns>Return no content response</returns>
        // https://localhost:44365/api/homework/3/tag/1
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}/group/{groupId}")]
        [Authorize(Roles = "Администратор, Преподаватель")]
        public ActionResult DeleteHomeworkGroup(int homeworkId, int groupId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var groupDto = _groupService.GetGroupById(groupId);
            if (groupDto is null)
                return NotFound($"Tag with id {groupId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

            _homeworkService.DeleteHomeworkGroup(homeworkId, groupId);

            return NoContent();
        }
        /// <summary>
        /// Add homework tag
        /// </summary>
        /// <param name="homeworkId">Homework Id</param>
        /// <param name="tagId">Tag Id</param>
        /// <returns>Return status code 201</returns>
        // https://localhost:44365/api/homework/3/tag/1
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult AddHomeworkTag(int homeworkId, int tagId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                return NotFound($"Homework with id {homeworkId} is not found");

            var tagDto = _tagService.GetTagById(tagId);
            if (tagDto is null)
                return NotFound($"Tag with id {tagId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
            {
                return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
            }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{homeworkId}/tag/{tagId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор")]
        public ActionResult DeleteHomeworkTag(int homeworkId, int tagId)
        {
            var homeworkDto = _homeworkService.GetHomeworkById(homeworkId);
            if (homeworkDto is null)
                    return NotFound($"Homework with id {homeworkId} is not found");

                var tagDto = _tagService.GetTagById(tagId);
                if (tagDto is null)
                    return NotFound($"Tag with id {tagId} is not found");

                var userGroup = this.SupplyUserGroupsList(_groupService);
                if (!User.IsInRole("Администратор") && !homeworkDto.Groups.Any(h => userGroup.Contains(h.Id)))
                {
                    return Forbid($"User is not in group {homeworkDto.Groups.Select(h => h.Id).ToList()}");
                }

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("attempt/by-user/{userId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptsByUserId(int userId)
        {
            var userDto = _userService.GetUserById(userId);
            if (userDto is null)
                return NotFound($"User with id {userId} is not found");

            var requsterGroups = this.SupplyUserGroupsList(_groupService);
            var userGroups = _groupService.GetGroupsByStudentId(userId);
            if (!User.IsInRole("Администратор") && userGroups.Intersect(requsterGroups).Count() == 0)
                return Forbid($"User is not in group of requester");

            var homeworkAttempts = _homeworkService.GetHomeworkAttemptsByUserId(userId);
            var avaliableHomeworkAttempts = GetAvaliableHomeworkAttepts(homeworkAttempts);
            var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(avaliableHomeworkAttempts);

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("attempt/by-group/{groupId}/by-status/{statusId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<List<HomeworkAttemptWithCountOutputModel>> GetHomeworkAttemptByStatusIdAndGroupId(int statusId, int groupId)
        {
            var groupDto = _groupService.GetGroupById(groupId);
            if (groupDto is null)
                return NotFound($"Group with id {groupId} is not found");

            if (!Enum.IsDefined(typeof(HomeworkAttemptStatus), statusId))
                return NotFound($"Status with id {statusId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !userGroup.Contains(groupId))
            {
                return Forbid($"User is not in group {groupId}");
            }

            var homeworkAttempts = _homeworkService.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId);
            var avaliableHomeworkAttempts = GetAvaliableHomeworkAttepts(homeworkAttempts);
            var result = _mapper.Map<List<HomeworkAttemptWithCountOutputModel>>(avaliableHomeworkAttempts);

            return Ok(result);
        }
        [HttpGet("homework-attempts-statuses")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<DictionaryOutputModel>), StatusCodes.Status200OK)]
        public ActionResult<List<DictionaryOutputModel>> GetHomeworkAttemptsStatuses()
        {
            var result = new List<DictionaryOutputModel>();
            foreach (int i in Enum.GetValues(typeof(HomeworkAttemptStatus)))
                result.Add(new DictionaryOutputModel { Id = i, Name = FriendlyNames.GetFriendlyGroupStatusName(((GroupStatus)i)) });
            return result;
        }
        private List<HomeworkDto> GetAvailableHomeworks(List<HomeworkDto> homeworkDtos)
        {
            var availableHomeworks = homeworkDtos;
            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор"))
            {
                availableHomeworks = new List<HomeworkDto>();
                homeworkDtos.ForEach(homework =>
                {
                    if (homework.Groups.Any(h => userGroup.Contains(h.Id)))
                    {
                        availableHomeworks.Add(homework);
                    }
                });
            }

            return availableHomeworks;
        }

        private List<HomeworkAttemptWithCountDto> GetAvaliableHomeworkAttepts(List<HomeworkAttemptWithCountDto> homeworkAttempts)
        {
            var avaliableHomeworkAttempts = homeworkAttempts;

            if (User.IsInRole("Студент"))
            {
                avaliableHomeworkAttempts = new List<HomeworkAttemptWithCountDto>();
                homeworkAttempts.ForEach(attempt =>
                {
                    if (attempt.Author.Id == Convert.ToInt32(User.FindFirst("id").Value))
                        avaliableHomeworkAttempts.Add(attempt);
                });
            }

            return avaliableHomeworkAttempts;
        }
    }
}
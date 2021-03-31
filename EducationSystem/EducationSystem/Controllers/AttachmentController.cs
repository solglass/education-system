using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using EducationSystem.Core.CustomExceptions;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/attachment/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentController : ControllerBase
    {  
        private IAttachmentService _service;
        private IHomeworkService _homeworkService;
        private IMapper _mapper;

        public AttachmentController(IAttachmentService attachmentService, IMapper mapper)
        {
            _service = attachmentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new Attachment
        /// </summary>
        /// <param name="attachmentInputModel">Model object of attachment to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/
        [ProducesResponseType(typeof(AttachmentOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult <AttachmentOutputModel> AddAttachment([FromBody] AttachmentInputModel attachmentInputModel)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);

            var attachmentDto = _mapper.Map<AttachmentDto>(attachmentInputModel);
            var newEntityId = _service.AddAttachment(attachmentDto);
            var newAttachmentDto = _service.GetAttachmentById(newEntityId);
            var result = _mapper.Map<AttachmentOutputModel>(newAttachmentDto);
            return Ok(result);
        }

        /// <summary>
        /// Get Attachment
        /// </summary>
        /// <param name="id">Id of the attachment to be returned</param>
        /// <returns>Return AttachmentOutputModel</returns>
        // https://localhost:44365/api/attachment/42
        [ProducesResponseType(typeof(AttachmentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<AttachmentOutputModel> GetAttachment(int id)
        {
            var attDto = _service.GetAttachmentById(id);
            if (attDto is null)
                return NotFound($"Attachment {id} was not found!");
            var attachment = _mapper.Map<AttachmentOutputModel>(attDto);
            return Ok(attachment);
        }

        /// <summary>
        /// Update Attachment
        /// </summary>
        /// <param name="id">Id of the attachment to be updated</param>
        /// <param name="attachmentInputModel">Model object of attachment with new parameters</param>
        /// <returns>Return updated AttachmentOutputModel</returns>
        // https://localhost:44365/api/attachment/42
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AttachmentOutputModel), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult<AttachmentOutputModel> UpdateAttachment([FromBody] AttachmentInputModel attachmentInputModel, int id)
        {
            if (_service.GetAttachmentById(id) is null)
                return NotFound($"Attachment {id} not found");
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var attDto = _mapper.Map<AttachmentDto>(attachmentInputModel);
            attDto.Id = id;
            _service.UpdateAttachment(attDto);
            var attachment = _service.GetAttachmentById(id);
            var result = _mapper.Map<AttachmentOutputModel>(attachment);
            return Ok(result);
        }

        /// <summary>
        /// Delete Attachment(by Id)
        /// </summary>
        /// <param name="id">Id of the attachment to be deleted</param>
        /// <returns>Return deleted AttachmentOutputModel</returns>
        // https://localhost:44365/api/attachment/42
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult DeleteAttachment(int id)
        {
            if (_service.GetAttachmentById(id) is null)
                return NotFound($"Attachment {id} not found");
            _service.DeleteAttachmentById(id);
            return NoContent();
        }


        /// <summary>
        /// Create new Attachment into Homework attempt(by Id)
        /// </summary>
        /// <param name="attachmentInputModel">Model object of attachment to be created</param>
        /// <param name="homeworkAttemptId">Id of the homework attempt where attachment to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/homeworkAttempt/4
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("homeworkAttempt/{homeworkAttemptId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult AddHomeworkAttemptAttachment([FromBody] AttachmentInputModel attachmentInputModel,  int homeworkAttemptId)
        {
            if (_homeworkService.GetHomeworkAttemptById(homeworkAttemptId) is null)
                return NotFound($"HomeworkAttempt {homeworkAttemptId} not found");
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var result = _service.AddHomeworkAttemptAttachment(_mapper.Map<AttachmentDto>(attachmentInputModel), 
                homeworkAttemptId);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Delete Attachment(by Id) from Homework attempt(by Id)
        /// </summary>
        /// <param name="attachmentId">Id of the attachment to be deleted</param>
        /// <param name="homeworkAttemptId">Id of the homework attempt where attachment to be deleted</param>
        /// <returns>Return deleted AttachmentOutputModel</returns>
        // https://localhost:44365/api/attachment/homeworkAttempt/4/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("homeworkAttempt/{homeworkAttemptId}/{attachmentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult DeleteHomeworkAttemptAttachment( int attachmentId, int homeworkAttemptId)
        {
            if (_homeworkService.GetHomeworkAttemptById(homeworkAttemptId) is null)
                return NotFound($"HomeworkAttempt {homeworkAttemptId} not found"); 

            if (_service.GetAttachmentById(attachmentId) is null)
                return NotFound($"Attachment {attachmentId} not found");

            var result = _service.DeleteHomeworkAttemptAttachment(attachmentId,
                homeworkAttemptId);
            return NoContent();
        }

        /// <summary>
        /// Create new Attachment into Comment(by Id)
        /// </summary>
        /// <param name="attachmentInputModel">Model object of attachment to be created</param>
        /// <param name="commentId">Id of the comment where attachment to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/comment/4
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("comment/{commentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult AddCommentAttachment([FromBody] AttachmentInputModel attachmentInputModel,  int commentId)
        {
            if (_homeworkService.GetCommentById(commentId) is null)
                return NotFound($"Comment {commentId} not found");
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var result = _service.AddCommentAttachment(_mapper.Map<AttachmentDto>(attachmentInputModel), commentId);
            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Delete Attachment(by Id) from Comment(by Id)
        /// </summary>
        /// <param name="attachmentId">Id of the attachment to be deleted</param>
        /// <param name="commentId">Id of the comment where attachment to be deleted</param>
        /// <returns>Return deleted AttachmentOutputModel</returns>
        //   https://localhost:44365/api/attachment/comment/4/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("comment/{commentId}/{attachmentId}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Студент")]
        public ActionResult DeleteCommentAttachment(int attachmentId , int commentId)
        {
            if (_homeworkService.GetCommentById(commentId) is null)
                return NotFound($"Comment {commentId} not found");

            if (_service.GetAttachmentById(attachmentId) is null)
                return NotFound($"Attachment {attachmentId} not found");

            var result = _service.DeleteCommentAttachment(attachmentId, commentId);
            return NoContent();
        }
    }
}


using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Http;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/attachment/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentController : ControllerBase
    {  
        private IAttachmentService _service;
        private IMapper _mapper;

        public AttachmentController(IAttachmentService attachmentService, IMapper mapper)
        {
            _service = attachmentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new Attachment
        /// </summary>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult <AttachmentOutputModel> AddAttachment([FromBody] AttachmentInputModel attachmentInputModel)
        {
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
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<AttachmentOutputModel> GetAttachment(int id)
        {
            var attDto = _service.GetAttachmentById(id);
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
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<AttachmentOutputModel> UpdateAttachment([FromBody] AttachmentInputModel attachmentInputModel, int id)
        {
            var attDto = _mapper.Map<AttachmentDto>(attachmentInputModel);
            attDto.Id = id;
            _service.ModifyAttachment(attDto, id);
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
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteAttachment(int id)
        {
            _service.DeleteAttachmentById(id);
            return NoContent();
        }

        /// <summary>
        /// Create new Attachment into Comment(by Id)
        /// </summary>
        /// <param name="attachmentInputModel">Model object of attachment to be created</param>
        /// <param name="commentId">Id of the comment where attachment to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/comment/4
        [HttpPost("comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddAttachmentToComment([FromBody] AttachmentInputModel attachmentInputModel,  int commentId)
        {
            var result = _service.AddCommentAttachment(_mapper.Map<AttachmentDto>(attachmentInputModel), commentId);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Create new Attachment into Homework attempt(by Id)
        /// </summary>
        /// <param name="attachmentInputModel">Model object of attachment to be created</param>
        /// <param name="homeworkAttemptId">Id of the homework attempt where attachment to be created</param>
        /// <returns>Return OutputModel of created attachment</returns>
        // https://localhost:44365/api/attachment/homeworkAttempt/4
        [HttpPost("homeworkAttempt/{homeworkAttemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddHomeworkAttemptAttachment([FromBody] AttachmentInputModel attachmentInputModel,  int homeworkAttemptId)
        {         
            var result = _service.AddAttachmentToHomeworkAttempt(_mapper.Map<AttachmentDto>(attachmentInputModel), 
                homeworkAttemptId);
           return Ok($"Вложение #{result} добавлено к попытке сдачи #{homeworkAttemptId}");
        }

        /// <summary>
        /// Delete Attachment(by Id) from Homework attempt(by Id)
        /// </summary>
        /// <param name="attachmentId">Id of the attachment to be deleted</param>
        /// <param name="homeworkAttemptId">Id of the homework attempt where attachment to be deleted</param>
        /// <returns>Return deleted AttachmentOutputModel</returns>
        // https://localhost:44365/api/attachment/homeworkAttempt/4/5
        [HttpDelete("homeworkAttempt/{homeworkAttemptId}/{attachmentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteHomeworkAttemptAttachment( int attachmentId, int homeworkAttemptId)
        {
            var result = _service.DeleteHomeworkAttemptAttachment(attachmentId,
                homeworkAttemptId);
            return NoContent();
        }

        //   https://localhost:44365/api/attachment/comment/4/5
        /// <summary>
        /// Delete Attachment(by Id) from Comment(by Id)
        /// </summary>
        /// <param name="attachmentId">Id of the attachment to be deleted</param>
        /// <param name="commentId">Id of the comment where attachment to be deleted</param>
        /// <returns>Return deleted AttachmentOutputModel</returns>
        //   https://localhost:44365/api/attachment/comment/4/5
        [HttpDelete("comment/{commentId}/{attachmentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteCommentAttachment(int attachmentId , int commentId)
        {
            var result = _service.DeleteCommentAttachment(attachmentId, commentId);
            return NoContent();
        }
    }
}


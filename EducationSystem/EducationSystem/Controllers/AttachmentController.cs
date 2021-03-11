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
        
        // https://localhost:44365/api/attachment/42
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<AttachmentOutputModel> GetAttachment(int id)
        {
           var attachment = new AttachmentOutputModel();
            try
            {
                attachment = _mapper.Map<AttachmentOutputModel>(_service.GetAttachmentById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(attachment);
        }

        // https://localhost:44365/api/attachment/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult<AttachmentOutputModel> UpdateAttachment([FromBody] AttachmentInputModel attachmentInputModel, int id)
        {
            try
            {
                var attDto = _mapper.Map<AttachmentDto>(attachmentInputModel);
                attDto.Id = id;
                _service.ModifyAttachment(attDto, id);
                var attachment = _service.GetAttachmentById(id);
                var result = _mapper.Map<AttachmentOutputModel>(attachment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // https://localhost:44365/api/attachment/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteAttachment(int id)
        {
            _service.DeleteAttachmentById(id);

            return NoContent();
        }
        // https://localhost:44365/api/attachment/comment/4
        [HttpPost("comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddCommentAttachment([FromBody] AttachmentInputModel attachmentInputModel,  int commentId)
        {
            var result = _service.AddCommentAttachment(_mapper.Map<AttachmentDto>(attachmentInputModel), commentId);
            return StatusCode(StatusCodes.Status201Created);
        }

       // https://localhost:44365/api/attachment/homeworkAttempt/4
        [HttpPost("homeworkAttempt/{homeworkAttemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddHomeworkAttemptAttachment([FromBody] AttachmentInputModel attachmentInputModel,  int homeworkAttemptId)
        {         
            var result = _service.AddHomeworkAttemptAttachment(_mapper.Map<AttachmentDto>(attachmentInputModel), 
                homeworkAttemptId);
            return StatusCode(StatusCodes.Status201Created);
        }

        // https://localhost:44365/api/attachment/homeworkAttempt/4/5
        [HttpDelete("homeworkAttempt/{homeworkAttemptId}/{attachmentId}")]
        public ActionResult DeleteHomeworkAttemptAttachment( int attachmentId, int homeworkAttemptId)
        {
            var result = _service.DeleteHomeworkAttemptAttachment(attachmentId,
                homeworkAttemptId);
            return NoContent();
        }

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


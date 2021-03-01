using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Controllers;
using EducationSystem.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/attachment/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentController : ControllerBase
    {  
        private IAttachmentService _service;
        private AttachmentMapper _attachmentMapper;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _service = attachmentService;
            _attachmentMapper = new AttachmentMapper();
        }

        // https://localhost:44365/api/attachment/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddAttachment([FromBody] AttachmentInputModel attachmentInputModel)
        {
            var attachmentDto = _attachmentMapper.ToDto(attachmentInputModel);
            var result = _service.AddAttachment(attachmentDto);
            return Ok($"Вложение #{result} добавлено");
        }

        
        // https://localhost:44365/api/attachment/42
        [HttpGet("{id}")]
        [AllowAnonymous]
        public dynamic GetAttachment(int id)
        {
           var attachment = new AttachmentOutputModel();
            try
            {
                attachment = _attachmentMapper.FromDto(_service.GetAttachmentById(id));
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
        public ActionResult UpdateAttachment([FromBody] AttachmentInputModel attachmentInputModel, int id)
        {
            try
            {
                _service.ModifyAttachment(_attachmentMapper.ToDto(attachmentInputModel, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok("Обновлено успешно");
        }

        // https://localhost:44365/api/attachment/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteAttachment(int id)
        {
            _service.DeleteAttachmentById(id);
            return Ok("Успех");
        }
     //   https://localhost:44365/api/attachment/comment/4
        [HttpPost("comment/{commentId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddAttachmentToComment([FromBody] AttachmentInputModel attachmentInputModel,  int commentId)
        {
            var attachmentDto = _attachmentMapper.ToDto(attachmentInputModel);
            var result = _service.AddAttachmentToComment(attachmentDto, commentId);
            return Ok($"Вложение #{result} добавлено к комментарию #{commentId}");
        }

       // https://localhost:44365/api/attachment/homeworkAttempt/4
        [HttpPost("homeworkAttempt/{homeworkAttemptId}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddAttachmentToHomeworkAttempt([FromBody] AttachmentInputModel attachmentInputModel,  int homeworkAttemptId)
        {
            var attachmentDto = _attachmentMapper.ToDto(attachmentInputModel);
            var result = _service.AddAttachmentToHomeworkAttempt(attachmentDto, 
                homeworkAttemptId);
            return Ok($"Вложение #{result} добавлено к попытке сдачи #{homeworkAttemptId}");
        }



    }
}


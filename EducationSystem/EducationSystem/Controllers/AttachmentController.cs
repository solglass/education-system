using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Controllers;
using EducationSystem.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/attachment/
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private AttachmentService _service;
        private AttachmentMapper _attachmentMapper;
        private AttachmentTypeMapper _attachmentTypeMapper;

        public AttachmentController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _service = new AttachmentService();
            _attachmentMapper = new AttachmentMapper();
            _attachmentTypeMapper = new AttachmentTypeMapper();
        }

        // https://localhost:44365/api/attachment/
        [HttpPost]
        public ActionResult AddAttachment([FromBody] AttachmentInputModel attachmentInputModel)
        {
            var attachmentDto = _attachmentMapper.ToDto(attachmentInputModel);
            var result = _service.AddAttachment(attachmentDto);
            return Ok($"Вложение #{result} добавлено");
        }

        // https://localhost:44365/api/attachment/
        [HttpGet]
        public ActionResult GetAttachments()
        {
            List<AttachmentOutputModel> attachments;
            try
            {
               attachments = _attachmentMapper.FromDtos(_service.GetAttachments());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(attachments);

        }

        // https://localhost:44365/api/attachment/42
        [HttpGet("{id}")]
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
        public ActionResult UpdateAttachment([FromBody] AttachmentInputModel attachmentInputModel)
        {
            try
            {
                _service.ModifyAttachment(_attachmentMapper.ToDto(attachmentInputModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok("Обновлено успешно");
        }

        // https://localhost:44365/api/attachment/42
        [HttpDelete("{id}")]
        public ActionResult DeleteAttachment(int id)
        {
            _service.DeleteAttachmentById(id);
            return Ok("Успех");
        }

        // https://localhost:44365/api/attachment/attachmentType/
        [HttpPost("attachmentType/{name}")]
        public ActionResult AddAttachmentType(string name)
        {
            var result = _service.AddAttachmentType(name);
            return Ok($"Тип вложения #{result} добавлен");
        }



        // https://localhost:44365/api/attachment/attachmentType
        [HttpGet("attachmentType")]
        public ActionResult GetAttachmentTypes()
        {
            List<AttachmentTypeOutputModel> attachmentTypes;
            try
            {
                attachmentTypes = _attachmentTypeMapper.FromDtos(_service.GetAttachmentTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(attachmentTypes);
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpGet("attachmentType/{id}")]
        public dynamic GetAttachmentType(int id)
        {
            var attachmentType = new AttachmentTypeOutputModel();
            try
            {
                attachmentType = _attachmentTypeMapper.FromDto(_service.GetAttachmentTypeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(attachmentType);
        }

        // https://localhost:44365/api/attachment/attachmentType/42/IMAGE
        [HttpPut("attachmentType/{id}/{name}")]
        public ActionResult UpdateAttachmentType(int id, string name)
        {
            _service.ModifyAttachmentType(id, name);
            return Ok("Успех");
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpDelete("attachmentType/{id}")]
        public ActionResult DeleteAttachmentType(int id)
        {
            _service.DeleteAttachmentTypeById(id);
            return Ok("Успех");
        }
        


    }
}


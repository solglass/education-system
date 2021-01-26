using EducationSystem.Controllers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    // https://localhost:44365/api/attachment/
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private AttachmentRepository _repo;

        public AttachmentController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new AttachmentRepository();
        }

        // https://localhost:44365/api/attachment/register
        [HttpPost("register")]
        public ActionResult Register(string path, int typeId)
        {
            AttachmentDto attachmentDto = new AttachmentDto();
            attachmentDto.Path = path;
            AttachmentTypeDto attachmentTypeDto = _repo.GetAttachmentTypeById(typeId);
            attachmentDto.AttachmentType = attachmentTypeDto;
            _repo.AddAttachment(attachmentDto);
            return Ok("Вложение добавлено");
        }

        // https://localhost:44365/api/attachment/
        [HttpGet]
        public ActionResult GetAttachments()
        {
            var attachments = _repo.GetAttachments();
            return Ok(attachments);
        }

        // https://localhost:44365/api/attachment/42
        [HttpGet("{id}")]
        public dynamic GetAttachment(int id)
        {
            var attachment = _repo.GetAttachmentById(id);
            return Ok(attachment);
        }

        // https://localhost:44365/api/attachment/42
        [HttpPut("{id}")]
        public ActionResult UpdateAttachment(int attachmentId, string path, int typeId)
        {
            _repo.ModifyAttachment(attachmentId, path, typeId);
            return Ok("success");
        }

        // https://localhost:44365/api/attachment/42
        [HttpDelete("{id}")]
        public ActionResult DeleteAttachment(int id)
        {
            _repo.DeleteAttachmentById(id);
            return Ok("success");
        }

        // https://localhost:44365/api/attachment/attachmentType/register
        [HttpPost("attachmentType/register")]
        public ActionResult Register(string name)
        {
            AttachmentTypeDto attachmentTypeDto = new AttachmentTypeDto();
            attachmentTypeDto.Name = name;
            _repo.AddAttachmentType(attachmentTypeDto);
            return Ok("Тип вложения добавлен");
        }



        // https://localhost:44365/api/attachment/attachmentType
        [HttpGet("attachmentType")]
        public ActionResult GetAttachmentTypes()
        {
            var attachmentTypes = _repo.GetAttachmentTypes();
            return Ok(attachmentTypes);
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpGet("attachmentType/{id}")]
        public dynamic GetAttachmentType(int id)
        {
            var attachmentType = _repo.GetAttachmentTypeById(id);
            return Ok(attachmentType);
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpPut("attachmentType/{id}")]
        public ActionResult UpdateAttachmentType(int attachmentTypeId, string name)
        {
            _repo.ModifyAttachmentType(attachmentTypeId, name);
            return Ok("success");
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpDelete("attachmentType/{id}")]
        public ActionResult DeleteAttachmentType(int id)
        {
            _repo.DeleteAttachmentTypeById(id);
            return Ok("success");
        }
        


    }
}


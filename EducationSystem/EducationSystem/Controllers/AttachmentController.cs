﻿using EducationSystem.API.Mappers;
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

       
        private AttachmentService _service;
        private AttachmentMapper _attachmentMapper;
        private AttachmentTypeMapper _attachmentTypeMapper;

        public AttachmentController()
        {
            _service = new AttachmentService();
            _attachmentMapper = new AttachmentMapper();
            _attachmentTypeMapper = new AttachmentTypeMapper();
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

        // https://localhost:44365/api/attachment/
        [HttpGet]
        [AllowAnonymous]
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
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteAttachment(int id)
        {
            _service.DeleteAttachmentById(id);
            return Ok("Успех");
        }

        // https://localhost:44365/api/attachment/attachmentType/
        [HttpPost("attachmentType/{name}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult AddAttachmentType(string name)
        {
            var result = _service.AddAttachmentType(name);
            return Ok($"Тип вложения #{result} добавлен");
        }



        // https://localhost:44365/api/attachment/attachmentType
        [HttpGet("attachmentType")]
        [AllowAnonymous]
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
        [AllowAnonymous]

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
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult UpdateAttachmentType(int id, string name)
        {
            _service.ModifyAttachmentType(id, name);
            return Ok("Успех");
        }

        // https://localhost:44365/api/attachment/attachmentType/42
        [HttpDelete("attachmentType/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Студент, Тьютор")]
        public ActionResult DeleteAttachmentType(int id)
        {
            _service.DeleteAttachmentTypeById(id);
            return Ok("Успех");
        }
        //  https://localhost:44365/api/attachment/comment/4
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


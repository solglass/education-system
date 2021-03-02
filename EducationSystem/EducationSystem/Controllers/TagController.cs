using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Models;
using EducationSystem.API.Mappers;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using EducationSystem.Business;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/tag/
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private TagMapper _tagMapper;
        private TagService _tagService;
        public TagController()
        {
            _tagService = new TagService();
            _tagMapper = new TagMapper();
        }
        // https://localhost:50221/api/tag/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult AddTag([FromBody] TagInputModel tag)
        {
            var tagDto = _tagMapper.ToDto(tag);
            var result= _tagService.AddTag(tagDto);
            return Ok($"Тег№{result} добавлен");
        }
        // https://localhost:50221/api/tag
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult GetTags()
        {
            var tagsDtos = _tagService.GetTags();
            var tags = _tagMapper.FromDtos(tagsDtos);
            return Ok(tags);
        }
        // https://localhost:50221/api/tag/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult GetTag(int id)
        {
            var tagDto = _tagService.GetTagById(id);
            var tag = _tagMapper.FromDto(tagDto);
            return Ok(tag);
        }
        // https://localhost:50221/api/tag/3
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult UpdateTag(int id, [FromBody] TagInputModel tag)
        {
            var tagDto = _tagMapper.ToDto(tag);
            _tagService.UpdateTag(tagDto);
            return Ok("Tag обновлён");
        }
        // https://localhost:50221/api/tag/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult DeleteTag(int id)
        {
            _tagService.DeleteTag(id);
            return Ok("Tag удалён");
        }
    }
}

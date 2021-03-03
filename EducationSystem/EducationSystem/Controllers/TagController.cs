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
using AutoMapper;
using EducationSystem.API.Models.OutputModels;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/tag/
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private ITagService _tagService;
        private readonly IMapper _mapper;
        public TagController(ITagRepository tagRepository, ITagService tagService, IMapper mapper)
        {
            _mapper = mapper;
            _tagService = tagService;
        }
        // https://localhost:50221/api/tag/
        [HttpPost]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult AddTag([FromBody] TagInputModel tag)
        {
            var tagDto = _mapper.Map<TagDto>(tag);
            var result= _tagService.AddTag(tagDto);
            return Ok($"Тег№{result} добавлен");
        }
        // https://localhost:50221/api/tag
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult GetTags()
        {
            var tagsDtos = _tagService.GetTags();
           
            var tags = _mapper.Map<List<TagOutputModel>>(tagsDtos);
            return Ok(tags);
        }
        // https://localhost:50221/api/tag/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult GetTag(int id)
        {
            var tagDto = _tagService.GetTagById(id);
            var tag = _mapper.Map<TagOutputModel>(tagDto);
            return Ok(tag);
        }
        //https://localhost:50221/api/tag/3
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult UpdateTag(int id, [FromBody] TagInputModel tag)
        {
            var tagDto = _mapper.Map<TagDto>(tag);
            tagDto.Id = id;
            _tagService.UpdateTag(tagDto);          
            return Ok("Tag обновлён");
        }
        //https://localhost:50221/api/tag/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult DeleteTag(int id)
        {
            _tagService.DeleteTag(id);
            return Ok("Tag удалён");
        }        
    }
}

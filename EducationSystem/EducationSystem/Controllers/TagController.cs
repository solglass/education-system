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
using System.Net;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/tag/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public ActionResult<TagOutputModel> AddTag([FromBody] TagInputModel tag)
        {
            var tagDto = _mapper.Map<TagDto>(tag);
            var id= _tagService.AddTag(tagDto);
           var result= _mapper.Map<TagOutputModel>(_tagService.GetTagById(id));
            return Ok(result);
        }
        // https://localhost:50221/api/tag
        [HttpGet]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult<List<TagOutputModel>> GetTags()
        {
            var tagsDtos = _tagService.GetTags();
            var tags = _mapper.Map<List<TagOutputModel>>(tagsDtos);
            return Ok(tags);
        }
        // https://localhost:50221/api/tag/3
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист,Студент")]
        public ActionResult<TagOutputModel> GetTag(int id)
        {
            var tagDto = _tagService.GetTagById(id);
            var tag = _mapper.Map<TagOutputModel>(tagDto);
            return Ok(tag);
        }
        //https://localhost:50221/api/tag/3
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult<TagOutputModel> UpdateTag(int id, [FromBody] TagInputModel tag)
        {
            var tagDto = _mapper.Map<TagDto>(tag);
            tagDto.Id = id;
            _tagService.UpdateTag(tagDto);
            var result = _mapper.Map<TagOutputModel>(_tagService.GetTagById(id));
            return Ok(result);
        }
        //https://localhost:50221/api/tag/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист")]
        public ActionResult DeleteTag(int id)
        {
            var result=_tagService.DeleteTag(id);
            if (result == 0)
            {
                return new NoContentResult();
            }
            return Ok();
        }
    }
}

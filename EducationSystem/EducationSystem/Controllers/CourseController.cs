using AutoMapper;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EducationSystem.API.Controllers
{
    // https://localhost:50221/api/course/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CourseController : ControllerBase
    {
        private ICourseService _courseService;
        private IMapper _mapper;
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        // https://localhost:50221/api/course/
        [HttpGet]
        public ActionResult<List<CourseOutputModel>> GetCourses()
        {
            var courses =_mapper.Map<List<CourseOutputModel>>( _courseService.GetCourses());
            return Ok(courses);
        }

        // https://localhost:50221/api/course/id
       [HttpGet("{id}")]
       public ActionResult<CourseOutputModel> GetCourse(int id)       
        {
            var  course = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            return Ok(course);
        }

        // https://localhost:50221/api/course/
        [HttpPost]
       [Authorize(Roles ="Админ, Менеджер, Методист")]
        public ActionResult<CourseOutputModel> CreateCourse([FromBody] CourseInputModel course)    
        {
            var id = _courseService.AddCourse(_mapper.Map<CourseDto>(course));
            var result = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            return Ok(result);                        
        }

        // https://localhost:50221/api/course/id
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<CourseOutputModel> UpdateCourseInfo(int id, [FromBody] CourseInputModel course)
        {
            var courseDto = _mapper.Map<CourseDto>(course);
            courseDto.Id = id;
            var result = _courseService.UpdateCourse(courseDto);
            var updateResult = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            return Ok(updateResult);
        }

        // https://localhost:50221/api/course/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<CourseOutputModel> DeleteCourse(int id)
        {
            var result = _courseService.DeleteCourse(id);
            var deleteResult = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            return Ok(deleteResult);
        }

        // https://localhost:XXXXX/api/course/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<CourseOutputModel> RecoverCourse(int id)
        {
            var result = _courseService.RecoverCourse(id);
            var recoverResult = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            return Ok(recoverResult);
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpPost("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult AddThemeToCourse(int courseId, int themeId)
        {
            int result = _courseService.AddThemeToCourse(courseId, themeId);
            return NoContent();                                                       
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpDelete("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult RemoveThemeFromCourse(int courseId, int themeId)
        {
            var result = _courseService.RemoveThemeFromCourse(courseId, themeId);
            return NoContent();
        }

        // https://localhost:XXXXX/api/course/theme/
        [HttpGet("theme")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult<List<ThemeOutputModel>> GetAllThemes()
        {
            var themes = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetThemes());
            return Ok(themes);
        }

        // https://localhost:XXXXX/api/course/theme/id
        [HttpGet("theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<ThemeOutputModel> GetThemeById(int id)
        {
            var theme = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(theme);

        }

        // https://localhost:XXXXX/api/course/theme/
        [HttpPost("theme")]
       [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> CreateTheme([FromBody] ThemeInputModel inputModel)
        {
            var  id = _courseService.AddTheme(_mapper.Map<ThemeDto>(inputModel));
            var result = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(result);                        

        }

        // https://localhost:XXXXX/api/course/theme/id/tag/id
        [HttpPost("theme/{themeId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель, Тьютор")]
        public ActionResult AddTagToTheme(int themeId, int tagId)
        {
            var result = _courseService.AddTagToTheme(themeId, tagId);
            return NoContent();                                                      
        }

        // https://localhost:XXXXX/api/course/theme/id/
        [HttpDelete("theme/{id}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> DeleteTheme(int id)
        {
            var result = _courseService.DeleteTheme(id);
            var deleteResult = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(deleteResult);
        }

        [HttpPut("theme/{id}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> RecoverTheme(int id)
        {
            var result = _courseService.RecoverTheme(id);
            var recoverResult = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(recoverResult);
        }
    }
}

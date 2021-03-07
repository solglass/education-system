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
    [Authorize]
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
        public ActionResult GetCourses()
        {
            List<CourseOutputModel> courses;
            try
            {
                courses =_mapper.Map<List<CourseOutputModel>>( _courseService.GetCourses());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(courses);
        }

        // https://localhost:50221/api/course/id
       [HttpGet("{id}")]
       public ActionResult GetCourse(int id)       
        {
            CourseOutputModel course;
            try
            {
                 course = _mapper.Map<CourseOutputModel>(_courseService.GetCourseById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(course);
        }

        // https://localhost:50221/api/course/
        [HttpPost]
       [Authorize(Roles ="Админ, Менеджер, Методист")]
        public ActionResult CreateCourse([FromBody] CourseInputModel course)    
        {
            int result;
            try
            {
                result = _courseService.AddCourse(_mapper.Map<CourseDto>(course));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result > 0)
                return Ok($"Курс №{result} добавлен!");
            else if (result == -1)
                return Problem("Ошибка! Не получилось добавить курс!");
            else
                return Problem($"Ошибка! К созданному курсу #{-(result+2)} не удалось привязать темы! ") ;
        }

        // https://localhost:50221/api/course/id
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult UpdateCourseInfo(int id, [FromBody] CourseInputModel course)
        {
            int result;
            try
            {
                var courseDto = _mapper.Map<CourseDto>(course);
                courseDto.Id = id;
                result = _courseService.UpdateCourse(courseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result == 0)
                return Ok($"Курс #{id} обновлен!");
            else 
                return Problem($"Ошибка! Не получилось обновить курс #{id}!");
        }

        // https://localhost:50221/api/course/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult DeleteCourse(int id)
        {
            var result = _courseService.DeleteCourse(id);
            if (result == 1)
                return Ok($"Курс #{id} удален!");
            else
                return Problem($"Ошибка! Не получилось удалить курс #{id}!");
        }

        // https://localhost:XXXXX/api/course/id/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult RecoverCourse(int id)
        {
            var result = _courseService.RecoverCourse(id);
            if (result == 1)
                return Ok($"Курс #{id} восстановлен!");
            else
                return Problem($"Ошибка! Не получилось восстановить курс #{id}!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpPost("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult AddThemeToCourse(int courseId, int themeId)
        {
            int result = _courseService.AddThemeToCourse(courseId, themeId);
            if (result > 0) 
                return Ok($"Тема {themeId} добавлена к курсу #{courseId}");
            else 
                return Problem($"Ошибка! Не получилось добавить к курсу #{courseId} тему #{themeId}!");
        }

        // https://localhost:XXXXX/api/course/3/theme/8
        [HttpDelete("{courseId}/theme/{themeId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult RemoveThemeFromCourse(int courseId, int themeId)
        {
            var result = _courseService.RemoveThemeFromCourse(courseId, themeId);
            if (result== 0)
                return Ok($"Тема {themeId} отвязана от курса #{courseId}");
            else
                return Problem($"Ошибка! Не получилось отвязать тему #{themeId} от курса #{courseId}!");
        }

        // https://localhost:XXXXX/api/course/theme/
        [HttpGet("theme")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult GetAllThemes()
        {
            List<ThemeOutputModel> themes;
            try
            {
                themes = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetThemes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(themes);
        }

        // https://localhost:XXXXX/api/course/theme/id
        [HttpGet("theme/{id}")]
        [Authorize(Roles = "Админ, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult GetThemeById(int id)
        {
            ThemeOutputModel theme;
            try
            {
                theme = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(theme);

        }

        // https://localhost:XXXXX/api/course/theme/
        [HttpPost("theme")]
        [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult CreateTheme([FromBody] ThemeInputModel inputModel)
        {
            int result;
            try
            {
               result = _courseService.AddTheme(_mapper.Map<ThemeDto>(inputModel));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (result > 0)
                return Ok($"Тема №{result} добавлена!");
            else
                return Problem($"Ошибка! К созданной теме #{-(result + 2)} не удалось привязать теги! ");
        }

        // https://localhost:XXXXX/api/course/theme/id/tag/id
        [HttpPost("theme/{themeId}/tag/{tagId}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель, Тьютор")]
        public ActionResult AddTagToTheme(int themeId, int tagId)
        {
            var result = _courseService.AddTagToTheme(themeId, tagId);
            if (result > 0)
                return Ok($"Тег #{tagId} добавлен к теме #{themeId}!");
            else
                return Problem($"Ошибка! Не получилось добавить тег  #{tagId} к теме #{themeId}!");
        }

        // https://localhost:XXXXX/api/course/theme/id/
        [HttpDelete("theme/{id}")]
        [Authorize(Roles = "Админ, Методист, Преподаватель")]
        public ActionResult DeleteTheme(int id)
        {
            var result = _courseService.DeleteTheme(id);
            if (result > 0)
                return Ok($"Тема удалена #{id}!");
            else
                return Problem($"Ошибка! Не получилось удалить тему #{id}!");
        }
    }
}

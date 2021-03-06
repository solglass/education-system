using AutoMapper;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EducationSystem.Core.CustomExceptions;

namespace EducationSystem.API.Controllers
{
    // https://localhost:50221/api/course/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private ICourseService _courseService;
        ITagService _tagService;
        private IMapper _mapper;
        public CourseController(ICourseService courseService, ITagService tagService , IMapper mapper)
        {
            _courseService = courseService;
            _tagService = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all the courses with their themes
        /// </summary>
        /// <returns>Returns the list of CourseOutputModels</returns>
        // https://localhost:50221/api/course/
        [ProducesResponseType(typeof(List<CourseOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<CourseOutputModel>> GetCourses()
        {
            var courses =_mapper.Map<List<CourseOutputModel>>( _courseService.GetCourses());
            return Ok(courses);
        }


        /// <summary>
        /// Gets only one course by its id  and the list of its ordered themes
        /// </summary>
        /// <param name="id"> is used to find necessary course</param>
        /// <returns>Returns the CourseWithProgramOutputModel which includes ordered themes of selected course</returns>
        // https://localhost:50221/api/course/id
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
       public ActionResult<CourseWithProgramOutputModel> GetCourse(int id)       
        {
            var  course = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(id));
            if(course == null)
            {
                return NotFound($"Course with id: {id} not found");
            }
            return Ok(course);
        }


        /// <summary>
        /// Creates Course
        /// </summary>
        /// <param name="course"> is used to get all the information about new course that is necessary to create it</param>
        /// <returns>Returns the CourseWithProgramOutputModel </returns>
        // https://localhost:50221/api/course/
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [HttpPost]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> CreateCourse([FromBody] CourseInputModel course)    
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var id = _courseService.AddCourse(_mapper.Map<CourseDto>(course));
            var result = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(id));
            return Ok(result);                        
        }

        /// <summary>
        /// Creates the copy of selected course and its program
        /// </summary>
        /// <param name="id"> is used to get all the information about  course that is necessary to copy</param>
        /// <returns>Returns the CourseWithProgramOutputModel </returns>
        // https://localhost:50221/api/course/
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpPost("{id}/copy")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> CopyCourse(int id)
        {
            if (_courseService.GetCourseById(id) == null)
                return NotFound($"The course with {id} is not found.");
            var newId = _courseService.AddCourseCopy(id);
            var result = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(newId));
            return Ok(result);
        }

        /// <summary>
        /// Updates Course
        /// </summary>
        /// /// <param name="id"> is used to find the course user wants to update</param>
        /// <param name="course"> is used to provide new information about selected course</param>
        /// <returns>Returns the CourseWithProgramOutputModel which includes ordered themes of selected course</returns>
        // https://localhost:50221/api/course/id
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> UpdateCourseInfo(int id, [FromBody] CourseInputModel course)
        {
            if (_courseService.GetCourseById(id) is null)
                return NotFound($"Course with id: {id} not found");
            var courseDto = _mapper.Map<CourseDto>(course);
            courseDto.Id = id;
            var result = _courseService.UpdateCourse(courseDto);
            var updateResult = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(id));
            return Ok(updateResult);
        }


        /// <summary>
        /// Deletes Course
        /// </summary>
        /// /// <param name="id"> is used to find the course user wants to delete</param>
        /// <returns>Returns the CourseWithProgramOutputModel which includes ordered themes of selected course</returns>
        // https://localhost:50221/api/course/id
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> DeleteCourse(int id)
        {
            if (_courseService.GetCourseById(id) is null)
                return NotFound($"Course with id: {id} not found");
            var result = _courseService.DeleteCourse(id);          
            var deleteResult = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(id));
            return Ok(deleteResult);
        }

        /// <summary>
        /// Recovers Course
        /// </summary>
        /// /// <param name="id"> is used to find the course user wants to recover</param>
        /// <returns>Returns the CourseWithProgramOutputModel which includes ordered themes of selected course</returns>
        // https://localhost:XXXXX/api/course/id/recovery
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CourseWithProgramOutputModel), StatusCodes.Status404NotFound)]
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> RecoverCourse(int id)
        {
            if (_courseService.GetCourseById(id) == null)
                return NotFound($"Course with id: {id} not found");
            var result = _courseService.RecoverCourse(id);
            var recoverResult = _mapper.Map<CourseWithProgramOutputModel>(_courseService.GetCourseWithProgramById(id));
            return Ok(recoverResult);
        }

        /// <summary>
        /// Recreates the course program that consists of list of ordered themes
        /// </summary>
        /// <param name="courseId"> is used to find the course user wants to connect with theme</param>
        /// <param name="program"> is used to specify the order of themes in course</param>
        /// <returns>Returns CourseWithProgramOutputModel</returns>
        // https://localhost:XXXXX/api/course/3/theme/8
        [ProducesResponseType(typeof(CourseWithProgramOutputModel),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string),StatusCodes.Status409Conflict)]
        [HttpPost("{courseId}/program")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<CourseWithProgramOutputModel> UpdateCourseProgram(int courseId, [FromBody]List<OrderedThemeInputModel> program)
        {
            if (_courseService.GetCourseById(courseId) == null)
                return NotFound($"Course with id:{courseId} not found");
              _courseService.AddUpdateCourseProgram(courseId, _mapper.Map<List<OrderedThemeDto>>(program));

            var result =_mapper.Map<CourseWithProgramOutputModel>( _courseService.GetCourseWithProgramById(courseId));
            return Ok(result);
        }

        
        /// <summary>
        /// Creates the connection between one material and one course
        /// </summary>
        /// <param name="courseId"> is used to find the course user wants to connect with material</param>
        /// <param name="materialId"> is used to find the material user wants to connect with course</param>
        /// <returns>Returns Created result</returns>
        // https://localhost:XXXXX/api/course/3/material/8
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{courseId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult AddMaterialToCourse(int courseId, int materialId)
        {
            int result = _courseService.AddMaterialToCourse(courseId, materialId);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Deletes the connection between one material and one course
        /// </summary>
        /// <param name="courseId"> is used to find the course user wants to break the connection with material</param>
        /// <param name="materialId"> is used to find the material user wants to break the connection with course</param>
        /// <returns>Returns NoContent result</returns>
        // https://localhost:XXXXX/api/material/3/theme/8
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{courseId}/material/{materialId}")]
        [Authorize(Roles = "Админ, Менеджер, Методист")]
        public ActionResult DeleteMaterialFromCourse(int courseId, int materialId)
        {
            var result = _courseService.DeleteMaterialFromCourse(courseId, materialId);
            return NoContent();
        }

        /// <summary>
        /// Gets all the themes with their tags
        /// </summary>
        /// <returns>Returns the list of ThemeOutputModels</returns>
        // https://localhost:XXXXX/api/course/theme/
        [ProducesResponseType(typeof(List<ThemeOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("theme")]
        [Authorize(Roles = "Администратор, Менеджер, Методист")]
        public ActionResult<List<ThemeOutputModel>> GetAllThemes()
        {
            var themes = _mapper.Map<List<ThemeOutputModel>>(_courseService.GetThemes());
            return Ok(themes);
        }

        /// <summary>
        /// Gets only one theme by its id  and the list of its tags
        /// </summary>
        /// <param name="id"> is used to find necessary theme</param>
        /// <returns>Returns the ThemeOutputModel</returns>
        // https://localhost:XXXXX/api/course/theme/id
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status404NotFound)]
        [HttpGet("theme/{id}")]
        [Authorize(Roles = "Администратор, Преподаватель, Тьютор, Методист, Студент")]
        public ActionResult<ThemeOutputModel> GetThemeById(int id)
        {
            if (_courseService.GetThemeById(id) == null)
                return NotFound($"Theme with id:{id} not found");
            var theme = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(theme);

        }

        /// <summary>
        /// Creates Theme
        /// </summary>
        /// <param name="theme"> is used to get all the information about new theme that is necessary to create it</param>
        /// <returns>Returns the ThemeOutputModel </returns>
        // https://localhost:XXXXX/api/course/theme/
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status409Conflict)]
        [HttpPost("theme")]
        [Authorize(Roles = "Администратор, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> CreateTheme([FromBody] ThemeInputModel theme)
        {
            if (!ModelState.IsValid)
                throw new ValidationException(ModelState);
            var  id = _courseService.AddTheme(_mapper.Map<ThemeDto>(theme));
            var result = _mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id));
            return Ok(result);                        

        }

        /// <summary>
        /// Creates the connection between one theme and one tag
        /// </summary>
        /// <param name="themeId"> is used to find the theme user wants to connect with tag</param>
        /// <param name="tagId"> is used to find the tag user wants to connect with theme</param>
        /// <returns>Returns Created result</returns>
        // https://localhost:XXXXX/api/course/theme/id/tag/id
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("theme/{themeId}/tag/{tagId}")]
        [Authorize(Roles = "Администратор, Методист, Преподаватель, Тьютор")]
        public ActionResult AddTagToTheme(int themeId, int tagId)
        {
            if (_courseService.GetThemeById(themeId) == null)
                return NotFound($"Theme with id:{themeId} not found");
            if (_tagService.GetTagById(tagId) == null)
                return NotFound($"Tag with id:{tagId} not found");
            _courseService.AddTagToTheme(themeId, tagId);
            return StatusCode(StatusCodes.Status201Created);                                                      
        }

        /// <summary>
        /// Deletes the connection between one theme and one course
        /// </summary>
        /// <param name="themeId"> is used to find the theme user wants to to break the connection with tag</param>
        /// <param name="tagId"> is used to find the tag user wants to break the connection with theme</param>
        /// <returns>Returns NoContent result</returns>
        // https://localhost:XXXXX/api/course/theme/id/tag/id
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("theme/{themeId}/tag/{tagId}")]
        [Authorize(Roles = "Администратор, Методист, Преподаватель, Тьютор")]
        public ActionResult RemoveTagFromTheme(int themeId, int tagId)
        {
            if (_courseService.GetThemeById(themeId) == null)
                return NotFound($"Theme with id:{themeId} not found");
            if (_tagService.GetTagById(tagId) == null)
                return NotFound($"Tag with id:{tagId} not found");
            _courseService.RemoveTagFromTheme(themeId, tagId);
            return NoContent();
        }

        /// <summary>
        /// Deletes Theme
        /// </summary>
        /// /// <param name="id"> is used to find the theme user wants to delete</param>
        /// <returns>Returns the ThemeOutputModel </returns>
        // https://localhost:XXXXX/api/course/theme/id/
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status404NotFound)]
        [HttpDelete("theme/{id}")]
        [Authorize(Roles = "Администратор, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> DeleteTheme(int id)
        {
            if (_courseService.GetThemeById(id) == null)
                return NotFound($"Theme with id:{id} not found");
            _courseService.DeleteTheme(id);           
            return Ok(_mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id)));
        }

        /// <summary>
        /// Recovers Theme
        /// </summary>
        /// /// <param name="id"> is used to find the theme user wants to recover</param>
        /// <returns>Returns the ThemeOutputModel </returns>
        // https://localhost:XXXXX/api/course/theme/id/
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ThemeOutputModel), StatusCodes.Status404NotFound)]
        [HttpPut("theme/{id}")]
        [Authorize(Roles = "Администратор, Методист, Преподаватель")]
        public ActionResult<ThemeOutputModel> RecoverTheme(int id)
        {
            if (_courseService.GetThemeById(id) == null)
                return NotFound($"Theme with id:{id} not found");
            _courseService.RecoverTheme(id);           
            return Ok(_mapper.Map<ThemeOutputModel>(_courseService.GetThemeById(id)));
        }
    }
}

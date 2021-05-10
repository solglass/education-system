using EducationSystem.Business.Model;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationSystem.Business
{
    public class CourseService : ICourseService
    {
        private ICourseRepository _courseRepo;
        private ITagRepository _tagRepo;


        public CourseService(ICourseRepository courseRepository,ITagRepository tagRepository)
        {
            _courseRepo = courseRepository;
            _tagRepo = tagRepository;

        }

        public List<CourseDto> GetCourses()
        {
            return _courseRepo.GetCourses();
        }

        public CourseDto GetCourseById(int id) => _courseRepo.GetCourseById(id);
        public CourseDto GetCourseWithProgramById(int id)
        {
            var course = _courseRepo.GetCourseById(id);
            if (course != null)
            {
                course.Themes = _courseRepo.GetCourse_Program(id);
            }
            return course;
        }

        public int AddCourseCopy(int id)
        {
            var course = _courseRepo.GetCourseById(id);
            int copyId = 0;
            if (course!=null)
            {
                var program = _courseRepo.GetCourse_Program(id);
                course.Name = $"{course.Name} - копия";
                course.Id = 0;
                copyId = _courseRepo.AddCourse(course);
                if (copyId>0)
                {
                    _courseRepo.AddCourse_Program(program.Select(obj => new CourseThemeDto
                    {
                        Course= new CourseDto { Id=copyId},
                        Theme= obj,
                        Order=obj.Order
                    }).ToList());

                    foreach (var material in course.Materials)
                    {
                        _courseRepo.AddCourse_Material(copyId, material.Id);
                    }
                }
            }
            return copyId;
        }

        public int UpdateCourse(CourseDto course)
        {
            int index = _courseRepo.UpdateCourse(course);
            if (index <= 0)
                return -1;
            return 0;
        }
        public int AddCourse(CourseDto course)
        {
            int index = _courseRepo.AddCourse(course);
            if (index <= 0)
                return -1;
            return index;
        }

        public int DeleteCourse(int id)
        {
            bool isDeleted = true;
            return _courseRepo.DeleteOrRecoverCourse(id, isDeleted);
        }

        public int RecoverCourse(int id)
        {
            bool isDeleted = false;
            return _courseRepo.DeleteOrRecoverCourse(id, isDeleted);
        }

        public int AddMaterialToCourse(int courseId, int materialId)
        {
            return _courseRepo.AddCourse_Material(courseId, materialId);
        }

        public int DeleteMaterialFromCourse(int courseId, int materialId)
        {
            return _courseRepo.DeleteCourse_Material(courseId, materialId);
        }

        public List<ThemeDto> GetThemes()
        {
            return _courseRepo.GetThemes();
        }

        public ThemeDto GetThemeById(int id)
        {
            return _courseRepo.GetThemeById(id);
        }

        public int AddTheme(ThemeDto theme)
        {
            int index = _courseRepo.AddTheme(theme);
            if (index <= 0)
                return -1;
            if (theme.Tags != null && theme.Tags.Count > 0)
            {
                foreach (var tag in theme.Tags)
                {
                    if (_tagRepo.ThemeTagAdd( index,  tag.Id ) <= 0)
                    {
                        return -2 - index;
                    }
                }
            }
            return index;
        }

        public int AddTagToTheme(int themeId, int tagId)
        {
            return _tagRepo.ThemeTagAdd( themeId,  tagId );
        }

        public int RemoveTagFromTheme(int themeId, int tagId)
        {
            return _tagRepo.ThemeTagDelete( themeId,  tagId );
        }

        public int DeleteTheme(int id)  
        {
            var isDeleted = true;       
            return _courseRepo.DeleteOrRecoverTheme(id, isDeleted);
        }
        public int RecoverTheme(int id)
        {
            var isDeleted = false;
            return _courseRepo.DeleteOrRecoverTheme(id, isDeleted);
        }
        public List<ThemeDto> GetUncoveredThemesByGroupId(int id)
        {
            return _courseRepo.GetUncoveredThemesByGroupId(id);
        }

        public int AddUpdateCourseProgram(int courseId, List<CourseThemeDto> program)
        {
            _courseRepo.DeleteCourse_Program(courseId);
            program.ForEach(item => item.Course = new CourseDto() { Id = courseId });
            return _courseRepo.AddCourse_Program(program);
        }
        
    }
}

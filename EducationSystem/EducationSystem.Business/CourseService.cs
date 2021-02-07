using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
   public class CourseService
    {
        private CourseRepository _courseRepo;
        private TagRepository _tagRepo;
        private LessonRepository _lessonRepo;
        private HomeworkRepository _homeworkRepo;

        public CourseService()
        {
            _courseRepo = new CourseRepository();
            _tagRepo = new TagRepository();
            _homeworkRepo = new HomeworkRepository();
            _lessonRepo = new LessonRepository();
        }

        public List<CourseDto> GetCourses()
        { 
            return _courseRepo.GetCourses();
        }

        public CourseDto GetCourseById(int id)
        {
            return _courseRepo.GetCourseById(id);
        }

        public int UpdateCourse (CourseDto course)
        {
            int index=_courseRepo.UpdateCourse(course);
            if (index <= 0)
                return -1;
            if (course.Themes != null && course.Themes.Count > 0)
            {
                foreach (var theme in course.Themes)
                {
                    if (_courseRepo.AddCourse_Theme(index, theme.Id) <= 0) //если такая запись уже имеется??
                    {
                        //метод для удаления тем из списка курса        
                        return -2;
                    }
                }
            }
            return 0;
        }
        public int AddCourse(CourseDto course)
        {
            int index = _courseRepo.AddCourse(course);
            if (index <= 0)
                return -1;
            if(course.Themes!=null && course.Themes.Count>0)
            {
                foreach(var theme in course.Themes)
                {
                    if (_courseRepo.AddCourse_Theme(index, theme.Id) <= 0)
                    {
                        //метод для удаления тем из списка курса
                        return -2;
                    }
                }
            }
            return index;
        }

        public int RemoveCourse(int id)
        {
           return _courseRepo.DeleteCourse(id); 
        }

        public int AddThemeToCourse(int courseId, int themeId)
        {
            return _courseRepo.AddCourse_Theme(courseId, themeId);
        }

        public int RemoveThemeFromCourse(int courseId, int themeId)
        {
           return _courseRepo.DeleteCourse_Theme(courseId, themeId);
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
            int index = _courseRepo.AddTheme(theme.Name);
            if (index <= 0)
                return -1;
            if (theme.Tags != null && theme.Tags.Count > 0)
            {
                foreach (var tag in theme.Tags)
                {
                    if (_tagRepo.ThemeTagAdd(new ThemeTagDto { ThemeId = index, TagId = tag.Id }) <= 0)
                    {
                        //метод для удаления тэгов из списка темы
                        return -2;
                    }
                }
            }
            return index;
        }

        public int AddTagToTheme(int themeId, int tagId)
        {
            return _tagRepo.ThemeTagAdd(new ThemeTagDto { ThemeId = themeId, TagId = tagId });
        }

        public int RemoveTagFromTheme(int themeId, int tagId)
        {
            return _tagRepo.ThemeTagDelete(themeId, tagId );
        }
        public int DeleteTheme(int id)
        {
           return _courseRepo.DeleteTheme(id);
        }
    }
}

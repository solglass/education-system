﻿using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class CourseService : ICourseService
    {
        private ICourseRepository _courseRepo;
        private ITagRepository _tagRepo;
        private ILessonRepository _lessonRepo;
        private IHomeworkRepository _homeworkRepo;

        public CourseService
            (
            ICourseRepository courseRepository, 
            ITagRepository tagRepository, 
            ILessonRepository lessonRepository, 
            IHomeworkRepository homeworkRepository
            )
        {
            _courseRepo = courseRepository;
            _tagRepo =  tagRepository;
            _homeworkRepo = homeworkRepository;
            _lessonRepo = lessonRepository;
        }

        public List<CourseDto> GetCourses()
        {
            return _courseRepo.GetCourses();
        }

        public CourseDto GetCourseById(int id)
        {
            return _courseRepo.GetCourseById(id);
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
            if (course.Themes != null && course.Themes.Count > 0)
            {
                foreach (var theme in course.Themes)
                {
                    if (_courseRepo.AddCourse_Theme(index, theme.Id) <= 0)
                    {
                        RemoveAllCourseThemesbyCourseId(index, course.Themes);
                        return -2 - index;
                    }
                }
            }
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
            int index = _courseRepo.AddTheme(theme);
            if (index <= 0)
                return -1;
            if (theme.Tags != null && theme.Tags.Count > 0)
            {
                foreach (var tag in theme.Tags)
                {
                    if (_tagRepo.ThemeTagAdd(new ThemeTagDto { ThemeId = index, TagId = tag.Id }) <= 0)
                    {

                        return -2 - index;
                    }
                }
            }
            return index;
        }

        public int AddTagToTheme(int themeId, int tagId)
        {
            return _tagRepo.ThemeTagAdd(new ThemeTagDto { ThemeId = themeId, TagId = tagId });
        }



        public int DeleteTheme(int id)  //  should remove all connections many-to-many
        {         
            return _courseRepo.DeleteTheme(id);
        }

        public List<ThemeDto> GetUncoveredThemesByGroupId(int id)
        {
            return _courseRepo.GetUncoveredThemesByGroupId(id);
        }
        private void RemoveAllCourseThemesbyCourseId(int courseId, List<ThemeDto> themes)
        {
            foreach (var theme in themes)
            {
                _courseRepo.DeleteCourse_Theme(courseId, theme.Id);
            }
        }

    }
}

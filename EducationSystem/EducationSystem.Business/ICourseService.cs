using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ICourseService
    {
        int AddCourse(CourseDto course);
        int AddTagToTheme(int themeId, int tagId);
        int AddTheme(ThemeDto theme);
        int AddThemeToCourse(int courseId, int themeId);
        int DeleteTheme(int id);
        CourseDto GetCourseById(int id);
        List<CourseDto> GetCourses();
        ThemeDto GetThemeById(int id);
        List<ThemeDto> GetThemes();
        List<ThemeDto> GetUncoveredThemesByGroupId(int id);
        int RemoveThemeFromCourse(int courseId, int themeId);
        int UpdateCourse(CourseDto course);
        int DeleteCourse(int id);
        int RecoverCourse(int id);
    }
}
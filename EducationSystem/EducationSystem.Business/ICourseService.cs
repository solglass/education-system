using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ICourseService
    {
        int AddCourse(CourseDto course);
        int AddTagToTheme(int themeId, int tagId);
        int RemoveTagFromTheme(int themeId, int tagId);
        int AddTheme(ThemeDto theme);
       // int AddThemeToCourse(int courseId, int themeId);
        int AddMaterialToCourse(int courseId, int materialId);
        int DeleteTheme(int id);
        int RecoverTheme(int id);
        CourseDto GetCourseById(int id);
        List<CourseDto> GetCourses();
        ThemeDto GetThemeById(int id);
        List<ThemeDto> GetThemes();
        List<ThemeDto> GetUncoveredThemesByGroupId(int id);
       // int RemoveThemeFromCourse(int courseId, int themeId);
        int DeleteMaterialFromCourse(int courseId, int materialId);
        int UpdateCourse(CourseDto course);
        int DeleteCourse(int id);
        int RecoverCourse(int id);
    }
}
using Dapper;
using EducationSystem.Data.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class CourseRepository : BaseRepository
    {

        public CourseRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<CourseDto> GetCourses()
        {
            var courseDictionary = new Dictionary<int, CourseDto>();
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var courses = _connection
                .Query<CourseDto, ThemeDto, CourseDto>(
                    "dbo.Course_SelectAll",
                    (course, theme) =>
                    {
                        if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                        }
                        
                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            courseEntry.Themes.Add(themeEntry);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return courseEntry;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return courses;
        }

        public CourseDto GetCourseById(int id)
        {
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var courseEntry = new CourseDto();
            var course = _connection
                .Query<CourseDto, ThemeDto,CourseDto>(
                    "dbo.Course_SelectById",
                    (course, theme) =>
                    {
                        if (courseEntry.Id == 0)
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                        }
                        if(theme!=null && !themeDictionary.TryGetValue(theme.Id, out  ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            courseEntry.Themes.Add(theme);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        return courseEntry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return course;
        }

        public int AddCourse(CourseDto course)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Add",
                new
                {
                    name = course.Name,
                    description = course.Description,
                    duration = course.Duration
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateCourse( CourseDto course)
        {
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {
                    id = course.Id,
                    name = course.Name,
                    description = course.Description,
                    duration = course.Duration,
                    isDeleted = course.IsDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteCourse(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteCourseHard (int id)
        {
            var result = _connection
                .Execute("dbo.Course_CompleteDelete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<ThemeDto> GetThemes()
        {
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var tagDictionary = new Dictionary<int, TagDto>();

            var themes = _connection
                .Query<ThemeDto, TagDto, ThemeDto>("dbo.Theme_SelectAll",                (theme, tag) =>                {

                    if (!themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                    {
                        themeEntry = theme;
                        themeEntry.Tags = new List<TagDto>();
                        themeDictionary.Add(themeEntry.Id, themeEntry);
                    }

                    if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                    {
                        tagEntry = tag;
                        themeEntry.Tags.Add(tagEntry);
                        tagDictionary.Add(tagEntry.Id, tagEntry);
                    }
                    return themeEntry;
                },                splitOn: "Id",                commandType: System.Data.CommandType.StoredProcedure)                .Distinct()
                .ToList();
            return themes;
        }


        public ThemeDto GetThemeById(int id)
        {
            var tagDictionary = new Dictionary<int, TagDto>();
            var themeEntry = new ThemeDto();

            var theme = _connection
                .Query<ThemeDto,TagDto, ThemeDto>(
                "dbo.Theme_SelectById",
                (theme, tag) =>
                {
                    if (themeEntry.Id == 0)
                    {
                        themeEntry = theme;
                        themeEntry.Tags = new List<TagDto>();
                    }
                    if (tag != null && !tagDictionary.TryGetValue(tag.Id, out TagDto tagEntry))
                    {
                        tagEntry = tag;
                        themeEntry.Tags.Add(tag);
                        tagDictionary.Add(tagEntry.Id, tagEntry);
                    }
                    return themeEntry;
                },
                new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return theme;
        }
        public int AddTheme(ThemeDto theme)
        {
            int result = _connection
                .QuerySingle<int>("dbo.Theme_Add",
                new
                {
                    theme.Name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int UpdateTheme(ThemeDto theme)
        {
            var result = _connection
                .Execute("dbo.Theme_Update",
                new
                {
                    theme.Id,
                    theme.Name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteTheme(int id)
        {
            var result = _connection
                .Execute("dbo.Theme_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }






        public int AddCourse_Theme(int courseId, int themeId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Theme_Add",
                new
                {
                    courseId,
                    themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteCourse_Theme(int courseId, int themeId)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_DeleteByCourseIdAndThemeId",
                new
                {
                    courseId,
                    themeId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }


        public int AddCourse_Theme_Material(int courseThemeID, int materialID)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Material_Add",
                new
                {
                    courseThemeID,
                    materialID
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteCourse_Theme_Material(int id)
        {
            var result = _connection
                .Execute("dbo.Course_Theme_Material_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<ThemeDto> GetUncoveredThemesByGroupId(int id)
        {
            var themes = _connection.
                Query<ThemeDto>("dbo.Theme_SelectAllUncoveredByGroupId",
                new { groupId = id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return themes;
        }
        public List<ThemeDto> GetThemesByCourseId(int id)
        {
            var themes = _connection.
                Query<ThemeDto>("dbo.Theme_SelectAllByCourseId",
                new { courseId = id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return themes;
        }

        public List<Course_Theme_MaterialDto> GetCourseThemeMaterialByThemeId(int id)
        {
            var result = _connection.
               Query<Course_Theme_MaterialDto>("dbo.Course_Theme_Material_SelectAllByThemeId",
               new { id }, commandType: System.Data.CommandType.StoredProcedure)
               .Distinct()
               .ToList();
            return result;
        }
        public List<Course_ThemeDto> GetCourseThemeByThemeId(int id)
        {
            var result = _connection.
               Query<Course_ThemeDto>("dbo.Course_Theme_SelectAllByThemeId",
               new { id }, commandType: System.Data.CommandType.StoredProcedure)
               .Distinct()
               .ToList();
            return result;
        }
    }
}

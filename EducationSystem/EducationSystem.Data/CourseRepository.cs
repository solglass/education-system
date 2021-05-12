using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {

        public CourseRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<CourseDto> GetCourses()
        {
            var courseDictionary = new Dictionary<int, CourseDto>();
            Dictionary<int, MaterialDto> materialDictionary = new Dictionary<int, MaterialDto>();
            var courses = _connection
                .Query<CourseDto,  MaterialDto, CourseDto>(
                    "dbo.Course_SelectAll",
                    (course, material) =>
                    {
                        if (!courseDictionary.TryGetValue(course.Id, out CourseDto courseEntry))
                        {
                            courseEntry = course;
                            courseEntry.Materials = new List<MaterialDto>();
                            courseDictionary.Add(courseEntry.Id, courseEntry);
                            materialDictionary = new Dictionary<int, MaterialDto>();
                        }
                        if (material != null && !materialDictionary.TryGetValue(material.Id, out MaterialDto materialEntry))
                        {
                            materialEntry = material;
                            courseEntry.Materials.Add(materialEntry);
                            materialDictionary.Add(materialEntry.Id, materialEntry);
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
            var materialDictionary = new Dictionary<int, MaterialDto>();
            var courseEntry = new CourseDto();
            var course = _connection
                .Query<CourseDto, MaterialDto, CourseDto>(
                    "dbo.Course_SelectById",
                    (course, material) =>
                    {
                        if (courseEntry.Id == 0)
                        {
                            courseEntry = course;
                            courseEntry.Materials = new List<MaterialDto>();
                        }
                        if (material != null && !materialDictionary.TryGetValue(material.Id, out MaterialDto materialEntry))
                        {
                            materialEntry = material;
                            courseEntry.Materials.Add(material);
                            materialDictionary.Add(materialEntry.Id, materialEntry);
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

        public int UpdateCourse(CourseDto course)
        {
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {
                    id = course.Id,
                    name = course.Name,
                    description = course.Description,
                    duration = course.Duration
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteOrRecoverCourse(int id, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Course_DeleteOrRecover",
                new
                {
                    id,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int HardDeleteCourse(int id)
        {
            var result = _connection
                .Execute("dbo.Course_HardDelete",
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
                .Query<ThemeDto, TagDto, ThemeDto>("dbo.Theme_SelectAll",
                (theme, tag) =>
                {

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
                },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return themes;
        }


        public ThemeDto GetThemeById(int id)
        {
            var tagDictionary = new Dictionary<int, TagDto>();
            var themeEntry = new ThemeDto();

            var theme = _connection
                .Query<ThemeDto, TagDto, ThemeDto>(
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
        public int DeleteOrRecoverTheme(int id, bool isDeleted)
        {
            var result = _connection
                .Execute("dbo.Theme_DeleteOrRecover",
                new
                {
                    id,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int AddCourse_Material(int courseId, int materialId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Course_Material_Add",
                new
                {
                    courseId,
                    materialId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteCourse_Material(int courseId, int materialId)
        {
            var result = _connection
                .Execute("dbo.Course_Material_Delete",
                new
                {
                    courseId,
                    materialId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<ThemeDto> GetUncoveredThemesByGroupId(int id)
        {
            var themes = _connection.
                Query<ThemeDto>(
                "dbo.Theme_SelectAllUncoveredByGroupId",
                new { groupId = id }, 
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return themes;
        }
        public int HardDeleteTheme(int id)
        {
            var result = _connection
                .Execute("dbo.Theme_HardDelete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }



        public int DeleteCourse_Program(int courseId)
        {
            var result = _connection
                .Execute("dbo.Course_Program_Delete",
                new
                {
                    courseId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<OrderedThemeDto> GetCourse_Program(int courseId)
        {
            var result = _connection.Query<OrderedThemeDto>(
                "dbo.Course_Program_SelectByCourseId",
                new
                {
                    id=courseId
                },
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int AddCourse_Program(int courseId,List<OrderedThemeDto> courseThemes)
        {
            var result = _connection
                .Execute("dbo.Course_Program_Update",
                courseThemes.Select(obj=> new
                {
                    courseId=courseId,
                    themeId=obj.Id,
                    order=obj.Order
                }).ToArray(),
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

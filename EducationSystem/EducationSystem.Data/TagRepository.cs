using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class TagRepository : BaseRepository, ITagRepository
    {

        public TagRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<TagDto> GetTags()
        {

            var result = _connection
                .Query<TagDto>("dbo.Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public TagDto GetTagById(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int TagUpdate(TagDto tag)
        {
            var result = _connection
                .Execute("dbo.Tag_Update", new { tag.Id, tag.Name },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int TagDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Tag_Delete", new { Id },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
        public int TagAdd(TagDto tag)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Tag_Add", new { tag.Name },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public MaterialTagDto GetMaterialTagById(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<MaterialTagDto>("dbo.Material_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int MaterialTagDelete(int materialId, int tagId)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Delete", new { materialId, tagId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int MaterialTagAdd(MaterialTagDto Tag)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Material_Tag_Add", new { Tag.TagId, Tag.MaterialId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<ThemeTagDto> GetThemeTagById(int Id)
        {
            var result = _connection
                .Query<ThemeTagDto>("dbo.Theme_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
      
        public int ThemeTagDelete(int themeId, int tagId)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Delete", new { themeId, tagId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int ThemeTagAdd(int themeId, int tagId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Theme_Tag_Add", new { tagId, themeId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<HomeworkTagDto> GetHomeworkTagById(int Id)
        {
            var result = _connection
                .Query<HomeworkTagDto>("dbo.Homework_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }            
    }

}

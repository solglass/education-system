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
                .Query<TagDto>("dbo.Tag_SelectAll", 
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public TagDto GetTagById(int id)
        {
            var result = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_SelectById",
                new 
                { 
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        
        public int TagDelete(int id)
        {
            var result = _connection
                .Execute("dbo.Tag_Delete",
                new
                {
                    id 
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
        public int TagAdd(TagDto tag)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Tag_Add",
                new
                { 
                    tag.Name
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int MaterialTagDelete(int materialId, int tagId)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Delete",
                new 
                { 
                    materialId, 
                    tagId 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int MaterialTagAdd(int materialId, int tagId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Material_Tag_Add",
                new 
                { 
                    tagId,
                    materialId
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

       
       
        public int ThemeTagDelete(int themeId, int tagId)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Delete",
                new
                {
                    themeId, 
                    tagId 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int ThemeTagAdd(int themeId, int tagId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Theme_Tag_Add",
                new
                { 
                    tagId,
                    themeId 
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

            
    }

}

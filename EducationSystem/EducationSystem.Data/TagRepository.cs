using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class TagRepository : BaseRepository
    {

        public TagRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<TagDto> GetTags()
        {
           
            //var result = _connection
            //    .Query<TagDto>("dbo.Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
            //    .ToList();
            //return result;
        }
        public List<TagDto> GetTagById(int Id)
        {
            var result = _connection
                .Query<TagDto>("dbo.Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int TagUpdate(int Id, string Name)
        {
            var result = _connection
                .Execute("dbo.Tag_Update", new { Id, Name },
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
                .Execute("dbo.Tag_Add", new { tag.Name },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<MaterialTagDto> GetMaterial_Tag()
        {
            var result = _connection
                .Query<MaterialTagDto>("dbo.Material_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<MaterialTagDto> GetTag_ById(int Id)
        {
            var result = _connection
                .Query<MaterialTagDto>("dbo.Material_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Material_TagUpdate(MaterialTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Update", new { Tag.Id, Tag.TagId, Tag.MaterialId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
        public int Material_TagDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int Material_TagAdd(MaterialTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Add", new { Tag.TagId, Tag.MaterialId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<ThemeTagDto> GetTheme_Tag()
        {
            var result = _connection
                .Query<ThemeTagDto>("dbo.Theme_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<ThemeTagDto> GetTheme_TagById(int Id)
        {
            var result = _connection
                .Query<ThemeTagDto>("dbo.Theme_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Theme_TagUpdate(ThemeTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Update", new { Tag.Id, Tag.TagId, Tag.ThemeId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int Theme_TagDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int Theme_TagAdd(ThemeTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Add", new { Tag.TagId, Tag.ThemeId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<HomeworkTagDto> GetHomework_Tag()
        {
            var result = _connection
                .Query<HomeworkTagDto>("dbo.Homework_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<HomeworkTagDto> GetHomework_TagById(int Id)
        {
            var result = _connection
                .Query<HomeworkTagDto>("dbo.Homework_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Homework_TagUpdate(HomeworkTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Update", new { Tag.Id, Tag.TagId, Tag.HomeworkId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int Homework_TagDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int Homework_TagAdd(HomeworkTagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Add", new { Tag.TagId, Tag.HomeworkId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

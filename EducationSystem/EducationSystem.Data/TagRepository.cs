using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class TagRepository : IRepository
    {

        public TagRepository()
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
        public void TagDelete(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_Delete", new { Id },
                commandType: System.Data.CommandType.StoredProcedure);

        }
        public int TagAdd(TagDto tag)
        {
            var result = _connection
                .Execute("dbo.Tag_Add", new { tag.Name },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<Material_TagDto> GetMaterial_Tag()
        {
            var result = _connection
                .Query<Material_TagDto>("dbo.Material_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<Material_TagDto> GetTag_ById(int Id)
        {
            var result = _connection
                .Query<Material_TagDto>("dbo.Material_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Material_TagUpdate(Material_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Update", new { Tag.Id, Tag.TagId, Tag.MaterialId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
        public void Material_TagDelete(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<Material_TagDto>("dbo.Material_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public int Material_TagAdd(Material_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Material_Tag_Add", new { Tag.TagId, Tag.MaterialId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<Theme_TagDto> GetTheme_Tag()
        {
            var result = _connection
                .Query<Theme_TagDto>("dbo.Theme_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<Theme_TagDto> GetTheme_TagById(int Id)
        {
            var result = _connection
                .Query<Theme_TagDto>("dbo.Theme_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Theme_TagUpdate(Theme_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Update", new { Tag.Id, Tag.TagId, Tag.ThemeId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public void Theme_TagDelete(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<Theme_TagDto>("dbo.Theme_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public int Theme_TagAdd(Theme_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Theme_Tag_Add", new { Tag.TagId, Tag.ThemeId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<Homework_TagDto> GetHomework_Tag()
        {
            var result = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<Homework_TagDto> GetHomework_TagById(int Id)
        {
            var result = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectById", new { Id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int Homework_TagUpdate(Homework_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Update", new { Tag.Id, Tag.TagId, Tag.HomeworkId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public void Homework_TagDelete(int Id)
        {
            var result = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public int Homework_TagAdd(Homework_TagDto Tag)
        {
            var result = _connection
                .Execute("dbo.Homework_Tag_Add", new { Tag.TagId, Tag.HomeworkId }, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }
}

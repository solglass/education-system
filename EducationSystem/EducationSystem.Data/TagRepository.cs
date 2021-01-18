using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class TagRepository
    {

        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public TagRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<TagDto> GetTag()
        {
            var tags = _connection
                .Query<TagDto>("dbo.Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<TagDto> GetTagById(int id)
        {
            var tags = _connection
                .Query<TagDto>("dbo.Tag_SelectById",new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public void TagUpdate(int id,string name)
        {
            var tags = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_Update", new { id,name }, commandType: System.Data.CommandType.StoredProcedure);
            
        }
        public void TagDelete(int id)
        {
            var tags = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            
        }
        public TagDto TagAdd(TagDto Tag)
        {
            var tags = _connection
                .QuerySingleOrDefault<TagDto>("dbo.Tag_Add", new { name=Tag.Name}, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public List<Material_TagDto> GetMaterial_Tag()
        {
            var tags = _connection
                .Query<Material_TagDto>("dbo.Material_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<Material_TagDto> GetTag_ById(int id)
        {
            var tags = _connection
                .Query<Material_TagDto>("dbo.Material_Tag_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public Material_TagDto Material_TagUpdate(int id, int tagId, int materialId)
        {
            var tags = _connection
                .QuerySingleOrDefault<Material_TagDto>("dbo.Material_Tag_Update", new { id, tagId, materialId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public void Material_TagDelete(int id)
        {
            var tags = _connection
                .QuerySingleOrDefault<Material_TagDto>("dbo.Material_Tag_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public Material_TagDto Material_TagAdd(Material_TagDto Tag)
        {
            var tags = _connection
                .QuerySingleOrDefault<Material_TagDto>("dbo.Material_Tag_Add", new { tagId = Tag.TagId, MaterialId = Tag.Material_TagId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public List<Theme_TagDto> GetTheme_Tag()
        {
            var tags = _connection
                .Query<Theme_TagDto>("dbo.Theme_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<Theme_TagDto> GetTheme_TagById(int id)
        {
            var tags = _connection
                .Query<Theme_TagDto>("dbo.Theme_Tag_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public Theme_TagDto Theme_TagUpdate(int id, int tagId, int materialId)
        {
            var tags = _connection
                .QuerySingleOrDefault<Theme_TagDto>("dbo.Theme_Tag_Update", new { id, tagId, materialId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public void Theme_TagDelete(int id)
        {
            var tags = _connection
                .QuerySingleOrDefault<Theme_TagDto>("dbo.Theme_Tag_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public Theme_TagDto Theme_TagAdd(Theme_TagDto Tag)
        {
            var tags = _connection
                .QuerySingleOrDefault<Theme_TagDto>("dbo.Theme_Tag_Add", new { tagId = Tag.TagId, ThemeId = Tag.Theme_TagId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public List<Homework_TagDto> GetHomework_Tag()
        {
            var tags = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public List<Homework_TagDto> GetHomework_TagById(int id)
        {
            var tags = _connection
                .Query<Homework_TagDto>("dbo.Homework_Tag_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return tags;
        }
        public Homework_TagDto Homework_TagUpdate(int id, int tagId, int materialId)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Update", new { id, tagId, materialId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
        public void Homework_TagDelete(int id)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);

        }
        public Homework_TagDto Homework_TagAdd(Homework_TagDto Tag)
        {
            var tags = _connection
                .QuerySingleOrDefault<Homework_TagDto>("dbo.Homework_Tag_Add", new { tagId = Tag.TagId, HomeworkId = Tag.Homework_TagId }, commandType: System.Data.CommandType.StoredProcedure);
            return tags;
        }
    }
}

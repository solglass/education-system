using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class MaterialRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public MaterialRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<MaterialDto> GetMaterials()
        {
            var materials = _connection
                .Query<MaterialDto>("dbo.Material_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return materials;
        }

        public MaterialDto GetMaterialsById(int id)
        {
            var materials = _connection
                .Query<MaterialDto>("dbo.Material_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return materials;
        }

        public void GetMaterialsAdd(string Link, string Description, bool IsDeleted)
        {
            var materials = _connection
                .Execute("dbo.Material_Add", new { Link, Description, IsDeleted }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetMaterialsUpdate(int Id, string Link, string Description)
        {
            var materials = _connection
                .Execute("dbo.Material_Update", new { Id, Link, Description }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetMaterialsDelete(int Id)
        {
            var materials = _connection
                .Execute("dbo.Material_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}

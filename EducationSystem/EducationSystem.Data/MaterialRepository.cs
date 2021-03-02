using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class MaterialRepository : BaseRepository, IMaterialRepository
    {
        public MaterialRepository(IOptions<AppSettingsConfig> options) : base(options)
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

        public List<MaterialDto> GetMaterialsByTagId(int id)
        {
            return _connection
                    .Query<MaterialDto>("dbo.Material_SelectByTagId",
                    new { tagId = id },
                    commandType: System.Data.CommandType.StoredProcedure)
                    .ToList();
        }

        public List<MaterialDto> GetMaterialsByGroupId(int id)
        {
            return _connection
                    .Query<MaterialDto>("dbo.Material_SelectByGroupId",
                    new { groupId = id },
                    commandType: System.Data.CommandType.StoredProcedure)
                    .ToList();
        }

        public MaterialDto GetMaterialById(int id)
        {
            var material = _connection
                .Query<MaterialDto>("dbo.Material_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return material;
        }

        public int AddMaterial(MaterialDto material)
        {
            int rows = _connection
                .Execute("dbo.Material_Add",
                new
                {
                    material.Link,
                    material.Description,
                    material.IsDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return rows;
        }

        public int UpdateMaterial(MaterialDto material)
        {
            int rows = _connection
                .Execute("dbo.Material_Update",
                new
                {
                    material.Id,
                    material.Link,
                    material.Description
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return rows;
        }

        public int DeleteOrRecoverMaterial(int id, bool isDeleted)
        {
            int rows = _connection
                .Execute("dbo.Material_DeleteOrRecover", 
                new
                { 
                    id,
                    isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return rows;
        }
       
        public int HardDeleteMaterial(int id)
        {
            int rows = _connection
                .Execute("dbo.Material_HardDelete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return rows;
        }
    }
}

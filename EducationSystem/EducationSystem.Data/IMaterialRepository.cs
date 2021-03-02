using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IMaterialRepository
    {
        int AddMaterial(MaterialDto material);
        MaterialDto GetMaterialById(int id);
        List<MaterialDto> GetMaterials();
        List<MaterialDto> GetMaterialsByGroupId(int id);
        List<MaterialDto> GetMaterialsByTagId(int id);
        int UpdateMaterial(MaterialDto material);
        int DeleteOrRecoverMaterial(int id, bool isDeleted);
    }
}
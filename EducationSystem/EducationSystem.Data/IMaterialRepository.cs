using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IMaterialRepository
    {
        int AddMaterial(MaterialDto material);
        int DeleteMaterialById(int id);
        MaterialDto GetMaterialById(int id);
        List<MaterialDto> GetMaterials();
        List<MaterialDto> GetMaterialsByGroupId(int id);
        List<MaterialDto> GetMaterialsByTagId(int id);
        int UpdateMaterial(int id, MaterialDto material);
    }
}
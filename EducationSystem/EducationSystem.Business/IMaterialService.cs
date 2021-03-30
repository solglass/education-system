using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IMaterialService
    {
        int AddMaterial(MaterialDto material);
        List<MaterialDto> GetMaterials();
        MaterialDto GetMaterialById(int id);
        List<MaterialDto> GetMaterialsByGroupId(int id);
        List<MaterialDto> GetMaterialsByTagId(int id);
        int DeleteMaterial(int id);
        int RecoverMaterial(int id);
        int UpdateMaterial(MaterialDto material);
    }
}
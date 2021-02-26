using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class MaterialService
    {
        private MaterialRepository _materialRepository;
        public MaterialService()
        {
            _materialRepository = new MaterialRepository();
        }
       
        public List<MaterialDto> GetMaterialsByTagId(int id) { return _materialRepository.GetMaterialsByTagId(id); }
        public List<MaterialDto> GetMaterialsByGroupId(int id) { return _materialRepository.GetMaterialsByGroupId(id); }

        public MaterialDto GetMaterialById(int id) { return _materialRepository.GetMaterialById(id); }
        public int AddMaterial(MaterialDto material) { return _materialRepository.AddMaterial(material); }
        public int UpdateMaterial(int id, MaterialDto material) { return _materialRepository.UpdateMaterial(id, material); }

        public int DeleteMaterial(int id) 
        {
            bool isDeleted = true;
            return _materialRepository.DeleteOrRecoverMaterial(id, isDeleted);
        }

        public int RecoverMaterial(int id)
        {
            bool isDeleted = false;
            return _materialRepository.DeleteOrRecoverMaterial(id, isDeleted);
        }

        public int HardDeleteMaterial(int id)
        {
            return _materialRepository.HardDeleteMaterial(id);
        }

    }
}

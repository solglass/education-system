using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class MaterialService : IMaterialService
    {
        private IMaterialRepository _materialRepository;
        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public List<MaterialDto> GetMaterialsByTagId(int id) { return _materialRepository.GetMaterialsByTagId(id); }
        public List<MaterialDto> GetMaterialsByGroupId(int id) { return _materialRepository.GetMaterialsByGroupId(id); }

        public MaterialDto GetMaterialById(int id) { return _materialRepository.GetMaterialById(id); }
        public int AddMaterial(MaterialDto material) { return _materialRepository.AddMaterial(material); }
        public int UpdateMaterial(int id, MaterialDto material) { return _materialRepository.UpdateMaterial(id, material); }
        public int DeleteMaterialById(int id) { return _materialRepository.DeleteMaterialById(id); }


    }
}

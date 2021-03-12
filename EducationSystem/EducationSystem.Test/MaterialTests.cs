using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class MaterialTests : BaseTest
    {
        private MaterialRepository _materialRepository;
        

        [OneTimeSetUp]
        public void MaterialOneTimeSetUp()
        {
            _materialRepository = new MaterialRepository(_options);
        }

        [TestCase(1)]
        public void MaterialAddPositiveTest(int caseId)
        {
            var dto = MaterialMock.GetMaterialMock(caseId);
            int id = _materialRepository.AddMaterial(dto);
            dto.Id = id;
            var actual = _materialRepository.GetMaterialById(id);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void MaterialUpdatePositiveTest(int caseId)
        {
            var dto = MaterialMock.GetMaterialMock(caseId);
            int id = _materialRepository.AddMaterial(dto);
            dto.Id = id;

            dto.Description = $"Description { caseId} update";

            var affectedRows = _materialRepository.UpdateMaterial(dto);
            var actual = _materialRepository.GetMaterialById(id);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }
    }
}

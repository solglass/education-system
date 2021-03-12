using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class MaterialTests : BaseTest
    {
        private MaterialRepository _materialRepository;
        private TagRepository _tagRepository;

        private List<int> _addedMaterialMockIds;
        private List<int> _addedTagMockIds;
        private List<int> _addedMaterialTagIds;

        [OneTimeSetUp]
        public void MaterialOneTimeSetUp()
        {
            _materialRepository = new MaterialRepository(_options);
            _tagRepository = new TagRepository(_options);

            _addedMaterialMockIds = new List<int>();
            _addedTagMockIds = new List<int>();
            _addedMaterialTagIds = new List<int>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void MaterialAddPositiveTest(int caseId)
        {
            var dto = MaterialMock.GetMaterialMock(caseId);
            int addedId = _materialRepository.AddMaterial(dto);
            dto.Id = addedId;
            _addedMaterialMockIds.Add(addedId);

            var actual = _materialRepository.GetMaterialById(addedId);

            Assert.AreEqual(dto, actual);
        }

        [TestCase(2)]
        public void MaterialUpdatePositiveTest(int caseId)
        {
            var dto = MaterialMock.GetMaterialMock(caseId);
            int addedId = _materialRepository.AddMaterial(dto);
            dto.Id = addedId;
            _addedMaterialMockIds.Add(addedId);

            dto.Description = $"Description { caseId} update";

            var affectedRows = _materialRepository.UpdateMaterial(dto);
            var actual = _materialRepository.GetMaterialById(addedId);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(2, false)]
        public void MaterialDeleteOrRecoverPositiveTest(int caseId, bool isDeleted)
        {
            var dto = MaterialMock.GetMaterialMock(caseId);
            int addedId = _materialRepository.AddMaterial(dto);
            dto.Id = addedId;
            _addedMaterialMockIds.Add(addedId);

            dto.IsDeleted = isDeleted;

            var affectedRows = _materialRepository.DeleteOrRecoverMaterial(dto.Id, dto.IsDeleted);
            var actual = _materialRepository.GetMaterialById(addedId);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(5)]
        public void MaterialsGetByTagIdPositiveTest(int caseId)
        {
            for (int i = 0; i < 4; i++)
            {
                var dto = MaterialMock.GetMaterialMock(i);
                int addedId = _materialRepository.AddMaterial(dto);
                dto.Id = addedId;
                _addedMaterialMockIds.Add(addedId);
            }

            var dtoTag = TagMock.GetTagMock(caseId);
            int addedTagId = _tagRepository.TagAdd(dtoTag);
            dtoTag.Id = addedTagId;
            _addedTagMockIds.Add(addedTagId);

            for (int i = 0; i < 4; i++)
            {
                var dto = MaterialMock.GetMaterialMock(i);
                int addedId = _materialRepository.AddMaterial(dto);
                dto.Id = addedId;
                _addedMaterialMockIds.Add(addedId);
            }

        }

        public void AddMaterials()
        {

        }

        [OneTimeTearDown]
        public void MaterialOneTimeTearDown()
        {
            _addedMaterialMockIds.ForEach(id =>
            {
                _materialRepository.HardDeleteMaterial(id);
            });
        }
    }
}

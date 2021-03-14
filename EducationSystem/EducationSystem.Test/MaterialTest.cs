using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class MaterialTest : BaseTest
    {
        private MaterialRepository _materialRepository;
        private TagRepository _tagRepository;
        private GroupRepository _groupRepository;

        private List<int> _addedMaterialMockIds;
        private List<int> _addedTagMockIds;
        private List<int> _addedGroupIds;
        private List<(int, int)> _addedMaterialTagIds;
        private List<(int, int)> _addedMaterialGroupIds;

        [OneTimeSetUp]
        public void MaterialOneTimeSetUp()
        {
            _materialRepository = new MaterialRepository(_options);
            _tagRepository = new TagRepository(_options);
            _groupRepository = new GroupRepository(_options);

            _addedMaterialMockIds = new List<int>();
            _addedTagMockIds = new List<int>();
            _addedGroupIds = new List<int>();
            _addedMaterialTagIds = new List<(int, int)>();
            _addedMaterialGroupIds = new List<(int, int)>();
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
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

        [TestCase(100)]
        public void MaterialsGetByTagIdPositiveTest(int caseId)
        {
            List<MaterialDto> expected = new List<MaterialDto>();
            for (int i = 0; i < 8; i+=2)
            {
                var dto = MaterialMock.GetMaterialMock(i);
                int addedId = _materialRepository.AddMaterial(dto);
                dto.Id = addedId;
                expected.Add(dto);
                _addedMaterialMockIds.Add(addedId);
            }

            var dtoTag = TagMock.GetTagMock(caseId);
            int addedTagId = _tagRepository.TagAdd(dtoTag);
            dtoTag.Id = addedTagId;
            _addedTagMockIds.Add(addedTagId);

            for (int i = 0; i < 4; i++)
            {
                _tagRepository.MaterialTagAdd(expected[i].Id, addedTagId);
                _addedMaterialTagIds.Add((expected[i].Id, addedTagId));
            }

            var actual = _materialRepository.GetMaterialsByTagId(addedTagId);

            Assert.AreEqual(expected, actual);

        }

        [TestCase(888)]
        public void MaterialsGetByGroupIdPositiveTest(int groupId)
        {
            List<MaterialDto> expected = new List<MaterialDto>();
            for (int i = 0; i < 8; i += 2)
            {
                var dto = MaterialMock.GetMaterialMock(i);
                int addedId = _materialRepository.AddMaterial(dto);
                dto.Id = addedId;
                expected.Add(dto);
                _addedMaterialMockIds.Add(addedId);
            }

            var dtoCourse = CourseMockGetter.GetCourseDtoMock(1);
            var dtoGroup = GroupMockGetter.GetGroupDtoMock(1);
            dtoGroup.Course = dtoCourse;
            int addedGroupId = _groupRepository.AddGroup(dtoGroup);
            dtoGroup.Id = addedGroupId;
            _addedGroupIds.Add(addedGroupId);

            for (int i = 0; i < 4; i++)
            {
                _groupRepository.AddGroup_Material(addedGroupId, expected[i].Id);
                _addedMaterialGroupIds.Add((addedGroupId, expected[i].Id));
            }

            var actual = _materialRepository.GetMaterialsByGroupId(addedGroupId);

            CollectionAssert.AreEqual(expected, actual);

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
            _addedTagMockIds.ForEach(id =>
            {
                _tagRepository.TagDelete(id);
            });
            _addedGroupIds.ForEach(id =>
            {
                _groupRepository.HardDeleteGroup(id);
            });
            _addedMaterialTagIds.ForEach(id =>
            {
                _tagRepository.MaterialTagDelete(id.Item1, id.Item2);
            });
            _addedMaterialTagIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup_Material(id.Item1, id.Item2);
            });
        }
    }
}

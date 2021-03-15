using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

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

        [TestCase(1)]
        public void MaterialAddPositiveTest(int mockId)
        {
            var dto = AddMaterial(mockId);

            var actual = _materialRepository.GetMaterialById(dto.Id);

            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void MaterialUpdatePositiveTest(int mockId)
        {
            var dto = AddMaterial(mockId);

            dto.Description = $"Description update";
            dto.Link = $"Link update";

            var affectedRows = _materialRepository.UpdateMaterial(dto);
            var actual = _materialRepository.GetMaterialById(dto.Id);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        public void MaterialDeleteOrRecoverPositiveTest(int mockId, bool isDeleted)
        {
            var dto = AddMaterial(mockId);

            dto.IsDeleted = isDeleted;

            var affectedRows = _materialRepository.DeleteOrRecoverMaterial(dto.Id, dto.IsDeleted);
            var actual = _materialRepository.GetMaterialById(dto.Id);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, 4)]
        public void MaterialsGetByTagIdPositiveTest(int mockId, int amountRelations)
        {
            var dtoTag = AddTag(mockId);

            List<MaterialDto> expected = new List<MaterialDto>();
            for (int i = 0; i < amountRelations; i++)
            {
                var dto = AddMaterial(mockId);
                expected.Add(dto);

                _tagRepository.MaterialTagAdd(expected[i].Id, dtoTag.Id);
                _addedMaterialTagIds.Add((expected[i].Id, dtoTag.Id));
            }

            var actual = _materialRepository.GetMaterialsByTagId(dtoTag.Id);

            Assert.AreEqual(expected, actual);

        }

        [TestCase(1, 4)]
        public void MaterialsGetByGroupIdPositiveTest(int mockId, int amountRelations)
        {
            var dtoGroup = AddGroup(mockId);

            List<MaterialDto> expected = new List<MaterialDto>();
            for (int i = 0; i < amountRelations; i ++)
            {
                var dto = AddMaterial(mockId);
                expected.Add(dto);

                _groupRepository.AddGroup_Material(dtoGroup.Id, expected[i].Id);
                _addedMaterialGroupIds.Add((dtoGroup.Id, expected[i].Id));
            }

            var actual = _materialRepository.GetMaterialsByGroupId(dtoGroup.Id);

            CollectionAssert.AreEqual(expected, actual);

        }

        public MaterialDto AddMaterial(int mockId)
        {
            var dto = MaterialMock.GetMaterialMock(mockId);
            int addedId = _materialRepository.AddMaterial(dto);
            dto.Id = addedId;
            _addedMaterialMockIds.Add(addedId);
            return dto;
        }

        public TagDto AddTag(int mockId)
        {
            var dtoTag = TagMock.GetTagMock(mockId);
            int addedTagId = _tagRepository.TagAdd(dtoTag);
            dtoTag.Id = addedTagId;
            _addedTagMockIds.Add(addedTagId);
            return dtoTag;
        }

        public GroupDto AddGroup(int mockId)
        {
            var dtoCourse = CourseMockGetter.GetCourseDtoMock(mockId);
            var dtoGroup = GroupMockGetter.GetGroupDtoMock(mockId);
            dtoGroup.Course = dtoCourse;
            int addedGroupId = _groupRepository.AddGroup(dtoGroup);
            dtoGroup.Id = addedGroupId;
            _addedGroupIds.Add(addedGroupId);
            return dtoGroup;
        }

        [OneTimeTearDown]
        public void MaterialOneTimeTearDown()
        {
            DeleteMaterials();
            DeleteTags();
            DeleteGroups();
            DeleteMaterialTags();
            DeleteMaterialGroups();
        }

        public void DeleteMaterials()
        {
            _addedMaterialMockIds.ForEach(id =>
            {
                _materialRepository.HardDeleteMaterial(id);
            });
        }
        public void DeleteTags()
        {
            _addedTagMockIds.ForEach(id =>
            {
                _tagRepository.TagDelete(id);
            });
        }
        public void DeleteGroups()
        {
            _addedGroupIds.ForEach(id =>
            {
                _groupRepository.HardDeleteGroup(id);
            });
        }
        public void DeleteMaterialTags()
        {
            _addedMaterialTagIds.ForEach(id =>
            {
                _tagRepository.MaterialTagDelete( id.Item1, id.Item2);
            });
        }
        public void DeleteMaterialGroups()
        {
            _addedMaterialGroupIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup_Material(id.Item1, id.Item2);
            });
        }
    }
}

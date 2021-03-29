using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class MaterialTest : BaseTest
    {
        private IMaterialRepository _materialRepository;
        private ITagRepository _tagRepository;
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;

        private List<int> _addedMaterialMockIds;
        private List<int> _addedTagMockIds;
        private List<int> _addedGroupIds;
        private List<int> _addedCourseIds;
        private List<(int, int)> _addedMaterialTagIds;
        private List<(int, int)> _addedMaterialGroupIds;

        [OneTimeSetUp]
        public void MaterialOneTimeSetUp()
        {
            _materialRepository = new MaterialRepository(_options);
            _tagRepository = new TagRepository(_options);
            _groupRepository = new GroupRepository(_options);
            _courseRepository = new CourseRepository(_options);

            _addedMaterialMockIds = new List<int>();
            _addedTagMockIds = new List<int>();
            _addedGroupIds = new List<int>();
            _addedCourseIds = new List<int>();
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

                _tagRepository.MaterialTagAdd(dto.Id, dtoTag.Id);
                _addedMaterialTagIds.Add((dto.Id, dtoTag.Id));
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

                _groupRepository.AddGroup_Material(dtoGroup.Id, dto.Id);
                _addedMaterialGroupIds.Add((dtoGroup.Id, dto.Id));
            }

            var actual = _materialRepository.GetMaterialsByGroupId(dtoGroup.Id);

            CollectionAssert.AreEqual(expected, actual);

        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void AddMaterialTagPositiveTest(int[] mockIds)
        {
            //Given
            var materialDto = AddMaterial(1);

            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = AddTag(mockIds[i]);
                expected.Add(tagDto);

                _tagRepository.MaterialTagAdd(materialDto.Id, tagDto.Id);
                _addedMaterialTagIds.Add((materialDto.Id, tagDto.Id));
            }

            //When
            var actual = _materialRepository.GetMaterialById(materialDto.Id).Tags;

            ////Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 })]
        public void DeleteMaterialTagPositiveTest(int[] mockIds)
        {
            // Given
            var materialDto = AddMaterial(1);

            var expected = new List<TagDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = AddTag(mockIds[i]);
                expected.Add(tagDto);

                _tagRepository.MaterialTagAdd(materialDto.Id, tagDto.Id);
                _addedMaterialTagIds.Add((materialDto.Id, tagDto.Id));
            }

            var toDeleteIdList = new List<(int, int)>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var tagDto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                tagDto.Name += "toDelete";
                var addedTagId = _tagRepository.TagAdd(tagDto);
                _addedTagMockIds.Add(addedTagId);
                tagDto.Id = addedTagId;

                _tagRepository.MaterialTagAdd(materialDto.Id, tagDto.Id);
                toDeleteIdList.Add((materialDto.Id, tagDto.Id));
            }

            // When
            toDeleteIdList.ForEach(themeTag =>
            {
                _tagRepository.MaterialTagDelete(themeTag.Item1, themeTag.Item2);
            });

            var actual = _materialRepository.GetMaterialById(materialDto.Id).Tags;

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void AddMaterialTag_NotExistMaterial_NegativeTest()
        {
            //Given
            var tagDto = AddTag(1);
            //When
            try
            {
                _tagRepository.MaterialTagAdd(-1, tagDto.Id);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddMaterialTag_NotExistTag_NegativeTest()
        {
            //Given
            var materialDto = AddMaterial(1);
            //When
            try
            {
                _tagRepository.MaterialTagAdd(materialDto.Id, -1);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddMaterialTagDoubleNegativeTest()
        {
            try
            {
                    var materialDto = AddMaterial(1);
                    var tagDto = AddTag(1);

                    _tagRepository.MaterialTagAdd(materialDto.Id, tagDto.Id);
                    _addedMaterialTagIds.Add((materialDto.Id, tagDto.Id));
                    _tagRepository.MaterialTagAdd(materialDto.Id, tagDto.Id);
                    _addedMaterialTagIds.Add((materialDto.Id, tagDto.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        public MaterialDto AddMaterial(int mockId)
        {
            var dto = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(mockId).Clone();
            dto.Id = _materialRepository.AddMaterial(dto);
            Assert.Greater(dto.Id, 0);
            _addedMaterialMockIds.Add(dto.Id);
            return dto;
        }

        public TagDto AddTag(int mockId)
        {
            var dtoTag = (TagDto)TagMockGetter.GetTagDtoMock(mockId).Clone();
            dtoTag.Name += "Delete";
            dtoTag.Id = _tagRepository.TagAdd(dtoTag);
            Assert.Greater(dtoTag.Id, 0);
            _addedTagMockIds.Add(dtoTag.Id);
            return dtoTag;
        }

        public GroupDto AddGroup(int mockId)
        {
            var dtoGroup = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
            var dtoCourse = AddCourse(mockId);
            dtoGroup.Course = dtoCourse;
            dtoGroup.Id = _groupRepository.AddGroup(dtoGroup);
            Assert.Greater(dtoGroup.Id, 0);
            _addedGroupIds.Add(dtoGroup.Id);
            return dtoGroup;
        }

        public CourseDto AddCourse(int mockId)
        {
            var dtoCourse = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            dtoCourse.Id = _courseRepository.AddCourse(dtoCourse);
            Assert.Greater(dtoCourse.Id, 0);
            _addedCourseIds.Add(dtoCourse.Id);
            return dtoCourse;
        }

        [TearDown]
        public void MaterialOneTimeTearDown()
        {
            DeleteMaterialGroups();
            DeleteMaterialTags();
            DeleteMaterials();
            DeleteTags();
            DeleteGroups();
            DeleteCourses();
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
        public void DeleteCourses()
        {
            _addedCourseIds.ForEach(id =>
            {
                _courseRepository.HardDeleteCourse(id);
            });
        }
    }
}

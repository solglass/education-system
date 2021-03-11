using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
namespace EducationSystem.Data.Tests
{
    public class TagRepositoryTest : BaseTest
    {
        private TagRepository _repository;
        private List<int> _addedTagDtoIds;
        private List<int> _addedMaterialTagDtoIds;
        private List<int> _addedThemeTagDtoIds;
        private TagDto _tagDtoMock;

        [OneTimeSetUp]
        public void TagTestOneTimeSetUp()
        {
            _repository = new TagRepository(_options);
            _addedTagDtoIds = new List<int>();
            _addedMaterialTagDtoIds = new List<int>();
            _addedThemeTagDtoIds = new List<int>();

            _tagDtoMock = (TagDto)GetTagDtoMock(1);
            var addedTagDtoId = _repository.TagAdd(_tagDtoMock);
            _tagDtoMock.Id = addedTagDtoId;
            _addedTagDtoIds.Add(addedTagDtoId);
        }

        [TestCase(1)]
        public void TagAddPositiveTest(int mockId)
        {
            // Given
            var dto = (TagDto)GetTagDtoMock(mockId);
            var addedTagId = _repository.TagAdd(dto);
            Assert.Greater(addedTagId, 0);

            _addedTagDtoIds.Add(addedTagId);
            dto.Id = addedTagId;

            // When
            var actual = _repository.GetTagById(addedTagId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void TagUpdatePositiveTest(int mockId)
        {
            // Given
            var dto = (TagDto)GetTagDtoMock(mockId);
            var addedTagId = _repository.TagAdd(dto);
            _addedTagDtoIds.Add(addedTagId);
            dto.Id = addedTagId;
            dto.Name = "New value1";

            // When
            var affectedRowsCount = _repository.TagUpdate(dto);
            var actual = _repository.GetTagById(addedTagId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void TagDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (TagDto)GetTagDtoMock(mockId);
            var addedTagId = _repository.TagAdd(dto);
            _addedTagDtoIds.Add(addedTagId);

            // When
            var affectedRowsCount = _repository.TagDelete(addedTagId);
            var actual = _repository.GetTagById(addedTagId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);

        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void TagSelectAllPositiveTest(int[] mockIds)
        {
            // Given
            var expected = new List<TagDto>();
            for (var i = 0; i < mockIds.Length; i++)
            {
                var dto = (TagDto)GetTagDtoMock(mockIds[i]);
                var addedTagId = _repository.TagAdd(dto);
                _addedTagDtoIds.Add(addedTagId);
                dto.Id = addedTagId;
                expected.Add(dto);
            }

            // When
            var actual = _repository.GetTags();

            //Then
            for (var i = actual.Count - 1; i > actual.Count - expected.Count; i--)
            {
                var j = i - actual.Count + expected.Count;
                Assert.IsTrue(CustomCompare(expected[j], actual[i]));
            }
        }
        [TestCase(1)]
        public void MaterialTagAddPositiveTest(int mockId)
        {
            // Given
            var dto = (MaterialTagDto)GetMaterialTagDtoMock(mockId);
            var addedMaterialTagId = _repository.MaterialTagAdd(dto);
            Assert.Greater(addedMaterialTagId, 0);

            _addedMaterialTagDtoIds.Add(addedMaterialTagId);
            dto.Id = addedMaterialTagId;

            // When
            var actual = _repository.GetMaterialTagById(addedMaterialTagId);

            // Then
            Assert.AreEqual(dto, actual);
        }
        [TestCase(1)]
        public void MaterialTagDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (MaterialTagDto)GetMaterialTagDtoMock(mockId);
            var addedMaterialTagId = _repository.MaterialTagAdd(dto);
            _addedMaterialTagDtoIds.Add(addedMaterialTagId);

            // When
            var affectedRowsCount = _repository.MaterialTagDelete(addedMaterialTagId);
            var actual = _repository.GetMaterialTagById(addedMaterialTagId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);
        }
        [TestCase(1)]
        public void ThemeTagAddPositiveTest(int mockId)
        {
            // Given
            var dto = (ThemeTagDto)GetThemeTagDtoMock(mockId);
            var addedThemeTagId = _repository.ThemeTagAdd(dto);
            Assert.Greater(addedThemeTagId, 0);

            _addedThemeTagDtoIds.Add(addedThemeTagId);
            dto.Id = addedThemeTagId;

            // When
            var actual = _repository.GetThemeTagById(addedThemeTagId);

            // Then
            Assert.AreEqual(dto, actual);
        }
        [TestCase(1)]
        public void ThemeTagDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (ThemeTagDto)GetThemeTagDtoMock(mockId);
            var addedThemeTagId = _repository.ThemeTagAdd(dto);
            _addedThemeTagDtoIds.Add(addedThemeTagId);

            // When
            var affectedRowsCount = _repository.ThemeTagDelete(addedThemeTagId);
            var actual = _repository.GetThemeTagById(addedThemeTagId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);
        }

        [TestCase(1)]
        public void ThemeTagUpdatePositiveTest(int mockId)
        {
            // Given
            var dto = (ThemeTagDto)GetThemeTagDtoMock(mockId);
            var addedThemeTagId = _repository.ThemeTagAdd(dto);
            _addedThemeTagDtoIds.Add(addedThemeTagId);
            dto.Id = addedThemeTagId;
            dto.TagId = 1;
            dto.ThemeId =2;

            // When
            var affectedRowsCount = _repository.ThemeTagUpdate(dto);
            var actual = _repository.GetThemeTagById(addedThemeTagId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }
        [TestCase(1)]
        public void EntityAddNegativeTest(int mockId)
        {
            /*
             * 
             *  1. 0 в качестве результата выполнения хранимки на add
             *  2. избыточные данные в dto (и при этом, они заносятся в DB)
             * 
             */
            try
            {
                // Given
                var dto = (ThemeTagDto)GetThemeTagDtoMock(mockId);
                var addedThemeTagId = _repository.ThemeTagAdd(dto);
                Assert.Greater(addedThemeTagId, 0);

                _addedThemeTagDtoIds.Add(addedThemeTagId);
                dto.Id = addedThemeTagId;

                // When
                var actual = _repository.GetThemeTagById(addedThemeTagId);

                // Then
                Assert.AreEqual(dto, actual);

            }
            catch(Exception ex)
            {

            }
        }

        [TestCase(1)]
        public void EntityUpdateNegativeTest(int mockId)
        {
            /*
             * 
             *  1. изменились свойства, которые не болжны были меняться
             * 
             */
        }

        [TestCase(1)]
        public void EntityDeleteNegativeTest(int mockId)
        {
            /*
             * 
             *  1. удалилось безвозвратно при soft delete
             * 
             */
        }

        [TestCase(1)]
        public void EntitySelectAllNegativeTest(int mockId)
        {
            /*
             * 
             *  1. expected.Count > actual.Count (когда inner join обрезает выборку)
             *  2. expected.Count < actual.Count (когда строки дублируются)
             * 
             */
        }
        private bool? CustomCompare(TagDto tagDto1, TagDto tagDto2)
        {
            if (tagDto1.Id == tagDto2.Id && tagDto1.Name == tagDto2.Name)
            {
                return true;
            }
            return false;
        }
        
        [TearDown]
        public void TagTestTearDown()
        {
            _addedTagDtoIds.ForEach(id =>
            {
                _repository.TagDelete(id);
            });
            _addedMaterialTagDtoIds.ForEach(id =>
            {
                _repository.MaterialTagDelete(id);
            });
            _addedMaterialTagDtoIds.ForEach(id =>
            {
                _repository.ThemeTagDelete(id);
            });
           
        }
        private static TagDto GetTagDtoMock(int id)
        {
            
            switch (id)
            {
                case 1: return new TagDto { Name = "Odin" };
                case 2: return new TagDto { Name = "Dva" };
                case 3: return new TagDto { Name = "Tri" };
                default: return new TagDto();
            }
        }
        private static MaterialTagDto GetMaterialTagDtoMock(int id)
        {

            switch (id)
            {
                case 1: return new MaterialTagDto { TagId=1, MaterialId=2 };
                case 2: return new MaterialTagDto { TagId = 2, MaterialId = 3 };
                case 3: return new MaterialTagDto { TagId = 3, MaterialId = 4 };
                default:return new MaterialTagDto();
            }
        }
        private static ThemeTagDto GetThemeTagDtoMock(int id)
        {

            switch (id)
            {
                case 1: return new ThemeTagDto { ThemeId=1,TagId=2 };
                case 2: return new ThemeTagDto { ThemeId = 3, TagId = 4 };
                case 3: return new ThemeTagDto { ThemeId = 7, TagId = 6 };
                default: return new ThemeTagDto();
            }
        }
    }
}


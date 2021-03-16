using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using EducationSystem.Data.Tests.Mocks;

namespace EducationSystem.Data.Tests
{
    public class TagsTests:BaseTest
    {
       
        private TagRepository _tagRepo;
        private List<int> _tagIdList;
        private TagDto _tagDtoMock;

        [SetUp]
        public void TagTestsSetup()
        {
            _tagRepo = new TagRepository(_options);
            _tagIdList = new List<int>();
        }


        [TestCase(1)]
        public void TagAddTest(int dtoMockNumber)
        {
            TagDto expected = (TagDto)TagMockGetter.GetTagDtoMock(dtoMockNumber).Clone();
            var added = _tagRepo.TagAdd(expected);
            Assert.Greater(added, 0);

            _tagIdList.Add(added);
            expected.Id = added;

            if (_tagIdList.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                TagDto actual = _tagRepo.GetTagById(added);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1)]
        public void TagDeleteTest(int dtoMockNumber)
        {
            TagDto expected = (TagDto)TagMockGetter.GetTagDtoMock(dtoMockNumber);
            _tagIdList.Add(_tagRepo.TagAdd(expected));
            if (_tagIdList.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                int newId = _tagIdList[_tagIdList.Count - 1];
                _tagRepo.TagDelete(newId);
                TagDto actual = _tagRepo.GetTagById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }
        [TestCase(new int[] { 1, 2, 3 })]
        public void EntitySelectAllPositiveTest(int[] mockIds)
        {
            // Given
            var expected = new List<TagDto>();
            for (var i = 0; i < mockIds.Length; i++)
            {
                var dto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]);
                var addedEntityId = _tagRepo.TagAdd(dto);
                _tagIdList.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);
            }

            // When
            var actual = _tagRepo.GetTags();

            // Then
            // in simple case:
            //CollectionAssert.AreEqual(expected, actual);

            // in worst case
            for (var i = actual.Count - 1; i > actual.Count - expected.Count; i--)
            {
                var j = i - actual.Count + expected.Count;
                Assert.IsTrue(CustomCompare(expected[j], actual[i]));
            }
        }

        private bool? CustomCompare(TagDto tagDto1, TagDto tagDto2)
        {
            if(tagDto1.Name==tagDto2.Name&& tagDto1.Id == tagDto2.Id)
            {
                return true;
            }
            return false;
        }

        [TearDown]
        public void TagsTestsTearDown()
        {

            foreach (int elem in _tagIdList)
            {
                _tagRepo.TagDelete(elem);
            }
        }

    }
}
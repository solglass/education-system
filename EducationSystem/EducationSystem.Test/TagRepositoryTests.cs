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
        public void TagSelectAllPositiveTest(int[] mockIds)
        {
            // Given
            var expected = new List<TagDto>();
            for (var i = 0; i < mockIds.Length; i++)
            {
                var dto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedTagId = _tagRepo.TagAdd(dto);
                _tagIdList.Add(addedTagId);
                dto.Id = addedTagId;
                expected.Add(dto);
            }

            // When
            var actual = _tagRepo.GetTags();

            // Then
            // in simple case:
            CollectionAssert.AreEqual(expected, actual);

            // in worst case
            //for (var i = 0; i < actual.Count; i++)
            //{
            //    //Assert.IsTrue(CustomCompare(expected[i], actual[i]));
            //}
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
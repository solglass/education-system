using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using EducationSystem.Data.Tests.Mocks;

namespace EducationSystem.Data.Tests
{
    public class TagRepositoryTests : BaseTest
    {
        private ITagRepository _tagRepo;
        private List<int> _tagIdList;

        [OneTimeSetUp]
        public void TagTestsSetup()
        {
            _tagRepo = new TagRepository(_options);
            _tagIdList = new List<int>();
        }


        [TestCase(1)]
        public void TagDeleteTest(int dtoMockNumber)
        {
            TagDto expected = (TagDto)TagMockGetter.GetTagDtoMock(dtoMockNumber).Clone();
            var addedTagId = _tagRepo.TagAdd(expected);

            _tagRepo.TagDelete(addedTagId);

            TagDto actual = _tagRepo.GetTagById(addedTagId);
            Assert.IsNull(actual);
        }
        [TestCase(new int[] { 1, 2, 3 })]
        public void TagSelectAllPositiveTest(int[] mockIds)
        {
            // Given
            var expected = _tagRepo.GetTags();
            for (var i = 0; i < mockIds.Length; i++)
            {
                var dto = (TagDto)TagMockGetter.GetTagDtoMock(mockIds[i]).Clone();
                var addedEntityId = _tagRepo.TagAdd(dto);
                _tagIdList.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);
            }
            // When
            var actual = _tagRepo.GetTags();
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        public void TagAddTest(int dtoMockNumber)
        {
            TagDto expected = (TagDto)TagMockGetter.GetTagDtoMock(dtoMockNumber).Clone();
            var added = _tagRepo.TagAdd(expected);
            Assert.Greater(added, 0);

            _tagIdList.Add(added);
            expected.Id = added;

            TagDto actual = _tagRepo.GetTagById(added);
            Assert.AreEqual(expected, actual);
        }
        [TestCase(4)]
        public void TagAdd_EmptyTag_NegativeTest(int mockId)
        {
            //Given
            var dto = (TagDto)TagMockGetter.GetTagDtoMock(mockId).Clone();
           //When
            try
            {
                var added = _tagRepo.TagAdd(dto);
                _tagIdList.Add(added);
            }
            //Then
            catch (Exception ex)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void TagAdd_Null_NegativeTest()
        {
            //Given
            //When
            try
            {
                var added = _tagRepo.TagAdd(null);
                _tagIdList.Add(added);
            }
            //Then
            catch (Exception ex)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void TagDeleteNegativeTest()
        {
            //Given
            //When
            var deletedRows = _tagRepo.TagDelete(-1);

            //Then
            Assert.AreEqual(0, deletedRows);
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

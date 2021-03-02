
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class TagsTests
    {
        private List<int> _tagId;
        private TagRepository _tRepo;
        [SetUp]
        public void TagTestsSetup()
        {
            _tagId = new List<int>();

        }


        [TestCase(2)]
        public void TagAddTests(int dtoMockNumber)
        {
            TagDto expected = GetMockTagAdd(dtoMockNumber);
            var added = _tRepo.TagAdd(expected);
            _tagId.Add(added);
            expected.Id = added;

            if (_tagId.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                TagDto actual = _tRepo.GetTagById(_tagId[_tagId.Count - 1]);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(2)]
        public void TagDelete(int dtoMockNumber)
        {
            TagDto expected = GetMockTagAdd(dtoMockNumber);
            _tagId.Add(_tRepo.TagAdd(expected));
            if (_tagId.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                int newId = _tagId[_tagId.Count - 1];
                _tRepo.TagDelete(newId);
                TagDto actual = _tRepo.GetTagById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(2)]
        public void TagUpdate(int dtoMockNumber)
        {
            TagDto expected = GetMockTagAdd(dtoMockNumber);
            _tagId.Add(_tRepo.TagAdd(expected));
            if (_tagId.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                int newId = _tagId[_tagId.Count - 1];
                expected.Name = "B_TESTCASE1";
                expected.Id = newId;
                _tRepo.TagUpdate(expected);
                TagDto actual = _tRepo.GetTagById(newId);
                Assert.AreEqual(expected, actual);
            }
        }

        public TagDto GetMockTagAdd(int n)
        {
            switch (n)
            {
                case 1:
                    TagDto tagDto = new TagDto();
                    tagDto.Name = "B_TESTCASE1";
                    return tagDto;
                case 2:
                    TagDto tagDto2 = new TagDto();
                    tagDto2.Name = "B_TESTCASE3";
                    return tagDto2;
                default:
                    throw new Exception();
            }
        }

        [TearDown]
        public void TagsTestsTearDown()
        {
            
            foreach (int elem in _tagId)
            {
                _tRepo.TagDelete(elem);
            }
        }

    }
}

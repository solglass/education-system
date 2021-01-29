using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;

namespace NUnitTestProject
{
    public class TagsTests
    {
        private List<int> _tagId;
        private TagRepository _tRepo;
        [SetUp]
        public void TagTestsSetup()
        {
            _tagId = new List<int>();
            _tRepo = new TagRepository();

        }


        [TestCase(1)]
        public void Tag_AddTests(int dtoMockNumber)
        {
            TagDto expected = GetMockTag_Add(dtoMockNumber);

            //expected.Id = _tRepo.GetTagById(_tagId[_tagId.Count - 1]);
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



        [TearDown]
        public void TagsTestsTearDown()
        {
            TagRepository tRepo = new TagRepository();
            foreach (int elem in _tagId)
            {
                tRepo.TagDelete(elem);
            }
        }
        [TestCase(1)]
        public void Tag_Delete(int dtoMockNumber)
        {
            TagDto expected = GetMockTag_Add(dtoMockNumber);
            TagRepository tRepo = new TagRepository();
            _tagId.Add(tRepo.TagAdd(expected));
            if (_tagId.Count == 0) { Assert.Fail("Tag addition failed"); }
            else
            {
                int newId = _tagId[_tagId.Count - 1];
                tRepo.TagDelete(newId);
                TagDto actual = tRepo.GetTagById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(1)]
        public void Tag_Update(int dtoMockNumber)
        {
            TagDto expected = GetMockTag_Add(dtoMockNumber);
            TagRepository tRepo = new TagRepository();
            _tagId.Add(tRepo.TagAdd(expected));
            if (_tagId.Count == 0) { Assert.Fail("Attachment addition failed"); }
            else
            {
                int newId = _tagId[_tagId.Count - 1];
                expected.Name = "B_TESTCASE2";
                expected.Id = newId;
                tRepo.TagUpdate(expected);
                TagDto actual = tRepo.GetTagById(newId);

                Assert.AreEqual(expected, actual);
            }
        }

        public TagDto GetMockTag_Add(int n)
        {
            switch (n)
            {
                case 1:
                    TagDto tagDto = new TagDto();
                    tagDto.Name = "B_TESTCASE2";
                    
                    return tagDto;
                default:
                    throw new Exception();
            }
        }


    }
}
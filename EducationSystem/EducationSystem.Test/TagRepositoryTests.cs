
using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;

namespace NUnitTestProject
{
    public class TagsTests
    {
        private int tagId;
        [SetUp]
        public void TagTestsSetup()
        {

        }

        [TestCase(1)]
        public void Tag_AddTests(int dtoMockNumber)
        {
            TagDto expected = GetMockTag_Add(dtoMockNumber);
           TagRepository tRepo = new TagRepository();
           tagId = tRepo.TagAdd(expected);
            TagDto actual = tRepo.GetTagById(tagId);
            Assert.AreEqual(expected, actual);
        }

       

        [TearDown]
        public void TagsTestsTearDown()
        {
            TagRepository tRepo = new TagRepository();
            if (tagId != 0)
            {
                tRepo.TagDelete(tagId);
            }
            
        }

        public TagDto GetMockTag_Add(int n)
        {
            switch (n)
            {
                case 1:
                   TagDto tagDto = new TagDto();
                    tagDto.Id = 1;
                    tagDto.Name = "WPF";
                    return tagDto;
                default:
                    throw new Exception();
            }
        }

     
    }
}
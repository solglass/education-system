using Dapper;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Test
{
    public class TagRepositoryTest
    {


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void DeleteTagTest(int id)
        {
            TagRepository tagRepository = new TagRepository();


            var expected = tagRepository.GetTags();


            var deleted = expected.Where(x => x.Id == id).ToList();
            

            expected.RemoveAll(x => x.Id == id);



            tagRepository.TagDelete(id);

            var actual = tagRepository.GetTags();

            for (int i = 0; i < deleted.Count; ++i)
            {
                tagRepository.TagAdd(deleted[i]);
            }

            Assert.AreEqual(expected,actual);
        }


    }
}
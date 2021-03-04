using EducationSystem.Core.Config;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class SampleTest : BaseTest
    {
        private SampleRepository _repository;
        private List<int> _addedSimpleDtoIds;
        private List<int> _addedComplexDtoIds;
        private SimpleDto _simpleDtoMock;

        [OneTimeSetUp]
        public void SampleTestOneTimeSetUp()
        {
            _repository = new SampleRepository(_options);
            _addedSimpleDtoIds = new List<int>();
            _addedComplexDtoIds = new List<int>();

            _simpleDtoMock = (SimpleDto)MockGetter.GetSampleDtoMock(1).Clone();
            var addedSampleDtoId = _repository.SampleAdd(_simpleDtoMock);
            _simpleDtoMock.Id = addedSampleDtoId;
            _addedSimpleDtoIds.Add(addedSampleDtoId);
        }

        // entity tests are below:
        // entity - group, user, payment, etc.

        [TestCase(1)]
        public void EntityAddPositiveTest(int mockId)
        {
            // Given
            var dto = (SimpleDto)MockGetter.GetSampleDtoMock(mockId).Clone();
            var addedEntityId = _repository.SampleAdd(dto);
            Assert.Greater(addedEntityId, 0);

            _addedSimpleDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            // When
            var actual = _repository.SampleSelectById(addedEntityId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void EntityUpdatePositiveTest(int mockId)
        {
            // Given
            var dto = (SimpleDto)MockGetter.GetSampleDtoMock(mockId).Clone();
            var addedEntityId = _repository.SampleAdd(dto);
            _addedSimpleDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            // мы меняем только те поля, которые могут измениться в рамках хранимки на апдейт
            dto.SampleProp1 = "New value1";
            dto.SampleProp2 = "New value2";

            // When
            var affectedRowsCount = _repository.SampleUpdate(dto);
            var actual = _repository.SampleSelectById(addedEntityId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            // проверяем, что все свойства, которые должны были измениться, изменились
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void EntityDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (SimpleDto)MockGetter.GetSampleDtoMock(mockId).Clone();
            var addedEntityId = _repository.SampleAdd(dto);
            _addedSimpleDtoIds.Add(addedEntityId);

            // When
            var affectedRowsCount = _repository.SampleDelete(addedEntityId);

            var actual = _repository.SampleSelectById(addedEntityId);

            //case1: hard delete
            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);

            //case2: soft delete
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsTrue(actual.IsDeleted);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void EntitySelectAllPositiveTest(int[] mockIds)
        {
            // Given
            var expected = new List<SimpleDto>();
            for(var i = 0; i < mockIds.Length; i++)
            {
                var dto = (SimpleDto)MockGetter.GetSampleDtoMock(mockIds[i]).Clone();
                var addedEntityId = _repository.SampleAdd(dto);
                _addedSimpleDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);
            }

            // When
            var actual = _repository.SampleSelectAll();

            // Then
            // in simple case:
            CollectionAssert.AreEqual(expected, actual);

            // in worst case
            for(var i = 0; i < actual.Count; i++)
            {
                //Assert.IsTrue(CustomCompare(expected[i], actual[i]));
            }
        }



        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        public void ComplexEntityAddPositiveTest(int mockId)
        {
            // Given
            var dto = (ComplexDto)MockGetter.GetComplexDtoMock(mockId).Clone();
            dto.SampleProp1 = _simpleDtoMock; 

            var addedEntityId = _repository.ComplexAdd(dto);
            Assert.Greater(addedEntityId, 0); // move to negative test

            _addedComplexDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            // When
            var actual = _repository.SampleSelectById(addedEntityId);

            // Then
            Assert.AreEqual(dto, actual);
            // or Assert.IsTrue(CustomCompare(dto, actual));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ComplexEntityUpdatePositiveTest(int mockId)
        {
            // Given
            var dto = (ComplexDto)MockGetter.GetComplexDtoMock(mockId).Clone();
            dto.SampleProp1 = _simpleDtoMock;

            var addedEntityId = _repository.ComplexAdd(dto);
            _addedComplexDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            // мы меняем только те поля, которые могут измениться в рамках хранимки на апдейт            
            dto.SampleProp2 = "New value2";

            // When
            var affectedRowsCount = _repository.ComplexUpdate(dto);
            var actual = _repository.SampleSelectById(addedEntityId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            // проверяем, что все свойства, которые должны были измениться, изменились
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ComplexEntityDeletePositiveTest(int mockId)
        {
            // Given
            var dto = (ComplexDto)MockGetter.GetComplexDtoMock(mockId).Clone();
            dto.SampleProp1 = _simpleDtoMock;

            var addedEntityId = _repository.ComplexAdd(dto);
            _addedComplexDtoIds.Add(addedEntityId);

            // When
            var affectedRowsCount = _repository.ComplexDelete(addedEntityId);
            var actual = _repository.SampleSelectById(addedEntityId);

            //case1: hard delete
            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsNull(actual);

            //case2: soft delete
            Assert.AreEqual(1, affectedRowsCount);
            Assert.IsTrue(actual.IsDeleted);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void ComplexEntitySelectAllPositiveTest(int[] mockIds)
        {
            var expected = new List<ComplexDto>();
            for (var i = 0; i < mockIds.Length; i++)
            {
                var dto = (ComplexDto)MockGetter.GetComplexDtoMock(mockIds[i]).Clone();
                dto.SampleProp1 = _simpleDtoMock;
                var addedEntityId = _repository.ComplexAdd(dto);
                _addedComplexDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);
            }

            // When
            var actual = _repository.ComplexSelectAll();

            // Then
            // in simple case:
            CollectionAssert.AreEqual(expected, actual);

            // in worst case
            for (var i = 0; i < actual.Count; i++)
            {
                //Assert.IsTrue(CustomCompare(expected[i], actual[i]));
            }
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

        [TearDown]
        public void SampleTestTearDown()
        {
            _addedComplexDtoIds.ForEach(id =>
            {
                _repository.ComplexHardDelete(id);
                // or _repository.ComplexDelete(id); 
            });
            _addedSimpleDtoIds.ForEach(id =>
            {
                _repository.SampleHardDelete(id);
                // or _repository.SampleDelete(id); 
            });
        }
    }










    // fake code below
    public class SampleRepository : BaseRepository 
    {
        public SampleRepository(IOptions<AppSettingsConfig> options) : base(options) {}
        
        public int SampleAdd(SimpleDto dto) { return 42; }
        public int ComplexAdd(ComplexDto dto) { return 42; }
        public int SampleUpdate(SimpleDto dto) { return 1; }
        public int ComplexUpdate(ComplexDto dto) { return 1; }
        public SimpleDto SampleSelectById(int id) { return new SimpleDto(); }
        public List<SimpleDto> SampleSelectAll() { return new List<SimpleDto>(); }
        public ComplexDto ComplexSelectById(int id) { return new ComplexDto(); }
        public List<ComplexDto> ComplexSelectAll() { return new List<ComplexDto>(); }
        public void ComplexHardDelete(int id) {  }
        public int ComplexDelete(int id) { return 1; }
        public void SampleHardDelete(int id) { SampleDelete(id); }
        public int SampleDelete(int id) { return 1; }
    }

    public class SimpleDto : ICloneable {
        public int Id { get; set; }
        public string SampleProp1 { get; set; }
        public string SampleProp2 { get; set; }
        public string SampleProp3 { get; set; }
        public bool IsDeleted { get; set; }
        public override bool Equals(object obj)
        {
            // except properties autogenerated by DB
            return base.Equals(obj);
        }

        public object Clone()
        {
            return new SimpleDto { Id = Id, SampleProp1 = SampleProp1, SampleProp2 = SampleProp2, SampleProp3 = SampleProp3 };
        }
    }

    /*
     * GroupDto -> CourseDto
     * Group -> CourseId
     * */
    public class ComplexDto : ICloneable
    {
        public int Id { get; set; }
        public SimpleDto SampleProp1 { get; set; }
        public string SampleProp2 { get; set; }
        public override bool Equals(object obj)
        {
            // except properties autogenerated by DB
            return base.Equals(obj);
        }

        public object Clone()
        {
            return new ComplexDto { Id = Id, SampleProp1 = SampleProp1, SampleProp2 = SampleProp2 };
        }
    }

    public static class MockGetter
    {
        public static SimpleDto GetSampleDtoMock(int id)
        {
            // switch
            return new SimpleDto();
        }

        public static ComplexDto GetComplexDtoMock(int id)
        {
            // switch
            return new ComplexDto { Id = 0, SampleProp1 = null, SampleProp2 = "Some value" };
        }
    }
}

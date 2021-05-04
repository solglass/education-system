using NUnit.Framework;
using EducationSystem.Data.Models;
using System.Collections.Generic;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupTests : BaseTest
    {
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IMaterialRepository _materialRepo;


        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _materialIdList;
        private List<(int, int)> _groupMaterialIdList;
        private CourseDto _courseDtoMock;


        [SetUp]
        public void OneTimeSetUp()
        {
            _groupRepo = new GroupRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _materialRepo = new MaterialRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _materialIdList = new List<int>();
            _groupMaterialIdList = new List<(int,int)>();

            _courseDtoMock = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            _courseDtoMock.Id = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(_courseDtoMock.Id);
        }

        [TestCase(new int[] { 1, 2 })]
        public void AddGroupMaterialPositiveTest(int[] mockIds)
        {
            //Given
            var expected = new List<MaterialDto>();

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[1]).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(groupDto.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {

                var materialDto = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(mockIds[i]).Clone();
                materialDto.Id = _materialRepo.AddMaterial(materialDto);
                _materialIdList.Add(materialDto.Id);
                expected.Add(materialDto);

                _groupRepo.AddGroup_Material(groupDto.Id, materialDto.Id);
                _groupMaterialIdList.Add((groupDto.Id, materialDto.Id));
            }


            //When
            var actual = _materialRepo.GetMaterialsByGroupId(groupDto.Id);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void AddGroupMaterialNegativeTestMaterialNotExists()
        {
            //Given
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(groupDto.Id);

            //When
            try
            {
                _groupRepo.AddGroup_Material(groupDto.Id, -1);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void AddGroupMaterialNegativeTestNotUniqueEntity()
        {
            //Given
            var expected = new List<MaterialDto>();

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(groupDto.Id);
            for (int i = 1; i < 3; i++)
            {

                var materialDto = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(i).Clone();
                materialDto.Id = _materialRepo.AddMaterial(materialDto);
                _materialIdList.Add(materialDto.Id);
                expected.Add(materialDto);

                _groupRepo.AddGroup_Material(groupDto.Id, materialDto.Id);
                _groupMaterialIdList.Add((groupDto.Id, materialDto.Id));
            }

            //When
            try
            {
                _groupRepo.AddGroup_Material(groupDto.Id, _materialIdList[1]);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }


        [TestCase(new int[] { 1, 2 })]
        public void DeleteGroupMaterialPositiveTest(int[] mockIds)
        {
            //Given
            var expected = new List<MaterialDto>();

            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[1]).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(groupDto.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {

                var materialDto = (MaterialDto)MaterialMockGetter.GetMaterialDtoMock(mockIds[i]).Clone();
                materialDto.Id = _materialRepo.AddMaterial(materialDto);
                _materialIdList.Add(materialDto.Id);
                expected.Add(materialDto);
                _groupRepo.AddGroup_Material(groupDto.Id, materialDto.Id);
                _groupMaterialIdList.Add((groupDto.Id, materialDto.Id));
                if (i == mockIds.Length - 1)
                {
                    _groupRepo.DeleteGroup_Material(groupDto.Id, materialDto.Id);
                    expected.RemoveAt(mockIds.Length - 1);
                }
            }

            //When
            var actual = _materialRepo.GetMaterialsByGroupId(groupDto.Id);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void DeleteGroupMaterialNegativeTestMaterialNotExists()
        {
            //Given
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            groupDto.Course = _courseDtoMock;
            groupDto.Id = _groupRepo.AddGroup(groupDto);
            _groupIdList.Add(groupDto.Id);
            //When
            var result = _groupRepo.DeleteGroup_Material(groupDto.Id, -1);
            //Then
            Assert.AreEqual(0, result);
        }
        [Test]
        public void DeleteCommentAttachmentNegativeTestRelationNotExists()
        {
            //Given
            //When
            var result = _groupRepo.DeleteGroup_Material(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }

        [TearDown]
        public void TearDown()
        {
            DeleteGroupMaterials();
            DeleteMaterials();
            DeleteGroups();
            DeleteCourse();
        }

        private void DeleteGroupMaterials()
        {
            foreach (var groupMaterialPair in _groupMaterialIdList)
            {
                _groupRepo.DeleteGroup_Material(groupMaterialPair.Item1, groupMaterialPair.Item2);
            }
        }
        private void DeleteMaterials()
        {
            foreach (var materialId in _materialIdList)
            {
                _materialRepo.HardDeleteMaterial(materialId);
            }
        }
        public void DeleteGroups()
        {
            foreach (var groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }
        public void DeleteCourse()
        {
            foreach (var courseId in _courseIdList)
            {
                _courseRepo.HardDeleteCourse(courseId);
            }
        }

    }
}

using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class GroupRepositoryTests
    {
        private GroupRepository _groupRepo;
        private CourseRepository _courseRepo;
        private GroupStatusRepository _groupStatusRepo;
        private CourseDto _courseDto;
        private GroupStatusDto _groupStatusDto;
        private int _groupId;
        private int _courseId;
        private int _groupStatusId;

        [SetUp]
        public void GroupTestsSetup()
        {
            _groupRepo = new GroupRepository();
            _courseRepo = new CourseRepository();
            _groupStatusRepo = new GroupStatusRepository();
            _groupId = 0;
            _courseId = 0;
            _groupStatusId = 0;
        }

        [TestCase()]
        public void Group_Add()
        {
            _courseDto = GetMockCourse_Add();
            _groupStatusDto = GetMockGroupStatus_Add();
            _courseId = _courseRepo.AddCourse(_courseDto);
            _groupStatusId = _groupStatusRepo.AddGroupStatus(_groupStatusDto.Name);
            GroupDto expected = GetMockGroup_Add();
            _groupId = _groupRepo.AddGroup(expected);

            if (_groupId == 0 || _courseId == 0 || _groupStatusId == 0) 
            { 
                Assert.Fail("Group addition failed"); 
            }
            else
            {
                GroupDto actual = _groupRepo.GetGroupById(_groupId);
                Assert.AreEqual(expected, actual);
            }

        }

        [TestCase()]
        public void Group_Update()
        {
            _courseDto = GetMockCourse_Add();
            _groupStatusDto = GetMockGroupStatus_Add();
            _courseId = _courseRepo.AddCourse(_courseDto);
            _groupStatusId = _groupStatusRepo.AddGroupStatus(_groupStatusDto.Name);
            GroupDto expected = GetMockGroup_Add();
            _groupId = _groupRepo.AddGroup(expected);

            if (_groupId == 0 || _courseId == 0 || _groupStatusId == 0)
            {
                Assert.Fail("Group addition failed");
            }
            else
            {
                expected.StartDate = new DateTime(2021, 02, 04);
                _groupRepo.UpdateGroup(expected);
                GroupDto actual = _groupRepo.GetGroupById(_groupId);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase()]
        public void Group_Delete()
        {
            _courseDto = GetMockCourse_Add();
            _groupStatusDto = GetMockGroupStatus_Add();
            _courseId = _courseRepo.AddCourse(_courseDto);
            _groupStatusId = _groupStatusRepo.AddGroupStatus(_groupStatusDto.Name);
            GroupDto expected = GetMockGroup_Add();
            _groupId = _groupRepo.AddGroup(expected);

            if (_groupId == 0 || _courseId == 0 || _groupStatusId == 0) 
            {
                Assert.Fail("Group addition failed"); 
            }
            else
            {
                _groupRepo.DeleteGroup(_groupId);
                GroupDto actual = _groupRepo.GetGroupById(_groupId);
                if (actual == null) 
                {
                    Assert.Pass(); 
                }
                else 
                    Assert.Fail("Failed to delete group");
            }
        }

        [TearDown]
        public void GroupsTestsTearDown()
        {
            _groupRepo.DeleteGroup(_groupId);
            _courseRepo.DeleteCourse(_courseId);
            _groupStatusRepo.DeleteGroupStatus(_groupStatusId);
        }

        public CourseDto GetMockCourse_Add()
        {
            return new CourseDto
            {
                Name = "TestCourseCase 1", 
                Description = "Test case 1", 
                Duration = 1
            };
        }

        public GroupStatusDto GetMockGroupStatus_Add()
        {
            return new GroupStatusDto
            {
                Name = "333"
            };
        }

        public GroupDto GetMockGroup_Add()
        {
            return new GroupDto
            {
                Course = _courseRepo.GetCourseById(_courseId),
                GroupStatus = _groupStatusRepo.GetGroupStatusById(_groupStatusId),
                StartDate = new DateTime(2021, 01, 01)
            };
        }
    }
}

using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using System.Collections.Generic;
using EducationSystem.Core.Enums;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupTests
    {
        private List<int> _groupsId;
        private GroupRepository gRepo;
        [SetUp]
        public void GroupsTestsSetup()
        {
            _groupsId = new List<int>();
            GroupDto expected = GetMockGroup_Add(1);


        }

        [TestCase(1)]
        public void Attachment_Add(int dtoMockNumber)
        {
            GroupDto expected = GetMockGroup_Add(dtoMockNumber);
            _groupsId.Add(gRepo.AddGroup(expected));
            expected.Id = _groupsId[_groupsId.Count - 1];
            if (_groupsId.Count == 0) { Assert.Fail("Group addition failed"); }
            else
            {
                GroupDto actual = gRepo.GetGroupById(expected.Id);
                Assert.AreEqual(expected, actual);
            }

        }


        [TestCase(1)]
        public void Group_Delete(int dtoMockNumber)
        {
            GroupDto expected = GetMockGroup_Add(dtoMockNumber);
            _groupsId.Add(gRepo.AddGroup(expected));
            if (_groupsId.Count == 0) { Assert.Fail("Group addition failed"); }
            else
            {
                int newId = _groupsId[_groupsId.Count - 1];
                gRepo.DeleteGroup(newId);
                GroupDto actual = gRepo.GetGroupById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(1)]
        public void Group_Update(int dtoMockNumber)
        {
            GroupDto expected = GetMockGroup_Add(dtoMockNumber);
            _groupsId.Add(gRepo.AddGroup(expected));
            if (_groupsId.Count == 0) { Assert.Fail("Group addition failed"); }

            int newId = _groupsId[_groupsId.Count - 1];
            GroupStatus groupStatusUpdate = GroupStatus.Finished;
            expected.GroupStatus = groupStatusUpdate;
            expected.Id = newId;
            gRepo.UpdateGroup(expected);
            GroupDto actual = gRepo.GetGroupById(newId);
            Assert.AreEqual(expected, actual);

        }



        [TearDown]
        public void AttachmentsTestsTearDown()
        {

            foreach (int elem in _groupsId)
            {
                gRepo.DeleteGroup(elem);
            }

        }

        public GroupDto GetMockGroup_Add(int n)
        {
            switch (n)
            {
                case 1:
                    GroupDto groupDto = new GroupDto();
                    GroupStatus groupStatus = GroupStatus.InProgress;
                    groupDto.GroupStatus = groupStatus;
                    CourseDto courseDto = new CourseDto()
                    {
                        Id = 999,
                        IsDeleted = false,
                        Name = "Test",
                    };
                    groupDto.Course = courseDto;
                    groupDto.StartDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    return groupDto;
                default:
                    throw new Exception();
            }
        }

    }
}

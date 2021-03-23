using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using EducationSystem.Core.Enums;
using System.Globalization;
using EducationSystem.Data.Tests.Mocks;

namespace EducationSystem.Data.Tests
{
  public class GroupRepositoryTests : BaseTest
  {
    private IHomeworkRepository _homeworkRepo;
    private IGroupRepository _groupRepo;
    private ICourseRepository _courseRepo;
    private IUserRepository _userRepo;
    private ITagRepository _tagRepo;

    private List<int> _homeworkIdList;
    private List<int> _groupIdList;
    private List<int> _courseIdList;
    private List<int> _themeIdList;
    private List<int> _tagIdList;
    private List<(int, int)> _themeHomeworkList;
    private List<(int, int)> _tagHomeworkList;

    private CourseDto _courseDtoMock;
    private GroupDto _groupDtoMock;


    [OneTimeSetUp]
    public void OneTimeSetUpTest()
    {
      _groupRepo = new GroupRepository(_options);
      _homeworkRepo = new HomeworkRepository(_options);
      _courseRepo = new CourseRepository(_options);
      _tagRepo = new TagRepository(_options);

      _groupIdList = new List<int>();
      _courseIdList = new List<int>();
      _themeIdList = new List<int>();
      _homeworkIdList = new List<int>();
      _tagIdList = new List<int>();
      _themeHomeworkList = new List<(int, int)>();
      _tagHomeworkList = new List<(int, int)>();

      _courseDtoMock = CourseMockGetter.GetCourseDtoMock(1);
      var addedCourseId = _courseRepo.AddCourse(_courseDtoMock);
      _courseIdList.Add(addedCourseId);
      _courseDtoMock.Id = addedCourseId;
    }

    private List<int> _groupsId;
    private GroupRepository gRepo;
    [SetUp]
    public void GroupsTestsSetup()
    {
      _groupsId = new List<int>();
      GroupDto expected = GetMockGroup_Add(1);

    }
    [TestCase(1)]
    public void GroupAddPositiveTest(int mockId)
    {
      //Given
      var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
      dto.Course = _courseDtoMock;

      var addedGroupId = _groupRepo.AddGroup(dto);
      Assert.Greater(addedGroupId, 0);

      _groupIdList.Add(addedGroupId);
      dto.Id = addedGroupId;

      //When
      var actual = _groupRepo.GetGroupById(addedGroupId);

      //Then
      Assert.AreEqual(dto, actual);

    }
    [TestCase(1,3)]
    public void GroupUpdatePositiveTest(int mockId, int statusId)
    {
      //Given
      var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
      dto.Course = _courseDtoMock;
      var addedGroupId = _groupRepo.AddGroup(dto);
      _groupIdList.Add(addedGroupId);
    
      dto = new GroupDto
      {
        Id = addedGroupId,
        StartDate = DateTime.Today.AddDays(1),
        Course = _courseDtoMock,
        GroupStatus = (GroupStatus)statusId
      };
      _groupRepo.UpdateGroup(dto);
    
      //When
      var actual = _groupRepo.GetGroupById(addedGroupId);
    
      //Then
      Assert.AreEqual(dto, actual);
    
    }

    [TestCase(new int[] { 1, 2, 3 })]
    [TestCase(new int[] { 1, 2, 3, 4 })]
    [TestCase(new int[] { 1, 2, })]
    public void GetAllGroupPositiveTest(int[] mockIds)
    {

      //Given
      var expected = new List<GroupDto>();
      var _addedGroupDtoIdsLocal = new List<int>();
      foreach (int mockId in mockIds)
      {
        var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
        dto.Course = _courseDtoMock;
        var addedEntityId = _groupRepo.AddGroup(dto);
        _groupIdList.Add(addedEntityId);
        _addedGroupDtoIdsLocal.Add(addedEntityId);

        dto.Id = addedEntityId;
        expected.Add(dto);
      }

      //When, Then
      var actual = _groupRepo.GetGroups();
      CollectionAssert.AreEqual(expected, actual);
    }

    //[TestCase(new int[] { 1, 2, 3 })]
    //[TestCase(new int[] { })]
    //public void GetGroupByThemeIdPositiveTest(int[] mockIds)
    //{
    //  //Given
    //  var courseDto = (CourseDto)_courseDtoMock.Clone();
    //  var addedCourseId = _courseRepo.AddCourse(courseDto);
    //  _courseIdList.Add(addedCourseId);
    //  courseDto.Id = addedCourseId;
    //
    //  var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
    //  var addedThemeId = _courseRepo.AddTheme(themeDto);
    //  var secondAddedThemeId = _courseRepo.AddTheme(themeDto);
    //  _themeIdList.Add(addedThemeId);
    //  _themeIdList.Add(secondAddedThemeId);
    //
    //  var expected = new List<GroupDto>();
    //  for (int i = 0; i < mockIds.Length; i++)
    //  {
    //    var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
    //    dto.Course = courseDto;
    //    var addedGroupId = _groupRepo.AddGroup(dto);
    //    _groupIdList.Add(addedGroupId);
    //    dto.Id = addedGroupId;
    //    expected.Add(dto);
    //  }
    //
    //  for (int i = 0; i < mockIds.Length; i++)
    //  {
    //    var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
    //    dto.Course = courseDto;
    //    var addedGroupId = _groupRepo.AddGroup(dto);
    //    _groupIdList.Add(addedGroupId);
    //    dto.Id = addedGroupId;
    //  }
    //  //When
    //  var actual = _groupRepo.GetGroupByThemeId( addedThemeId);
    //
    //  //Then
    //  CollectionAssert.AreEqual(expected, actual);
    //}
    [Test]
    public void GetGroupByThemeIdPositiveTest()
    {
      //Given;
      var expectedGroups = new List<GroupDto>();
      var groupMockIds = new int[] { 1, 2, 3 };
      for (int i = 0; i < groupMockIds.Length; i++)
      {
        var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockIds[i]).Clone();

        group.Course = (CourseDto)_courseDtoMock.Clone();
        var addedCourseId = _courseRepo.AddCourse(group.Course);
        group.Course.Id = addedCourseId;
        _courseIdList.Add(addedCourseId);


        var addedGroupId = _groupRepo.AddGroup(group);
        _groupIdList.Add(addedGroupId);
        group.Id = addedGroupId;
        expectedGroups.Add(group);

        var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();
        var addedThemeId = _courseRepo.AddTheme(themeDto);
        var secondAddedThemeId = _courseRepo.AddTheme(themeDto);
        _themeIdList.Add(addedThemeId);
        _themeIdList.Add(secondAddedThemeId);
      }

      //When
      var actual = _groupRepo.GetGroupByThemeId(addedThemeId);

      //Then
      CollectionAssert.AreEqual(expectedGroups, actual);
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



    //[TearDown]
    //public void AttachmentsTestsTearDown()
    //{
    //
    //    foreach (int elem in _groupsId)
    //    {
    //        gRepo.DeleteGroup(elem);
    //    }
    //
    //}

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
    [OneTimeTearDown]
    public void TearDowTest()
    {
      DeleteThemeHomeworks();
      DeteleTagHomeworks();
      DeleteHomeworks();
      DeleteGroups();
      DeleteCourse();
      DeleteThemes();
    }

    private void DeteleTagHomeworks()
    {
      foreach (var tagHomework in _tagHomeworkList)
      {
        _homeworkRepo.HomeworkTagDelete(tagHomework.Item1, tagHomework.Item2);
      }
    }

    private void DeleteThemeHomeworks()
    {
      foreach (var theneHomeworkPair in _themeHomeworkList)
      {
        _homeworkRepo.DeleteHomework_Theme(theneHomeworkPair.Item1, theneHomeworkPair.Item2);
      }
    }

    private void DeleteThemes()
    {
      foreach (int themeId in _themeIdList)
      {
        _courseRepo.HardDeleteTheme(themeId);
      }
    }

    private void DeleteHomeworks()
    {
      foreach (int homeworkId in _homeworkIdList)
      {
        _homeworkRepo.HardDeleteHomework(homeworkId);
      }
    }

    public void DeleteGroups()
    {
      foreach (int groupId in _groupIdList)
      {
        _groupRepo.HardDeleteGroup(groupId);
      }
    }
    public void DeleteCourse()
    {
      foreach (int courseId in _courseIdList)
      {
        _courseRepo.HardDeleteCourse(courseId);
      }
    }

  }
}

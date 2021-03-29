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
    public class GroupTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IUserRepository _userRepo;
        private ITagRepository _tagRepo;
        private ILessonRepository _lessonRepo;


        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _themeIdList;
        private List<int> _lessonIdList;
        private List<int> _tagIdList;
        private List<(int, int)> _lessonThemeIdList;
        private List<(int, int)> _themeHomeworkList;
        private List<(int, int)> _tagHomeworkList;
        private List<(int, int)> _courseThemeIdList;
        private List<(int, int)> _addedUserRoleIdList;
        private List<(int, int)> _addedTutorToGroupIdList;
        private List<int> _addedUserIdList;

        private CourseDto _courseDtoMock;
        private GroupDto _groupDtoMock;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _tagRepo = new TagRepository(_options);
            _lessonRepo = new LessonRepository(_options);
            _userRepo = new UserRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _themeIdList = new List<int>();
            _lessonIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _lessonThemeIdList = new List<(int, int)>();
            _tagIdList = new List<int>();
            _themeHomeworkList = new List<(int, int)>();
            _courseThemeIdList = new List<(int, int)>();
            _tagHomeworkList = new List<(int, int)>();
            _addedUserRoleIdList = new List<(int, int)>();
            _addedTutorToGroupIdList = new List<(int, int)>();
            _addedUserIdList = new List<int>();

            _courseDtoMock = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            _courseDtoMock.Id = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(_courseDtoMock.Id);
        }

        private List<int> _groupsId;
        private GroupRepository gRepo;

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
        [TestCase(1, 3)]
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
        [TestCase(new int[] { 1, 2 })]
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

        [TestCase(2)]
        public void GetGroupByThemeIdPositiveTest(int themeMockId)
        {
            //Given;
            var expectedGroups = new List<GroupDto>();
            var groupMockIds = new int[] { 1, 2, 3 };
            for (int i = 0; i < groupMockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                expectedGroups.Add(groupDto);
                _groupIdList.Add(groupDto.Id);
            }
            var listLesson = new List<LessonDto>();
            for (int i = 0; i < groupMockIds.Length; i++)
            {
                var lessonMockDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(i + 1).Clone();
                lessonMockDto.Group = expectedGroups[i];
                lessonMockDto.Id = _lessonRepo.AddLesson(lessonMockDto);
                _lessonIdList.Add(lessonMockDto.Id);
                listLesson.Add(lessonMockDto);
            }

            var themeMockDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(themeMockId).Clone();
            themeMockDto.Id = _courseRepo.AddTheme(themeMockDto);
            _themeIdList.Add(themeMockDto.Id);

            for (int i = 0; i < listLesson.Count; i++)
            {
                _lessonRepo.AddLessonTheme(listLesson[i].Id, themeMockDto.Id);
                _lessonThemeIdList.Add((listLesson[i].Id, themeMockDto.Id));

            }
            //When
            var actual = _groupRepo.GetGroupByThemeId(themeMockDto.Id);

            //Then
            CollectionAssert.AreEqual(expectedGroups, actual);
        }

        [TestCase(2)]
        public void GetGroupProgramsByGroupIdPositiveTest(int groupMockId)
        {
            //Given;

            var expectedGroup = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            expectedGroup.Course = _courseDtoMock;

            var addedGroupId = _groupRepo.AddGroup(expectedGroup);
            Assert.Greater(addedGroupId, 0);

            _groupIdList.Add(addedGroupId);
            expectedGroup.Id = addedGroupId;

            var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(1).Clone();


            var addedThemeId = _courseRepo.AddTheme(themeDto);
            Assert.Greater(addedThemeId, 0);

            _themeIdList.Add(addedThemeId);
            themeDto.Id = addedThemeId;

            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var addedCourseId = _courseRepo.AddCourse(courseDto);
            Assert.Greater(addedCourseId, 0);

            _courseIdList.Add(addedCourseId);
            courseDto.Id = addedCourseId;


            _courseRepo.AddCourse_Theme(courseDto.Id, themeDto.Id);
            _courseThemeIdList.Add((courseDto.Id, themeDto.Id));

            //When
            var actual = _groupRepo.GetGroupProgramsByGroupId(1);
            //Then
            Assert.AreEqual(expectedGroup, actual);
        }

        [TestCase(1)]
        public void GetGroupByIdPositiveTest(int mockId)
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
        [TestCase(new int[] {1, 2, 3} )]

        public void GetGroupsWithoutTutorsPositiveTest(int[] mockIds) 
        {
            //Given
            var expectedGroups = new List<GroupDto>();
            var GroupsWithTutors = new List<GroupDto>();
           

            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(4).Clone();

            userDtoMock.Id = _userRepo.AddUser(userDtoMock);
            _userRepo.AddRoleToUser(userDtoMock.Id, 4);
            Assert.Greater(userDtoMock.Id, 0);
            userDtoMock.Roles =new List<Role> { Role.Tutor };
            _addedUserRoleIdList.Add((userDtoMock.Id, (int)Role.Tutor));
            _addedUserIdList.Add(userDtoMock.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                GroupsWithTutors.Add(groupDto);
                _groupIdList.Add(groupDto.Id);

                _groupRepo.AddTutorToGroup(userDtoMock.Id, groupDto.Id);
                _addedTutorToGroupIdList.Add((userDtoMock.Id, groupDto.Id));
            }

           
            var groupMockIds = new int[] { 4, 5 };
            for (int i = 0; i < groupMockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                expectedGroups.Add(groupDto);
                _groupIdList.Add(groupDto.Id);
            }

            //When
            var actual = _groupRepo.GetGroupsWithoutTutors();

            //Then
            CollectionAssert.AreEqual(expectedGroups, actual);
        }

    [TearDown]
    public void TearDown()
    { 
      DeleteThemeHomeworks();
      DeleteLessonTheme();
      DeleteThemeHomeworks();
      DeteleTagHomeworks();
      DeleteTutorGroup();
      DeleteThemes();
      DeleteHomeworks();
      DeleteGroups();
      DeleteCourse();
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
        public void DeleteTutorGroup()
        {
            foreach ((int, int) groupId in _addedTutorToGroupIdList)
            {
                _groupRepo.DeleteTutorGroup(groupId.Item1, groupId.Item2);
            }
        }
        public void DeleteCourse()
    {
      foreach (int courseId in _courseIdList)
      {
        _courseRepo.HardDeleteCourse(courseId);
      }
    }
        
        public void DeleteLessonTheme()
        {
            foreach ((int,int) courseId in _lessonThemeIdList)
            {
                _lessonRepo.DeleteLessonTheme(courseId.Item1,courseId.Item2);
            }
        }

    }
}

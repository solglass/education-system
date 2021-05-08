using NUnit.Framework;
using EducationSystem.Data.Models;
using System.Collections.Generic;
using EducationSystem.Core.Enums;
using System.Globalization;
using EducationSystem.Data.Tests.Mocks;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupTests : BaseTest
    {
        private IHomeworkRepository _homeworkRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;
        private IUserRepository _userRepo;
        private ILessonRepository _lessonRepo;
        private IMaterialRepository _materialRepo;


        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _themeIdList;
        private List<int> _lessonIdList;
        private List<int> _materialIdList;
        private List<(int, int)> _lessonThemeIdList;
        private List<(int, int)> _themeHomeworkList;
        private List<(int, int)> _tagHomeworkList;
        private List<(int, int)> _courseThemeIdList;
        private List<(int, int)> _addedUserRoleIdList;
        private List<(int, int)> _addedTutorToGroupIdList;
        private List<(int, int)> _groupMaterialIdList;
        private List<(int, int)> _studentGroupIdList;
        private List<int> _addedUserIdList;
        private List<(int, int)> _addedTeacherToGroupIdList;
        private List<int> _addedGroupReportIdList;

        private CourseDto _courseDtoMock;


        [SetUp]
        public void OneTimeSetUp()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _lessonRepo = new LessonRepository(_options);
            _userRepo = new UserRepository(_options);
            _materialRepo = new MaterialRepository(_options);

            _groupIdList = new List<int>();
            _courseIdList = new List<int>();
            _themeIdList = new List<int>();
            _lessonIdList = new List<int>();
            _homeworkIdList = new List<int>();
            _materialIdList = new List<int>();
            _lessonThemeIdList = new List<(int, int)>();
            _themeHomeworkList = new List<(int, int)>();
            _courseThemeIdList = new List<(int, int)>();
            _tagHomeworkList = new List<(int, int)>();
            _addedUserRoleIdList = new List<(int, int)>();
            _addedTutorToGroupIdList = new List<(int, int)>();
            _groupMaterialIdList = new List<(int, int)>();
            _addedUserIdList = new List<int>();
            _studentGroupIdList = new List<(int, int)>();
            _addedTeacherToGroupIdList = new List<(int, int)>();
            _addedGroupReportIdList = new List<int>();

            _courseDtoMock = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            _courseDtoMock.Id = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(_courseDtoMock.Id);
        }

        [TestCase(1)]
        public void GroupAddPositiveTest(int mockId)
        {
            //Given
            var expected = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
            expected.Course = _courseDtoMock;

            var addedGroupId = _groupRepo.AddGroup(expected);
            Assert.Greater(addedGroupId, 0);

            _groupIdList.Add(addedGroupId);
            expected.Id = addedGroupId;

            //When
            var actual = _groupRepo.GetGroupById(addedGroupId);

            //Then
            Assert.AreEqual(expected, actual);

        }
        [TestCase(1, 3)]
        public void GroupUpdatePositiveTest(int mockId, int statusId)
        {
            //Given
            var expected = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
            expected.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(expected);
            _groupIdList.Add(addedGroupId);

            expected = new GroupDto
            {
                Id = addedGroupId,
                StartDate = expected.StartDate.AddDays(1),
                Course = _courseDtoMock,
                GroupStatus = (GroupStatus)statusId
            };
            _groupRepo.UpdateGroup(expected);

            //When
            var actual = _groupRepo.GetGroupById(addedGroupId);

            //Then
            Assert.AreEqual(expected, actual);

        }
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1, 2 })]
        public void GetAllGroupPositiveTest(int[] mockIds)
        {

            //Given
            var expected = new List<GroupDto>();

            foreach (int mockId in mockIds)
            {
                var dto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
                dto.Course = _courseDtoMock;
                var addedEntityId = _groupRepo.AddGroup(dto);
                _groupIdList.Add(addedEntityId);

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
            var expected = new List<GroupDto>();
            var groupMockIds = new int[] { 1, 2, 3 };
            for (int i = 0; i < groupMockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                expected.Add(groupDto);
                _groupIdList.Add(groupDto.Id);
            }
            var listLesson = new List<LessonDto>();
            for (int i = 0; i < groupMockIds.Length; i++)
            {
                var lessonMockDto = (LessonDto)LessonMockGetter.GetLessonDtoMock(i + 1).Clone();
                lessonMockDto.Group = expected[i];
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
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetGroupProgramsByGroupIdPositiveTest()
        {
            //Given;
            var themes = new List<ThemeDto>();
            var expected = (GroupDto)GroupMockGetter.GetGroupDtoMock(2).Clone();
            expected.Course = _courseDtoMock;

           // expected.Course.Themes = themes;

            var addedGroupId = _groupRepo.AddGroup(expected);
            Assert.Greater(addedGroupId, 0);

            _groupIdList.Add(addedGroupId);
            expected.Id = addedGroupId;

            var groupMockIds = new int[] { 1, 2, 3 };
            for (int i = 0; i < groupMockIds.Length; i++) 
            {
                var themeDto = (ThemeDto)ThemeMockGetter.GetThemeDtoMock(groupMockIds[i]).Clone();
                var addedThemeId = _courseRepo.AddTheme(themeDto);
                Assert.Greater(addedThemeId, 0);

                _themeIdList.Add(addedThemeId);
                themeDto.Id = addedThemeId;
                themes.Add(themeDto);

               // _courseRepo.AddCourse_Theme(_courseDtoMock.Id, themeDto.Id);
                _courseThemeIdList.Add((_courseDtoMock.Id, themeDto.Id));
            }

            //When
           // var actual = _groupRepo.GetGroupProgramsByGroupId(expected.Id);
            //Then
           // Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        public void GetGroupByIdPositiveTest(int mockId)
        {
            //Given
            var expected = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
            expected.Course = _courseDtoMock;

            var addedGroupId = _groupRepo.AddGroup(expected);
            Assert.Greater(addedGroupId, 0);

            _groupIdList.Add(addedGroupId);
            expected.Id = addedGroupId;

            //When
            var actual = _groupRepo.GetGroupById(addedGroupId);

            //Then
            Assert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2, 3 },4,4)]

        public void GetGroupsWithoutTutorsPositiveTest(int[] mockIds,int tutorMockId, int userMockId)
        {
            //Given
            var expected = new List<GroupDto>();


            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(userMockId).Clone();

            userDtoMock.Id = _userRepo.AddUser(userDtoMock);
            _userRepo.AddRoleToUser(userDtoMock.Id, tutorMockId);
            Assert.Greater(userDtoMock.Id, 0);
            userDtoMock.Roles = new List<Role> { Role.Tutor };
            _addedUserRoleIdList.Add((userDtoMock.Id, (int)Role.Tutor));
            _addedUserIdList.Add(userDtoMock.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
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
                expected.Add(groupDto);
                _groupIdList.Add(groupDto.Id);
            }

            //When
            var actual = _groupRepo.GetGroupsWithoutTutors();

            //Then
            CollectionAssert.AreEqual(expected, actual);
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

        [TestCase(new int[] { 1, 2 })]

        public void GetGroupsByCourseIdPositiveTest(int [] mockIds)
        {
            //Given
            var expected = new List<GroupDto>();

            for (int i = 0; i < mockIds.Length; i++) 
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(groupDto.Id);
                expected.Add(groupDto);
            }

            //When
            var courseId = expected[1].Course.Id;
            var actual = _groupRepo.GetGroupsByCourseId(courseId);

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

        [TestCase(new int[] { 1, 2 }, 2)]
        
        public void GetGroupsByStudentIdPositiveTest(int [] mockIds, int studentMockId)
        {
            //Given
            var expected = new List<int>();

            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(4).Clone();
            

            userDtoMock.Id = _userRepo.AddUser(userDtoMock);
            _userRepo.AddRoleToUser(userDtoMock.Id, studentMockId);
            Assert.Greater(userDtoMock.Id, 0);
            userDtoMock.Roles = new List<Role> { Role.Student };
            _addedUserRoleIdList.Add((userDtoMock.Id, (int)Role.Student));
            _addedUserIdList.Add(userDtoMock.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(groupDto.Id);
                expected.Add(groupDto.Id);

                

                var studentGroupDtoMock = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(i+1).Clone();
                studentGroupDtoMock.Group = groupDto;
                studentGroupDtoMock.User = userDtoMock;
                studentGroupDtoMock.Id = _groupRepo.AddStudentGroup(studentGroupDtoMock);

                _studentGroupIdList.Add((userDtoMock.Id, groupDto.Id));
            }

            //When
            var actual = _groupRepo.GetGroupsByStudentId(userDtoMock.Id);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2 }, 4)]
        public void GetGroupsByTutorIdPositiveTest(int[] mockIds, int tutorMockId)
        {
            //Given
            var expected = new List<int>();

            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(tutorMockId).Clone();
            userDtoMock.Id = _userRepo.AddUser(userDtoMock);
            _userRepo.AddRoleToUser(userDtoMock.Id, (int)Role.Tutor);
            Assert.Greater(userDtoMock.Id, 0);
            userDtoMock.Roles = new List<Role> { Role.Tutor };
            _addedUserRoleIdList.Add((userDtoMock.Id, (int)Role.Tutor));
            _addedUserIdList.Add(userDtoMock.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(groupDto.Id);
                expected.Add(groupDto.Id);

                _groupRepo.AddTutorToGroup(userDtoMock.Id, groupDto.Id);
                _addedTutorToGroupIdList.Add((userDtoMock.Id, groupDto.Id));
            }

            //When
            var actual = _groupRepo.GetGroupsByTutorId(userDtoMock.Id);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1, 2 }, 3)]
        public void GetGroupsByTeacherIdPositiveTest(int[] mockIds, int teacherMockId)
        {
            //Given
            var expected = new List<int>();

            var userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(teacherMockId).Clone();


            userDtoMock.Id = _userRepo.AddUser(userDtoMock);
            _userRepo.AddRoleToUser(userDtoMock.Id, (int)Role.Teacher);
            Assert.Greater(userDtoMock.Id, 0);
            userDtoMock.Roles = new List<Role> { Role.Teacher };
            _addedUserRoleIdList.Add((userDtoMock.Id, (int)Role.Teacher));
            _addedUserIdList.Add(userDtoMock.Id);

            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock;
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(groupDto.Id);
                expected.Add(groupDto.Id);

                _groupRepo.AddTeacherGroup(userDtoMock.Id, groupDto.Id);
                _addedTeacherToGroupIdList.Add((userDtoMock.Id, groupDto.Id));
            }

            //When
            var actual = _groupRepo.GetGroupsByTeacherId(userDtoMock.Id);

            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(new int[] { 1, 2 },3,4,2)]
        public void GenerateReportPositiveTest(int[] mockIds, int teacherMockId, int tutorMockId, int studentMockId)
        {
            //Given
            var groupsId = new List<int>();
            var expected = new List<GroupReportDto>();
            //var userDtoMock = new List<UserDto>();

            var teacherDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(teacherMockId).Clone();
            teacherDtoMock.Id = _userRepo.AddUser(teacherDtoMock);
            _userRepo.AddRoleToUser(teacherDtoMock.Id, teacherMockId);
            Assert.Greater(teacherDtoMock.Id, 0);
            teacherDtoMock.Roles = new List<Role> { Role.Teacher };
            _addedUserRoleIdList.Add((teacherDtoMock.Id, (int)Role.Teacher));
            _addedUserIdList.Add(teacherDtoMock.Id);

            var tutorDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(tutorMockId).Clone();
            tutorDtoMock.Id = _userRepo.AddUser(tutorDtoMock);
            _userRepo.AddRoleToUser(tutorDtoMock.Id, (int)Role.Tutor);
            Assert.Greater(tutorDtoMock.Id, 0);
            tutorDtoMock.Roles = new List<Role> { Role.Tutor };
            _addedUserRoleIdList.Add((tutorDtoMock.Id, (int)Role.Tutor));
            _addedUserIdList.Add(tutorDtoMock.Id);

            var studentDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(studentMockId).Clone();
            studentDtoMock.Id = _userRepo.AddUser(studentDtoMock);
            _userRepo.AddRoleToUser(studentDtoMock.Id, (int)Role.Student);
            Assert.Greater(studentDtoMock.Id, 0);
            studentDtoMock.Roles = new List<Role> { Role.Student };
            _addedUserRoleIdList.Add((studentDtoMock.Id, (int)Role.Student));
            _addedUserIdList.Add(studentDtoMock.Id);


            for (int i = 0; i < mockIds.Length; i++)
            {
                var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockIds[i]).Clone();
                groupDto.Course = _courseDtoMock; 
                groupDto.Id = _groupRepo.AddGroup(groupDto);
                _groupIdList.Add(groupDto.Id);
                groupsId.Add(groupDto.Id);


                var groupReportDto = (GroupReportDto)GroupReportMockGetter.GetGroupReportDtoMock(mockIds[i]).Clone();
                groupReportDto.Name = groupDto.Course.Name;
                groupReportDto.GroupId = groupDto.Id;
                groupReportDto.StartDate = groupDto.StartDate;
                groupReportDto.EndDate = groupDto.StartDate.AddDays(7 * _courseDtoMock.Duration);
                _addedGroupReportIdList.Add(groupDto.Id);

                _groupRepo.AddTutorToGroup(tutorDtoMock.Id, groupDto.Id);
                _addedTutorToGroupIdList.Add((tutorDtoMock.Id, groupDto.Id));

                _groupRepo.AddTeacherGroup(teacherDtoMock.Id, groupDto.Id);
                _addedTeacherToGroupIdList.Add((teacherDtoMock.Id, groupDto.Id));

                var studentGroupDtoMock = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(i + 1).Clone();
                studentGroupDtoMock.Group = groupDto;
                studentGroupDtoMock.User = studentDtoMock;
                studentGroupDtoMock.Id = _groupRepo.AddStudentGroup(studentGroupDtoMock);

                _studentGroupIdList.Add((studentDtoMock.Id, groupDto.Id));

                groupReportDto.StudentCount= groupReportDto.StudentCount+1;
                groupReportDto.TutorCount++;
                groupReportDto.TeacherCount++;

                expected.Add(groupReportDto);
            }
            //When
            var actual = _groupRepo.GenerateReport();
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }

        [TearDown]
    public void TearDown()
    { 
      DeleteThemeHomeworks();
      DeleteLessonTheme();
      DeleteUserRoles();
      DeleteCourseThemes();
      DeleteThemeHomeworks();
      DeteleTagHomeworks();
      DeleteTutorGroup();
      DeleteStudentGroups();
      DeleteGroupMaterials();
      DeleteTeacherGroups();
      DeleteMaterials();
      DeleteThemes();
      DeleteLesson();
      DeleteHomeworks();
      DeleteGroups();
      DeleteCourse();
      DeleteUsers();
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
      foreach (var themeHomeworkPair in _themeHomeworkList)
      {
        _homeworkRepo.DeleteHomework_Theme(themeHomeworkPair.Item1, themeHomeworkPair.Item2);
      }
    }
    private void DeleteGroupMaterials()
    {
        foreach (var groupMaterialPair in _groupMaterialIdList)
        {
            _groupRepo.DeleteGroup_Material(groupMaterialPair.Item1, groupMaterialPair.Item2);
        }
    }
    private void DeleteUserRoles()
    {

            _addedUserRoleIdList.ForEach(record =>
            {
                _userRepo.DeleteRoleFromUser(record.Item1, record.Item2);
            });
        }
        private void DeleteCourseThemes()
        {
            foreach (var courseId in _courseIdList)
            {
                _courseRepo.DeleteCourse_Program(courseId);
            }
        }
    private void DeleteStudentGroups()
    {
        foreach (var groupStudentPair in _studentGroupIdList)
        {
            _groupRepo.DeleteStudentGroup(groupStudentPair.Item1, groupStudentPair.Item2);
        }
    }
    private void DeleteTeacherGroups()
    {
        foreach (var groupTeacherPair in _addedTeacherToGroupIdList)
        {
            _groupRepo.DeleteTeacherGroup(groupTeacherPair.Item1, groupTeacherPair.Item2);
        }
    }
    private void DeleteMaterials()
    {
        foreach (var materialId in _materialIdList)
        {
          _materialRepo.HardDeleteMaterial(materialId);
        }
    }
    private void DeleteUsers()
    {
        foreach (var userId in _addedUserIdList)
        {
            _userRepo.HardDeleteUser(userId);
        }
    }

        private void DeleteThemes()
    {
      foreach (var themeId in _themeIdList)
      {
        _courseRepo.HardDeleteTheme(themeId);
      }
    }

    private void DeleteHomeworks()
    {
      foreach (var homeworkId in _homeworkIdList)
      {
        _homeworkRepo.HardDeleteHomework(homeworkId);
      }
    }

    public void DeleteGroups()
    {
      foreach (var groupId in _groupIdList)
      {
        _groupRepo.DeleteGroup(groupId);
      }
    }
        public void DeleteLesson()
        {
            foreach (var lessonId in _lessonIdList)
            {
                _lessonRepo.HardDeleteLesson(lessonId);
            }
        }

        public void DeleteTutorGroup()
        {
            foreach (var tutorGroupPair in _addedTutorToGroupIdList)
            {
                _groupRepo.DeleteTutorGroup(tutorGroupPair.Item1, tutorGroupPair.Item2);
            }
        }
        public void DeleteCourse()
    {
      foreach (var courseId in _courseIdList)
      {
        _courseRepo.HardDeleteCourse(courseId);
      }
    }
        
        public void DeleteLessonTheme()
        {
            foreach (var lessonThemePair in _lessonThemeIdList)
            {
                _lessonRepo.DeleteLessonTheme(lessonThemePair.Item1, lessonThemePair.Item2);
            }
        }

    }
}

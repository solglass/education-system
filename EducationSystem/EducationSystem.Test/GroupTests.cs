using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
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
        private IMaterialRepository _materialRepo;


        private List<int> _homeworkIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;
        private List<int> _themeIdList;
        private List<int> _lessonIdList;
        private List<int> _materialIdList;
        private List<int> _tagIdList;
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

        private CourseDto _courseDtoMock;
        private GroupDto _groupDtoMock;

        private List<int> _groupsId;
        private GroupRepository gRepo;

        [SetUp]
        public void SetUp()
        {
            _groupRepo = new GroupRepository(_options);
            _homeworkRepo = new HomeworkRepository(_options);
            _courseRepo = new CourseRepository(_options);
            _tagRepo = new TagRepository(_options);
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
            _tagIdList = new List<int>();
            _themeHomeworkList = new List<(int, int)>();
            _courseThemeIdList = new List<(int, int)>();
            _tagHomeworkList = new List<(int, int)>();
            _addedUserRoleIdList = new List<(int, int)>();
            _addedTutorToGroupIdList = new List<(int, int)>();
            _groupMaterialIdList = new List<(int, int)>();
            _addedUserIdList = new List<int>();
            _studentGroupIdList = new List<(int, int)>();
            _addedTeacherToGroupIdList = new List<(int, int)>();

            _courseDtoMock = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            _courseDtoMock.Id = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(_courseDtoMock.Id);
        }

        

       

        [TestCase(new int[] { 1,2,3 }, new int[] { 3,4},1)]
        public void AddTeacherGroupPositiveTest(int[] groupWithTeacherMockIds, int[] groupWithoutTeacherMockIds, int userMockId)
        {
            //Given
            var teacher = (UserDto)UserMockGetter.GetUserDtoMock(userMockId).Clone();
            teacher.Id = _userRepo.AddUser(teacher);
            _addedUserIdList.Add(teacher.Id);

            var expected = new List<int>();
            foreach (var groupMockId in groupWithTeacherMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
                expected.Add(group.Id);
            }

            foreach (var groupMockId in groupWithoutTeacherMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
            }
            //When
            foreach (var group in expected)
            {
                var result = _groupRepo.AddTeacherGroup(teacher.Id,group);
                _addedTeacherToGroupIdList.Add((teacher.Id, group)); 
            }
            var actual = _groupRepo.GetGroupsByTeacherId(teacher.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        public void AddTeacherGroupNegativeTestTeacherNotExists(int groupMockId)
        {
            //Given
            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);
            //When
            try
            {
                var result = _groupRepo.AddTeacherGroup(-1, group.Id);
                _addedTeacherToGroupIdList.Add((-1, group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddTeacherGroupNegativeTestGroupNotExists(int teacherMockId)
        {
            //Given
            var teacher = (UserDto)UserMockGetter.GetUserDtoMock(teacherMockId).Clone();
            teacher.Id = _userRepo.AddUser(teacher);
            _addedUserIdList.Add(teacher.Id);
            //When
            try
            {
                var result = _groupRepo.AddTeacherGroup(teacher.Id, -1);
                _addedTeacherToGroupIdList.Add((teacher.Id, -1));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1,1)]
        public void AddTeacherGroupNegativeTestNotUniqueEntity(int teacherMockId, int groupMockId)
        {
            //Given
            var teacher = (UserDto)UserMockGetter.GetUserDtoMock(teacherMockId).Clone();
            teacher.Id = _userRepo.AddUser(teacher);
            _addedUserIdList.Add(teacher.Id);

            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);

             _groupRepo.AddTeacherGroup(teacher.Id, group.Id);
            _addedTeacherToGroupIdList.Add((teacher.Id, group.Id));
            //When
            try
            {
                _groupRepo.AddTeacherGroup(teacher.Id, group.Id);
                _addedTeacherToGroupIdList.Add((teacher.Id, group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 4 }, 1)]
        public void DeleteTeacherGroupPositiveTest(int[] groupToDeleteMockIds, int[] groupToSaveMockIds, int teacherMockId)
        {
            //Given
            var teacher = (UserDto)UserMockGetter.GetUserDtoMock(teacherMockId).Clone();
            teacher.Id = _userRepo.AddUser(teacher);
            _addedUserIdList.Add(teacher.Id);

            var expected = new List<int>();
            foreach (var groupMockId in groupToSaveMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
                expected.Add(group.Id);
                _groupRepo.AddTeacherGroup(teacher.Id, group.Id);
                _addedTeacherToGroupIdList.Add((teacher.Id, group.Id));
            }

            var groupToDeleteIds = new List<int>();
            foreach (var groupMockId in groupToDeleteMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
            }
            //When
            foreach (var group in groupToDeleteIds)
            {
                var result = _groupRepo.DeleteTeacherGroup(teacher.Id, group);
                Assert.AreEqual(1, result);
            }
            var actual = _groupRepo.GetGroupsByTeacherId(teacher.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void DeleteTeacherGroupNegativeTestEntityNotExists()
        {
            //Given

            //When
            var result = _groupRepo.DeleteTeacherGroup(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }





        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 4 }, 1)]
        public void AddTutorGroupPositiveTest(int[] groupWithTutorMockIds, int[] groupWithoutTutorMockIds, int userMockId)
        {
            //Given
            var tutor = (UserDto)UserMockGetter.GetUserDtoMock(userMockId).Clone();
            tutor.Id = _userRepo.AddUser(tutor);
            _addedUserIdList.Add(tutor.Id);

            var expected = new List<int>();
            foreach (var groupMockId in groupWithTutorMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
                expected.Add(group.Id);
            }

            foreach (var groupMockId in groupWithoutTutorMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
            }
            //When
            foreach (var group in expected)
            {
                _groupRepo.AddTutorToGroup(tutor.Id, group);
                _addedTutorToGroupIdList.Add((tutor.Id, group));
            }
            var actual = _groupRepo.GetGroupsByTutorId(tutor.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [TestCase(1)]
        public void AddTutorGroupNegativeTestTutorNotExists(int groupMockId)
        {
            //Given
            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);
            //When
            try
            {
                var result = _groupRepo.AddTutorToGroup(-1, group.Id);
                _addedTutorToGroupIdList.Add((-1, group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddTutorGroupNegativeTestGroupNotExists(int tutorMockId)
        {
            //Given
            var tutor = (UserDto)UserMockGetter.GetUserDtoMock(tutorMockId).Clone();
            tutor.Id = _userRepo.AddUser(tutor);
            _addedUserIdList.Add(tutor.Id);
            //When
            try
            {
                var result = _groupRepo.AddTutorToGroup(tutor.Id, -1);
                _addedTutorToGroupIdList.Add((tutor.Id, -1));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1, 1)]
        public void AddTutorGroupNegativeTestNotUniqueEntity(int tutorMockId, int groupMockId)
        {
            //Given
            var tutor = (UserDto)UserMockGetter.GetUserDtoMock(tutorMockId).Clone();
            tutor.Id = _userRepo.AddUser(tutor);
            _addedUserIdList.Add(tutor.Id);

            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);

            _groupRepo.AddTutorToGroup(tutor.Id, group.Id);
            _addedTutorToGroupIdList.Add((tutor.Id, group.Id));
            //When
            try
            {
                _groupRepo.AddTutorToGroup(tutor.Id, group.Id);
                _addedTutorToGroupIdList.Add((tutor.Id, group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 4 }, 1)]
        public void DeleteTutorGroupPositiveTest(int[] groupToDeleteMockIds, int[] groupToSaveMockIds, int tutorMockId)
        {
            //Given
            var tutor = (UserDto)UserMockGetter.GetUserDtoMock(tutorMockId).Clone();
            tutor.Id = _userRepo.AddUser(tutor);
            _addedUserIdList.Add(tutor.Id);

            var expected = new List<int>();
            foreach (var groupMockId in groupToSaveMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
                expected.Add(group.Id);
                 _groupRepo.AddTutorToGroup(tutor.Id, group.Id);
                _addedTutorToGroupIdList.Add((tutor.Id, group.Id));
            }

            var groupToDeleteIds = new List<int>();
            foreach (var groupMockId in groupToDeleteMockIds)
            {
                var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
                group.Course = _courseDtoMock;
                group.Id = _groupRepo.AddGroup(group);
                _groupIdList.Add(group.Id);
            }
            //When
            foreach (var group in groupToDeleteIds)
            {
                var result = _groupRepo.DeleteTutorGroup(tutor.Id, group);
                Assert.AreEqual(1, result);
            }
            var actual = _groupRepo.GetGroupsByTutorId(tutor.Id);
            //Then
            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void DeleteTutorGroupNegativeTestEntityNotExists()
        {
            //Given

            //When
            var result = _groupRepo.DeleteTutorGroup(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }

        [TestCase( 1, 1 )]
        public void AddStudentGroupPositiveTest(int groupMockId, int studentMockId)
        {
            //Given
            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);

            var student = (UserDto)UserMockGetter.GetUserDtoMock(studentMockId).Clone();
            student.Id = _userRepo.AddUser(student);
            _addedUserIdList.Add(student.Id);

            var expected =new StudentGroupDto { 
                Group=group,
                User=student,
                ContractNumber=1
                };
           
            //When, Then
            
            expected.Id = _groupRepo.AddStudentGroup(expected);
            _studentGroupIdList.Add((expected.User.Id, expected.Group.Id));
            var actual = _groupRepo.GetStudentGroupByGroupAndUserIds(expected.Group.Id,expected.User.Id);
            Assert.AreEqual(expected, actual);

        }
        [TestCase(1)]
        public void AddStudentGroupNegativeTestUserNotExists(int groupMockId)
        {
            //Given
            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);

            var expected = new StudentGroupDto
            {
                Group = group,
                User = new UserDto { Id=-1},
                ContractNumber = 1
            };
            //When,Then
            try
            {
                expected.Id = _groupRepo.AddStudentGroup(expected);
                _studentGroupIdList.Add((expected.User.Id, expected.Group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1)]
        public void AddStudentGroupNegativeTestGroupNotExists(int studentMockId)
        {
            //Given
            var student = (UserDto)UserMockGetter.GetUserDtoMock(studentMockId).Clone();
            student.Id = _userRepo.AddUser(student);
            _addedUserIdList.Add(student.Id);

            var expected = new StudentGroupDto
            {
                Group = new GroupDto { Id=-1},
                User = student,
                ContractNumber = 1
            };
            //When,Then
            try
            {
                expected.Id = _groupRepo.AddStudentGroup(expected);
                _studentGroupIdList.Add((expected.User.Id, expected.Group.Id));
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1, new int[] { 1, 2 })]
        public void DeleteStudentGroupPositiveTest(int groupMockId, int[] studentMockIds)
        {
            //Given
            var group = (GroupDto)GroupMockGetter.GetGroupDtoMock(groupMockId).Clone();
            group.Course = _courseDtoMock;
            group.Id = _groupRepo.AddGroup(group);
            _groupIdList.Add(group.Id);

            var studentToRemove = (UserDto)UserMockGetter.GetUserDtoMock(studentMockIds[0]).Clone();
            studentToRemove.Id = _userRepo.AddUser(studentToRemove);
            _addedUserIdList.Add(studentToRemove.Id);

            var studentGroupToRemove = new StudentGroupDto
            {
                Group = group,
                User = studentToRemove,
                ContractNumber = 1
            };

            var studentToSave = (UserDto)UserMockGetter.GetUserDtoMock(studentMockIds[1]).Clone();
            studentToSave.Id = _userRepo.AddUser(studentToSave);
            _addedUserIdList.Add(studentToSave.Id);

            var studentGroupToSave = new StudentGroupDto
            {
                Group = group,
                User = studentToSave,
                ContractNumber = 2
            };

            studentGroupToSave.Id = _groupRepo.AddStudentGroup(studentGroupToSave);
            _studentGroupIdList.Add((studentGroupToSave.User.Id, studentGroupToSave.Group.Id));
            studentGroupToRemove.Id = _groupRepo.AddStudentGroup(studentGroupToRemove);
            _studentGroupIdList.Add((studentGroupToRemove.User.Id, studentGroupToRemove.Group.Id));

            //When, then
            var result = _groupRepo.DeleteStudentGroup(studentGroupToRemove.User.Id, studentGroupToRemove.Group.Id);
            Assert.AreEqual(1, result);

            var actual = _groupRepo.GetStudentGroupByGroupAndUserIds(studentGroupToSave.Group.Id, studentGroupToSave.User.Id);
            Assert.AreEqual(studentGroupToSave, actual);

            actual = _groupRepo.GetStudentGroupByGroupAndUserIds(studentGroupToRemove.Group.Id, studentGroupToRemove.User.Id);
            Assert.IsNull(actual);
        }
        [Test]
        public void DeleteStudentGroupNegativeTestEntityNotExists()
        {
            //Given
            //When
            var result = _groupRepo.DeleteStudentGroup(-1, -1);
            //Then
            Assert.AreEqual(0, result);
        }

        [TearDown]
            public void TearDown()
            { 
              DeleteThemeHomeworks();
              DeleteLessonTheme();
              DeleteThemeHomeworks();
              DeteleTagHomeworks();
              DeleteTutorGroup();
              DeleteStudentGroups();
              DeleteGroupMaterials();
              DeleteTeacherGroups();
              DeleteMaterials();
              DeleteThemes();
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
        private void DeleteUsers()
        {
            foreach (var user in _addedUserIdList)
            {
                _userRepo.HardDeleteUser(user);
            }
        }
    private void DeleteThemeHomeworks()
    {
      foreach (var theneHomeworkPair in _themeHomeworkList)
      {
        _homeworkRepo.DeleteHomework_Theme(theneHomeworkPair.Item1, theneHomeworkPair.Item2);
      }
    }
    private void DeleteGroupMaterials()
    {
        foreach (var groupMaterialPair in _groupMaterialIdList)
        {
            _groupRepo.DeleteGroup_Material(groupMaterialPair.Item1, groupMaterialPair.Item2);
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

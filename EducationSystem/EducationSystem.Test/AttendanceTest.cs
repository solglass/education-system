using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class AttendanceTest : BaseTest
    {
        private ILessonRepository _lessonRepository;
        private IUserRepository _userRepository;
        private ICourseRepository _courseRepository;
        private IGroupRepository _groupRepository;

        private List<int> _addedCourseIds;
        private List<int> _addedUserIds;
        private List<int> _addedGroupIds;
        private List<int> _addedLessonIds;
        private List<int> _addedAttendanceIds;

        private GroupDto _group;
        private List<LessonDto> _lessons;
        private List<UserDto> _students;

        private const int _amountLessons = 4;
        private const int _amountStudents = 5;

        [OneTimeSetUp]
        public void AttendanceOneTimeSetUp()
        {
            _userRepository = new UserRepository(_options);
            _courseRepository = new CourseRepository(_options);
            _groupRepository = new GroupRepository(_options);
            _lessonRepository = new LessonRepository(_options);

            _addedUserIds = new List<int>();
            _addedCourseIds = new List<int>();
            _addedGroupIds = new List<int>();
            _addedLessonIds = new List<int>();
            _addedAttendanceIds = new List<int>();

            _group = AddGroup(1);

            _lessons = new List<LessonDto>();
            for (int i = 0; i < _amountLessons; i++) 
            {
                _lessons.Add(AddLesson(i+1));
            }

            _students = new List<UserDto>();
            for (int i = 0; i < _amountStudents; i++) 
            {
                _students.Add(AddUser(i+1));
            }
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 4)]
        public void AddAttendancePositiveTest(int mockLessonId, int mockUserId)
        {
            var dto = AddAttendance(mockLessonId, mockUserId);

            var actual = _lessonRepository.GetAttendanceById(dto.Id);

            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        public void DeleteAttendancePositiveTest(int mockLessonId, int mockUserId)
        {
            var dto = AddAttendance(mockLessonId, mockUserId);

            var affectedRows = _lessonRepository.DeleteAttendance(dto.Id);
            var actual = _lessonRepository.GetAttendanceById(dto.Id);

            Assert.AreEqual(1, affectedRows);
            Assert.IsNull(actual);
        }

        [TestCase(1, 1, false, "Tupaya otmazka")]
        [TestCase(2, 1, true, null)]
        public void UpdateAttendancePositiveTest(int mockLessonId, int mockUserId, bool isAbsent, string reason)
        {
            var dto = AddAttendance(mockLessonId, mockUserId);
            dto.IsAbsent = isAbsent;
            dto.ReasonOfAbsence = reason;

            var affectedRows = _lessonRepository.UpdateAttendance(dto);
            var actual = _lessonRepository.GetAttendanceById(dto.Id);

            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, 2)]
        public void GetAttendancesByLessonIdPositiveTest(int findLessonMockId, int otherLessonMockId)
        {
            List<AttendanceDto> attendanceFindLesson = new List<AttendanceDto>();
            List<AttendanceDto> attendanceOtherLesson = new List<AttendanceDto>();
            for (int i =1; i <= _amountStudents; i++)
            {
                attendanceFindLesson.Add(AddAttendance(findLessonMockId, i));
                attendanceOtherLesson.Add(AddAttendance(otherLessonMockId, i));
            }

            var actual = _lessonRepository.GetAttendancesByLessonId(_lessons[findLessonMockId-1].Id);

            CollectionAssert.AreEqual(attendanceFindLesson, actual);
        }

        [TestCase(1, 5, 2)]
        public void GetAttendancesByUserIdPositiveTest(int findUserMockId, int otherUserMockId, int amountLesson)
        {
            List<AttendanceDto> attendanceFindUser = new List<AttendanceDto>();
            List<AttendanceDto> attendanceOtherUser = new List<AttendanceDto>();
            for (int i = 1; i <= amountLesson; i++)
            {
                attendanceFindUser.Add(AddAttendance(i, findUserMockId));
                attendanceOtherUser.Add(AddAttendance(i, otherUserMockId));
            }

            var actual = _lessonRepository.GetAttendancesByUserId(_students[findUserMockId-1].Id);

            CollectionAssert.AreEqual(attendanceFindUser, actual);
        }

        [TestCase(1, 1, 0)] // незаполненные данные о посещении
        [TestCase(0, 1, 1)] // пустой урок
        [TestCase(1, _amountStudents+5, 1)] //несуществующий юзер
        public void AddAttendanceNegativeTest(int mockLessonId, int mockUserId, int mockAttendanceId)
        {
            try
            {
                var dto = AddAttendance(mockLessonId, mockUserId, mockAttendanceId);
            }
            catch 
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(1, 1, 1)]
        [TestCase(1, 1, 2)]
        public void AddAttendanceDoubleNegativeTest(int mockLessonId, int mockUserId, int mockAttendanceId)
        {
            try
            {
                var dto = AddAttendance(mockLessonId, mockUserId);
                var dtoCopy = AddAttendance(mockLessonId, mockUserId, mockAttendanceId);
            }
            catch 
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [Test]
        public void UpdateAttendanceNegativeTest() 
        {
            var dto = AddAttendance(1, 1);
            try
            {
                _lessonRepository.UpdateAttendance(null);
            }
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }



        [TearDown]
        public void AttendanceTearDown()
        {
            DeleteAttendances();
        }

        [OneTimeTearDown]
        public void AttendanceOneTimeTearDown()
        {
            DeleteAttendances();
            DeleteUsers();
            DeleteLessons();
            DeleteGroups();
            DeleteCourses();
        }

        private AttendanceDto AddAttendance(int mockLessonId, int mockStudentId, int mockAttendanceId=1)
        {
            var attendanceDto = (AttendanceDto)AttendanceMockGetter.GetAttendance(mockAttendanceId).Clone();
            attendanceDto.Lesson = _lessons[mockLessonId - 1];
            attendanceDto.User = _students[mockStudentId - 1];
            attendanceDto.Id = _lessonRepository.AddAttendance(attendanceDto);
            Assert.Greater(attendanceDto.Id, 0);
            _addedAttendanceIds.Add(attendanceDto.Id);
            return attendanceDto;
        }

        private UserDto AddUser(int mockId)
        {
            var userDto = UserMockGetter.GetUserDtoMock(mockId);
            userDto.Id = _userRepository.AddUser(userDto);
            Assert.Greater(userDto.Id, 0);
            _addedUserIds.Add(userDto.Id);
            return userDto;
        }

        private LessonDto AddLesson(int mockId)
        {
            var dtoLesson = (LessonDto)LessonMockGetter.GetLessonDtoMock(mockId).Clone();
            dtoLesson.Group = _group;
            dtoLesson.Id = _lessonRepository.AddLesson(dtoLesson);
            Assert.Greater(dtoLesson.Id, 0);
            _addedLessonIds.Add(dtoLesson.Id);
            return dtoLesson;
        }

        private GroupDto AddGroup(int mockId)
        {
            var dtoGroup = (GroupDto)GroupMockGetter.GetGroupDtoMock(mockId).Clone();
            var dtoCourse = AddCourse(mockId);
            dtoGroup.Course = dtoCourse;
            dtoGroup.Id = _groupRepository.AddGroup(dtoGroup);
            Assert.Greater(dtoGroup.Id, 0);
            _addedGroupIds.Add(dtoGroup.Id);
            return dtoGroup;
        }

        private CourseDto AddCourse(int mockId)
        {
            var dtoCourse = (CourseDto)CourseMockGetter.GetCourseDtoMock(mockId).Clone();
            dtoCourse.Id = _courseRepository.AddCourse(dtoCourse);
            Assert.Greater(dtoCourse.Id, 0);
            _addedCourseIds.Add(dtoCourse.Id);
            return dtoCourse;
        }

        private void DeleteCourses()
        {
            _addedCourseIds.ForEach(id =>
            {
                _courseRepository.HardDeleteCourse(id);
            });
        }
        private void DeleteGroups()
        {
            _addedGroupIds.ForEach(id =>
            {
                _groupRepository.HardDeleteGroup(id);
            });
        }
        public void DeleteLessons()
        {
            _addedLessonIds.ForEach(id =>
            {
                _lessonRepository.HardDeleteLesson(id);
            });
        }
        public void DeleteUsers()
        {
            _addedUserIds.ForEach(id =>
            {
                _userRepository.HardDeleteUser(id);
            });
        }
        public void DeleteAttendances()
        {
            _addedAttendanceIds.ForEach(id =>
            {
                _lessonRepository.DeleteAttendance(id);
            });
        }
    }
}

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
        }

        [TestCase(1)]
        public void AddAttendancePositiveTest(int mockId)
        {
            var dto = AddAttendance(mockId);

            var actual = _lessonRepository.GetAttendanceById(dto.Id);

            Assert.AreEqual(dto, actual);
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

        private AttendanceDto AddAttendance(int mockId)
        {
            var attendanceDto = (AttendanceDto)AttendanceMockGetter.GetAttendance(mockId).Clone();
            attendanceDto.Lesson = AddLesson(mockId);
            attendanceDto.User = AddUser(mockId);
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
            var dtoGroup = AddGroup(mockId);
            dtoLesson.Group = dtoGroup;
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

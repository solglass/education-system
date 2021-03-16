using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class FeedbackTest : BaseTest
    {
        private ILessonRepository _lessonRepo;
        private IUserRepository _userRepo;
        private IGroupRepository _groupRepo;
        private ICourseRepository _courseRepo;

        private List<int> _feedbackIdList;
        private List<int> _lessonIdList;
        private List<int> _userIdList;
        private List<int> _groupIdList;
        private List<int> _courseIdList;

        private UserDto _userDtoMock;
        private LessonDto _lessonDtoMock;
        private GroupDto _groupDtoMock;
        private CourseDto _courseDtoMock;

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {

            _groupRepo = new GroupRepository(_options);
            _lessonRepo = new LessonRepository(_options);
            _userRepo= new UserRepository(_options);
            _courseRepo = new CourseRepository(_options);

            _feedbackIdList = new List<int>();
            _courseIdList = new List<int>();
            _groupIdList = new List<int>();
            _lessonIdList = new List<int>();
            _userIdList = new List<int>();

            _userDtoMock = UserMockGetter.GetUserDtoMock(1);
            var addedUserId = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(addedUserId);
            _userDtoMock.Id = addedUserId;

            _courseDtoMock = CourseMockGetter.GetCourseDtoMock(1);
            var addedCourseId = _courseRepo.AddCourse(_courseDtoMock);
            _courseIdList.Add(addedCourseId);
            _courseDtoMock.Id = addedCourseId;

            _groupDtoMock = GroupMockGetter.GetGroupDtoMock(1);
            _groupDtoMock.Course = _courseDtoMock;
            var addedGroupId = _groupRepo.AddGroup(_groupDtoMock);
            _groupIdList.Add(addedGroupId);
            _groupDtoMock.Id = addedGroupId;

            _lessonDtoMock = LessonMockGetter.GetLessonDtoMock(1);
            _lessonDtoMock.Group = _groupDtoMock;
            var addedLessonId = _lessonRepo.AddLesson(_lessonDtoMock);
            _lessonIdList.Add(addedLessonId);
            _lessonDtoMock.Id = addedLessonId;
        }

        [TestCase(1)]
        public void AddFeedbackPositiveTest(int mockId)
        {
            //Given
            var dto = (FeedbackDto)FeedbackMockGetter.GetFeedbackDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Lesson= _lessonDtoMock;

            var addedFeedbackId = _lessonRepo.AddFeedback(dto);
            Assert.Greater(addedFeedbackId, 0);

            _feedbackIdList.Add(addedFeedbackId);
            dto.Id = addedFeedbackId;

            //When
            var actual = _lessonRepo.GetFeedbackById(addedFeedbackId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        [TestCase(1)]
        public void UpdateFeedbackPositiveTest(int mockId)
        {
            //Given
            var dto = (FeedbackDto)FeedbackMockGetter.GetFeedbackDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Lesson = _lessonDtoMock;
            var addedFeedbackId = _lessonRepo.AddFeedback(dto);
            _feedbackIdList.Add(addedFeedbackId);

            dto = new FeedbackDto
            {
                Id = addedFeedbackId,
                User = dto.User,
                Lesson = dto.Lesson,
                Message = "Update test",
                UnderstandingLevel = Core.Enums.UnderstandingLevel.Medium
            };

            _lessonRepo.UpdateFeedback(dto);

            //When
            var actual = _lessonRepo.GetFeedbackById(addedFeedbackId);

            //Then
            Assert.AreEqual(dto, actual);

        }

        [TestCase(1)]
        public void DeleteFeedbackPositiveTest(int mockId)
        {
            //Given
            var dto = (FeedbackDto)FeedbackMockGetter.GetFeedbackDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Lesson = _lessonDtoMock;

            var addedFeedbackId = _lessonRepo.AddFeedback(dto);
            _feedbackIdList.Add(addedFeedbackId);
            dto.Id = addedFeedbackId;

            var amountDeleted = _lessonRepo.DeleteFeedback(dto.Id); 

            Assert.Greater(amountDeleted, 0);

            //When
            var actual = _lessonRepo.GetFeedbackById(addedFeedbackId);

            //Then
            Assert.IsNull(actual);

        }

        [OneTimeTearDown]
        public void TearDowTest()
        {
            DeleteFeedbacks();

            DeleteUsers();

            DeleteLessons();
            DeleteGroups();
            DeleteCourse();

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
        public void DeleteLessons()
        {
            foreach (int lessonId in _lessonIdList)
            {
                _lessonRepo.HardDeleteLesson(lessonId);
            }
        }

        public void DeleteUsers()
        {
            foreach (int userId in _userIdList)
            {
                _userRepo.HardDeleteUser(userId);
            }
        }
        public void DeleteFeedbacks()
        {
            foreach (int feedBackId in _feedbackIdList)
            {
                _lessonRepo.DeleteFeedback(feedBackId);
            }
        }
    }
}

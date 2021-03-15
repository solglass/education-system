using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
   public class PaymentTests: BaseTest
    {
        private UserRepository _userRepository;
        private GroupRepository _groupRepository;
        private CourseRepository _courseRepository;
        private PaymentRepository _repository;


        private List<int> _addedUserDtoIds;
        private List<int> _addedPaymentDtoIds;
        private List<int> _addedGroupDtoIds;
        private List<int> _addedCourseDtoIds;
        private List<(int, int)> _addedStudentGroupDtoIds;
        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            _groupRepository = new GroupRepository(_options);
            _userRepository = new UserRepository(_options);
            _repository = new PaymentRepository(_options);
            _courseRepository = new CourseRepository(_options);

            _addedUserDtoIds = new List<int>();
            _addedGroupDtoIds = new List<int>();
            _addedPaymentDtoIds = new List<int>();
            _addedCourseDtoIds = new List<int>();
            _addedStudentGroupDtoIds = new List<(int, int)>();

        }
        [TestCase(1)]
        public void AddPaymentPositiveTest(int mockId)
        {
            //Given
            var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
            var userDto = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var studentGroupDto = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(1).Clone();


            var addedUserId = _userRepository.AddUser(userDto);
            var addedCourseId = _courseRepository.AddCourse(courseDto);
            Assert.Greater(addedCourseId, 0);
            courseDto.Id = addedCourseId;
            groupDto.Course = courseDto;

            var addedGroupId = _groupRepository.AddGroup(groupDto);
            userDto.Id = addedUserId;
            studentGroupDto.User.Id = addedUserId;
            studentGroupDto.Group.Course = courseDto;
            studentGroupDto.Group.Id = addedGroupId;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDto);
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);
            Assert.Greater(addedStudentGroupId, 0);

            dto.Student = userDto;

            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);


            //When
            var addedEntityId = _repository.AddPayment(dto);

            // Then

            _addedPaymentDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            var actual = _repository.GetPaymentById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }
        /*
        [TestCase(1)]
        public void GetPaymentByContractNumberPositiveTest(int mockId)
        {
            throw new NotImplementedException();
        }

        [TestCase(1)]
        public void GetPaymentByIdPositiveTest(int mockId)
        {
            throw new NotImplementedException();
        }

        [TestCase(1)]
        public void GetPaymentsByPeriodPositiveTest(int mockId)
        {
            throw new NotImplementedException();
        }

        [TestCase(1)]
        public void GetPaymentsByUserIdPositiveTest(int mockId)
        {
            throw new NotImplementedException();
        }

        [TestCase(1)]
        public void UpdatePaymentPositiveTest(int mockId)
        {
            throw new NotImplementedException();
        }

        */
        [OneTimeTearDown]
        public void TearDowTest()
        {
            _addedUserDtoIds.ForEach(id =>
            {
                _userRepository.HardDeleteUser(id);
            });

            _addedPaymentDtoIds.ForEach(id =>
            {
                _repository.DeletePayment(id);
            });


            _addedGroupDtoIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup(id);
            });


            _addedStudentGroupDtoIds.ForEach(record => 
            _groupRepository.DeleteStudentGroup(record.Item1, record.Item2));

            _addedCourseDtoIds.ForEach(id =>
           _courseRepository.HardDeleteCourse(id));
        }
    }
 
}

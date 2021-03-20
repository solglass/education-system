using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class PaymentTests : BaseTest
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
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
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
            studentGroupDto.ContractNumber = dto.ContractNumber;
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

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void GetPaymentByIdPositiveTest(int mockId)
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
            studentGroupDto.ContractNumber = dto.ContractNumber;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDto);
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);
            Assert.Greater(addedStudentGroupId, 0);

            dto.Student = userDto;

            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);

            var addedEntityId = _repository.AddPayment(dto);

            _addedPaymentDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            //When
            var actual = _repository.GetPaymentById(addedEntityId);


            // Then
            Assert.AreEqual(dto, actual);
        }




        [TestCase(3, 99)]
        [TestCase(2, 11)]
        public void GetPaymentByContractNumberPositiveTest(int numberOfPayments, int contractNumber)
        {

            //Given
            var expected = new List<PaymentDto>();
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
            studentGroupDto.ContractNumber = contractNumber;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDto);
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);
            Assert.Greater(addedStudentGroupId, 0);


            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);

            for (int mockId = 1; mockId <= numberOfPayments; mockId++)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
                userDto.Login = null;
                userDto.Password = null;
                dto.Student = userDto;
                dto.ContractNumber = contractNumber;
                var addedEntityId = _repository.AddPayment(dto);

                _addedPaymentDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);

            }
            //When

            var actual = _repository.GetPaymentByContractNumber(contractNumber);

            // Then
            CollectionAssert.AreEqual(expected, actual, new PaymentComparer());
        }




        [TestCase(3, "2021.01", "2021.02")]
        public void GetPaymentsByPeriodPositiveTest(int numberOfPayments, string periodFrom, string periodTo)
        {
            //Given
            var expected = new List<PaymentDto>();
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


            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);

            for (int mockId = 1; mockId <= numberOfPayments; mockId++)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
                userDto.Login = null;
                userDto.Password = null;
                dto.Student = userDto;
                var addedEntityId = _repository.AddPayment(dto);

                _addedPaymentDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);

            }
            //When

            var actual = _repository.GetPaymentsByPeriod(periodFrom, periodTo);

            // Then
            CollectionAssert.AreEqual(expected, actual, new PaymentComparer());
        }

        [TestCase(6)]
        public void GetPaymentsByUserIdPositiveTest(int numberOfPayments)
        {
            //Given
            var expected = new List<PaymentDto>();
            var userDto = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();

            var studentGroupDtoFirst = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(1).Clone();
            var studentGroupDtoSecond = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(2).Clone();


            var addedUserId = _userRepository.AddUser(userDto);
            var addedCourseId = _courseRepository.AddCourse(courseDto);
            Assert.Greater(addedCourseId, 0);

            courseDto.Id = addedCourseId;
            groupDto.Course = courseDto;

            var addedGroupId = _groupRepository.AddGroup(groupDto);
            userDto.Id = addedUserId;
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);

            studentGroupDtoFirst.User.Id = addedUserId;
            studentGroupDtoFirst.Group.Course = courseDto;
            studentGroupDtoFirst.Group.Id = addedGroupId;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDtoFirst);
            Assert.Greater(addedStudentGroupId, 0);
            studentGroupDtoSecond.User.Id = addedUserId;
            studentGroupDtoSecond.Group.Course = courseDto;
            studentGroupDtoSecond.Group.Id = addedGroupId;
            addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDtoSecond);

            Assert.Greater(addedStudentGroupId, 0);


            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);

            for (int mockId = 1; mockId <= numberOfPayments; mockId++)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
                userDto.Login = null;
                userDto.Password = null;
                dto.Student = userDto;
                var addedEntityId = _repository.AddPayment(dto);

                _addedPaymentDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
                expected.Add(dto);

            }

            //When

            var actual = _repository.GetPaymentsByUserId(addedUserId);

            // Then
            CollectionAssert.AreEqual(expected, actual, new PaymentComparer());
        }

        [TestCase(1)]
        public void UpdatePaymentPositiveTest(int mockId)
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
            studentGroupDto.ContractNumber = dto.ContractNumber;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDto);
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);
            Assert.Greater(addedStudentGroupId, 0);

            dto.Student = userDto;

            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);

            var addedEntityId = _repository.AddPayment(dto);
            _addedPaymentDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;
            dto.Amount = 4444;
            dto.Period = "2021.02";
            dto.IsPaid = true;

            //When
            _repository.UpdatePayment(dto);

            // Then
            var actual = _repository.GetPaymentById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        public void GetStudentsNotPaidInMonthPositiveTest (int mockId)
        { Assert.Fail(); }

        [TearDown]
        public void TearDowTest()
        {
            _addedStudentGroupDtoIds.ForEach(record =>
            _groupRepository.DeleteStudentGroup(record.Item1, record.Item2));


            _addedGroupDtoIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup(id);
            });


            _addedCourseDtoIds.ForEach(id =>
           _courseRepository.HardDeleteCourse(id));

            _addedUserDtoIds.ForEach(id =>
            {
                _userRepository.HardDeleteUser(id);
            });

            _addedPaymentDtoIds.ForEach(id =>
            {
                _repository.DeletePayment(id);
            });

        }
    }

    public class PaymentComparer : System.Collections.IComparer
    {
        public PaymentComparer()
        {

        }

        public int Compare(object expected, object actual)
        {
            if (expected is PaymentDto && actual is PaymentDto)
            {

                var paymentDtoExpected = (PaymentDto)expected;
                var paymentDtoActual = (PaymentDto)actual;

                var userDtoExpected = (UserDto)paymentDtoExpected.Student;
                var userDtoActual = (UserDto)paymentDtoActual.Student;

                if (
                    ((paymentDtoExpected.Id == paymentDtoActual.Id) &&
                (paymentDtoExpected.ContractNumber == paymentDtoActual.ContractNumber) &&
                (paymentDtoExpected.Amount == paymentDtoActual.Amount) &&
                (paymentDtoExpected.Date == paymentDtoActual.Date) &&
                (paymentDtoExpected.IsPaid == paymentDtoActual.IsPaid) &&
                string.Equals(paymentDtoExpected.Period, paymentDtoActual.Period)) &&

                ((userDtoExpected.Id == userDtoActual.Id) &&
                string.Equals(userDtoExpected.FirstName, userDtoActual.FirstName) &&
                string.Equals(userDtoExpected.LastName, userDtoActual.LastName) &&
                string.Equals(userDtoExpected.Phone, userDtoActual.Phone) &&
                string.Equals(userDtoExpected.Email, userDtoActual.Email) &&
                (userDtoExpected.IsDeleted == userDtoActual.IsDeleted))
                    )

                    return 0;
                else return 1;
            }
            else if (expected.Equals(actual)) return 0;
            return 1;
        }

    }

}

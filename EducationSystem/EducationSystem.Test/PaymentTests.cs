using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests
{
    [ExcludeFromCodeCoverage]
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
            SetupRelatedEntititesRegulary(dto);

            //When
            var addedEntityId = _repository.AddPayment(dto);

            // Then

            _addedPaymentDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            var actual = _repository.GetPaymentById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }


        [Test]
        public void AddPaymentNegativeTestNullEntity()
        {
            //Given

            //When
            try
            {
                var paymentId = _repository.AddPayment(null);
                _addedPaymentDtoIds.Add(paymentId);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(99)]
        public void PaymentAddNegativeTestEmptyProprties(int mockId)
        {
            //Given
            var payment = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();

            //When
            try
            {
                payment.Id = _repository.AddPayment(payment);
                _addedPaymentDtoIds.Add(payment.Id);
            }
            //Then
            catch
            {
                Assert.Pass();
            }
            Assert.Fail();
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
            SetupRelatedEntititesRegulary(dto);

            var addedEntityId = _repository.AddPayment(dto);

            _addedPaymentDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;

            //When
            var actual = _repository.GetPaymentById(addedEntityId);


            // Then
            Assert.AreEqual(dto, actual);
        }


        [Test]
        public void GetPaymentByIdNegativeTestNotExist()
        {
            //Given

            //When
            var actual = _repository.GetPaymentById(-1);
            //Then
            Assert.IsNull(actual);
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

            SetupRelatedEntitiesForSeveralPayments(userDto, groupDto, courseDto, studentGroupDto, contractNumber);


            for (int mockId = 1; mockId <= numberOfPayments; mockId++)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
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

        [TestCase(-1)]
        [TestCase(Int32.MaxValue)]
        public void GetPaymentByContractNumberNegativeTestWrongContractNumber(int wrongnumber)
        {     
            //Given

            //When
            var actual = _repository.GetPaymentByContractNumber(wrongnumber);
            //Then
            Assert.IsEmpty(actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4 }, "2021.01", "2021.02")]
        public void GetPaymentsByPeriodPositiveTest(int[] mockIds, string periodFrom, string periodTo)
        {
            //Given
            var expected = new List<PaymentDto>();
            var userDto = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var studentGroupDto = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(1).Clone();

            SetupRelatedEntitiesForSeveralPayments(userDto, groupDto, courseDto, studentGroupDto);

            foreach (var mockId in mockIds)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
                dto.Student = userDto;
                var addedEntityId = _repository.AddPayment(dto);

                _addedPaymentDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;
               if (mockId < 4)
                expected.Add(dto);

            }
            //When

            var actual = _repository.GetPaymentsByPeriod(periodFrom, periodTo);

            // Then
            CollectionAssert.AreEqual(expected, actual, new PaymentComparer());
        }


        [TestCase("0001.01", "0001.01")]
        [TestCase( "2021.02", "2021.01")]
        public void GetPaymentByPeriodNegativeTestWrongPeriod(string periodFrom, string periodTo)
        {
            //Given

            //When
            var actual = _repository.GetPaymentsByPeriod(periodFrom, periodTo);
            //Then
            Assert.IsEmpty(actual);
        }


        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void GetPaymentsByUserIdPositiveTest(int[] mockIds)
        {
            //Given
            var expected = new List<PaymentDto>();
            var userDto = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var groupDtoSecond = (GroupDto)GroupMockGetter.GetGroupDtoMock(2).Clone();

            var studentGroupDtoFirst = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(1).Clone();
            var studentGroupDtoSecond = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(2).Clone();
            var addedUserId = _userRepository.AddUser(userDto);

            SetupRelatedEntititesForSeveralPaymentsAndTwoStudentGroups(userDto, groupDto, groupDtoSecond, courseDto, studentGroupDtoFirst, studentGroupDtoSecond, addedUserId);


            foreach (var mockId in mockIds)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
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

        [TestCase(-1)]
        [TestCase(Int32.MaxValue)]
        public void GetPaymentByUserIdNegativeTestWrongIdNumber(int wrongnumber)
        {
            //Given

            //When
            var actual = _repository.GetPaymentsByUserId (wrongnumber);
            //Then
            Assert.IsEmpty(actual);
        }

        [TestCase(1)]
        public void UpdatePaymentPositiveTest(int mockId)
        {
            //Given
            var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();

            SetupRelatedEntititesRegulary(dto);

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
        public void UpdatePaymentNegativeTestEntityNotExists(int mockId)
        {
            //Given
            var payment = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();

            //When
            var result = _repository.UpdatePayment(payment);

            //Then
            Assert.AreEqual(0, result);
        }

        [TestCase(1, 99)]
        public void PaymentUpdateNegativeTestEmptyProperties(int mockToAddId, int mockToUpdate)
        {
            //Given
            var payment = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockToAddId).Clone();
            var paymentId = _repository.AddPayment(payment);
            _addedPaymentDtoIds.Add(paymentId);
            //When
            try
            {
                payment = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockToUpdate).Clone();
                payment.Id = paymentId;
                _repository.UpdatePayment(payment);

            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void PaymentUpdateNegativeTestNullEntity()
        {
            //Given

            //When
            try
            {
                _repository.UpdatePayment(null);
            }
            //Then
            catch (Exception)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }

        [TestCase(new int[] { 4, 5, 6, 7 }, "2021.05")]
        [TestCase(new int[] { 4, 5, 6, 7 }, "2021.06")]
        public void GetListOfStudentsByPeriodWhoHaveNotPaidPositiveTest(int[] mockIds, string month)
        {
            var expected = new List<UserDto>();
            var userDto = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            var groupDto = (GroupDto)GroupMockGetter.GetGroupDtoMock(1).Clone();
            var courseDto = (CourseDto)CourseMockGetter.GetCourseDtoMock(1).Clone();
            var studentGroupDto = (StudentGroupDto)StudentGroupMockGetter.GetStudentGroupDtoMock(2).Clone();

            SetupRelatedEntitiesForSeveralPayments(userDto, groupDto, courseDto, studentGroupDto);

            foreach (var mockId in mockIds)
            {
                var dto = (PaymentDto)PaymentMockGetter.GetPaymentDtoMock(mockId).Clone();
                dto.Student = userDto;
                var addedEntityId = _repository.AddPayment(dto);

                _addedPaymentDtoIds.Add(addedEntityId);
                dto.Id = addedEntityId;

            }

            expected.Add(userDto);

            //When

            var actual = _repository.GetListOfStudentsByPeriodWhoHaveNotPaid(month);

            // Then
            CollectionAssert.AreEqual(expected, actual, new UserComparer());
        }

        [TestCase("0001.01")]
        [TestCase("2045.02")]
        public void GetListOfStudentsByPeriodWhoHaveNotPaidNegativeTestWrongPeriod(string month)
        {
            //Given

            //When
            var actual = _repository.GetListOfStudentsByPeriodWhoHaveNotPaid(month);
            //Then
            Assert.IsEmpty(actual);
        }

        [TearDown]
        public void TearDownTest()
        {

            _addedStudentGroupDtoIds.ForEach(record =>
            _groupRepository.DeleteStudentGroup(record.Item1, record.Item2));

            _addedGroupDtoIds.ForEach(id =>
            {
                _groupRepository.DeleteGroup(id);
            });


            _addedUserDtoIds.ForEach(id =>
            {
                _userRepository.HardDeleteUser(id);
            });


            _addedPaymentDtoIds.ForEach(id =>
            {
                _repository.DeletePayment(id);
            });

            _addedCourseDtoIds.ForEach(id =>
            _courseRepository.HardDeleteCourse(id));


        }

        private void SetupRelatedEntititesRegulary(PaymentDto dto )
        {

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
        }

        private void SetupRelatedEntitiesForSeveralPayments( UserDto userDto, GroupDto groupDto, CourseDto courseDto, StudentGroupDto studentGroupDto, int contractNumber = -1)
        {
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
            if (contractNumber >= 0)
                studentGroupDto.ContractNumber = contractNumber;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDto);
            Assert.Greater(addedUserId, 0);
            Assert.Greater(addedGroupId, 0);
            Assert.Greater(addedStudentGroupId, 0);


            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedCourseDtoIds.Add(addedCourseId);
        }

        private void SetupRelatedEntititesForSeveralPaymentsAndTwoStudentGroups(UserDto userDto, GroupDto groupDto, GroupDto groupDtoSecond, CourseDto courseDto, StudentGroupDto studentGroupDtoFirst, StudentGroupDto studentGroupDtoSecond,  int addedUserId)

        {
            userDto.Id = addedUserId;

            var addedCourseId = _courseRepository.AddCourse(courseDto);

            courseDto.Id = addedCourseId;
            groupDto.Course = courseDto;
            groupDtoSecond.Course = courseDto;
            var addedGroupId = _groupRepository.AddGroup(groupDto);


            var addedGroupSecondId = _groupRepository.AddGroup(groupDtoSecond);


            studentGroupDtoFirst.User.Id = addedUserId;
            studentGroupDtoFirst.Group.Course = courseDto;
            studentGroupDtoFirst.Group.Id = addedGroupId;
            var addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDtoFirst);

            studentGroupDtoSecond.User.Id = addedUserId;
            studentGroupDtoSecond.Group.Course = courseDto;
            studentGroupDtoSecond.Group.Id = addedGroupSecondId;
            addedStudentGroupId = _groupRepository.AddStudentGroup(studentGroupDtoSecond);



            _addedUserDtoIds.Add(addedUserId);
            _addedGroupDtoIds.Add(addedGroupId);
            _addedGroupDtoIds.Add(addedGroupSecondId);
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupId));
            _addedStudentGroupDtoIds.Add((addedUserId, addedGroupSecondId));
            _addedCourseDtoIds.Add(addedCourseId);
        }
    }

    [ExcludeFromCodeCoverage]
    internal class PaymentComparer : System.Collections.IComparer
    {
        internal PaymentComparer()
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

                var UserComparer = new UserComparer();

                if (
                    ((paymentDtoExpected.Id == paymentDtoActual.Id) &&
                (paymentDtoExpected.ContractNumber == paymentDtoActual.ContractNumber) &&
                (paymentDtoExpected.Amount == paymentDtoActual.Amount) &&
                (paymentDtoExpected.Date == paymentDtoActual.Date) &&
                (paymentDtoExpected.IsPaid == paymentDtoActual.IsPaid) &&
                string.Equals(paymentDtoExpected.Period, paymentDtoActual.Period)) &&
                (UserComparer.Compare(userDtoExpected, userDtoActual) == 0)
                    )

                    return 0;
                else return 1;
            }
            else if (expected.Equals(actual)) return 0;
            return 1;
        }

    }

    [ExcludeFromCodeCoverage]
    internal class UserComparer : System.Collections.IComparer
    {
        internal UserComparer()
        {

        }

        public int Compare(object expected, object actual)
        {
            if (expected is UserDto && actual is UserDto)
            {

                var userDtoExpected = (UserDto)expected;
                var userDtoActual = (UserDto)actual;

                if (

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

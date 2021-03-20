using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using System.Globalization;
using EducationSystem.Data.Tests.Mocks;
using EducationSystem.Core.Enums;
using System.Linq;

namespace EducationSystem.Data.Tests
{
    public class UserTests : BaseTest
    {
        private List<int> _addedUserDtoIds;
        private List<(int, int)> _addedUserRoleIds;
        private UserRepository _repository;

        [OneTimeSetUp]
        public void UserRepositoryTestsSetup()
        {
            _addedUserDtoIds = new List<int>();
            _repository = new UserRepository(_options);
            _addedUserRoleIds = new List<(int, int)>();
            var junk = _repository.GetUsers();
            if (junk.Count > 0)
            {
                junk.ForEach(user =>
                {
                    user.Roles.ForEach(role =>
                    {
                        _repository.DeleteRoleFromUser(user.Id, (int)role);
                    });
                    _repository.HardDeleteUser(user.Id);
                });
            };

        }

        //  entity tests are below:

        [TestCase(1)]
        public void UserGetUserByIdPositiveTest(int mockId)
        {
            //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);
            _repository.AddRoleToUser(addedEntityId, 1);
            expected.Roles = new List<Role> { Role.Admin };
            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));
            expected.Id = addedEntityId;

            //When, Then
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void UserGetUsersPositiveTest(int[] mockIds)
        {

            //Given
            var expected = new List<UserDto>();
            var _addedUserDtoIdsLocal = new List<int>();
            foreach (int mockId in mockIds)
            {
                var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
                var addedEntityId = _repository.AddUser(dto);
                _repository.AddRoleToUser(addedEntityId, 1);
                dto.Roles = new List<Role> { Role.Admin };
                _addedUserDtoIds.Add(addedEntityId);
                _addedUserDtoIdsLocal.Add(addedEntityId);
                _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));
                
                dto.Id = addedEntityId;
                expected.Add(dto);
            }

            //When, Then
            var actual = _repository.GetUsers();
            CollectionAssert.AreEqual(expected, actual);

            _addedUserDtoIdsLocal.ForEach(id =>
            {
                CollectionAssert.AreEqual(expected.First(item => item.Id == id).Roles, actual.First(item => item.Id == id).Roles);
            });
        


        }


        [TestCase(1)]
        public void UserAddPositiveTest(int mockId)
        {
             //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);
            _repository.AddRoleToUser(addedEntityId, 1);
            Assert.Greater(addedEntityId, 0);
            expected.Roles = new List<Role> { Role.Admin };
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            //When, Then
            _addedUserDtoIds.Add(addedEntityId);
            expected.Id = addedEntityId;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }


        [TestCase(1)]
        [TestCase(6)]
        public void UserDeleteOrRecoverPositiveTest(int mockId)
        {
            //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);
            _repository.AddRoleToUser(addedEntityId, 1);
            expected.Roles = new List<Role> { Role.Admin };
            _addedUserDtoIds.Add(addedEntityId);
            expected.Id = addedEntityId;
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            //When, Then
            _repository.DeleteOrRecoverUser(addedEntityId, true);
            expected.IsDeleted = true;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }

        [TestCase(1)]
       public void UserUpdatePositiveTest(int mockId)
       {
            //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);
            _repository.AddRoleToUser(addedEntityId, 1);
            expected.Roles = new List<Role> { Role.Admin };

            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            expected = new UserDto
            {
                Id = addedEntityId,
                Email = "qwer4@mail.ru",
                FirstName = "John",
                BirthDate = DateTime.ParseExact("02.02.1902", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                LastName = "Doe",
                Phone = "7111111111",
                UserPic = " 33",
                //not changed here
                Login = expected.Login,
                Password = expected.Password,
                IsDeleted = expected.IsDeleted,
                Roles = expected.Roles
        };

            //When, Then
            _repository.UpdateUser(expected);

            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }



        [TestCase(1)]
        public void UserDeleteRolePositiveTest(int mockId)
        {
            //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);

            _addedUserDtoIds.Add(addedEntityId);

            _repository.AddRoleToUser(addedEntityId, 1);
            _repository.AddRoleToUser(addedEntityId, 2);
            _repository.AddRoleToUser(addedEntityId, 3);

            _addedUserRoleIds.Add((addedEntityId, 1));
            _addedUserRoleIds.Add((addedEntityId, 2));
            _addedUserRoleIds.Add((addedEntityId, 3));

            //When, Then
            _repository.DeleteRoleFromUser(addedEntityId, 2);

            expected.Id = addedEntityId;
            expected.Roles = new List<Role> { Role.Admin,Role.Teacher };
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }

        [TestCase(1)]
        public void UserCheckRolePositiveTest(int mockId)
        {
            //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _addedUserDtoIds.Add(addedEntityId);
            _repository.AddRoleToUser(addedEntityId, 1);
            _addedUserRoleIds.Add((addedEntityId, 1));
            var expected = new UserDto()
            {
                Id = addedEntityId,
                Login = dto.Login,
                Password = dto.Password,
                Roles = new List<Role> { Role.Admin }
            };

            // When
            var actual = _repository.CheckUser(dto.Login);
            
            //Then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1)]
        public void UserChangePasswordPositiveTest(int mockId)
        {
            //Given
            var expected = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(expected);
            _repository.AddRoleToUser(addedEntityId, 1);
            expected.Roles = new List<Role> { Role.Admin };
            Assert.Greater(addedEntityId, 0);
            expected.Id = addedEntityId;

            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, 1));
            string newPassword = "fskljjsdljf";

            //When, Then
            _repository.ChangeUserPassword(addedEntityId, expected.Password, newPassword);

            expected.Password = newPassword;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(expected, actual);
            CollectionAssert.AreEqual(expected.Roles, actual.Roles);
        }


        [TearDown]
        public void UserTestTearDown()
        {

            _addedUserRoleIds.ForEach(record =>
            {
                _repository.DeleteRoleFromUser(record.Item1, record.Item2);
            });

            _addedUserDtoIds.ForEach(id =>
            {
                _repository.HardDeleteUser(id); 
            });

        }

    }
}

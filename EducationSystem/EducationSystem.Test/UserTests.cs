using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using System.Globalization;
using EducationSystem.Data.Tests.Mocks;
using EducationSystem.Core.Enums;

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

        }

        //  entity tests are below:

        [TestCase(1)]
        public void UserGetUserByIdPositiveTest(int mockId)
        {
            //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _repository.AddRoleToUser(addedEntityId, 1);
            dto.Roles = new List<Role> { Role.Admin };
            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));
            dto.Id = addedEntityId;

            //When
            var actual = _repository.GetUserById(addedEntityId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        public void UserGetUsersPositiveTest(int[] mockIds)
        {
            //Given
            var dtoList = new List<UserDto>();
            foreach (int mockId in mockIds)
            {
                var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
                var addedEntityId = _repository.AddUser(dto);
                _repository.AddRoleToUser(addedEntityId, 1);
                dto.Roles = new List<Role> { Role.Admin };
                _addedUserDtoIds.Add(addedEntityId);
                _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));
                dto.Id = addedEntityId;
                dtoList.Add(dto);
            }

            //When
            var actual = _repository.GetUsers();

            // Then
            Assert.AreEqual(dtoList, actual);
        }


        [TestCase(1)]
        public void UserAddPositiveTest(int mockId)
        {
             //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _repository.AddRoleToUser(addedEntityId, 1);
            Assert.Greater(addedEntityId, 0);
            dto.Roles = new List<Role> { Role.Admin };
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            //When
            _addedUserDtoIds.Add(addedEntityId);

            // Then
            dto.Id = addedEntityId;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }


        [TestCase(1)]
        public void UserDeleteOrRecoverPositiveTest(int mockId)
        {
            //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _repository.AddRoleToUser(addedEntityId, 1);
            dto.Roles = new List<Role> { Role.Admin };
            _addedUserDtoIds.Add(addedEntityId);
            dto.Id = addedEntityId;
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            //When
            _repository.DeleteOrRecoverUser(addedEntityId, true);

            // Then
            dto.IsDeleted = true;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
       public void UserUpdatePositiveTest(int mockId)
       {
            //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _repository.AddRoleToUser(addedEntityId, 1);
            dto.Roles = new List<Role> { Role.Admin };

            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, (int)Role.Admin));

            dto = new UserDto
            {
                Id = addedEntityId,
                Email = "qwer4@mail.ru",
                FirstName = "John",
                BirthDate = DateTime.ParseExact("02.02.1902", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                LastName = "Doe",
                Phone = "7111111111",
                UserPic = " 33",
                //not changed here
                Login = dto.Login,
                Password = dto.Password,
                IsDeleted = dto.IsDeleted,
                Roles = dto.Roles
        }; 

            //When
            _repository.UpdateUser(dto);

            // Then
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }



        [TestCase(1)]
        public void UserDeleteRolePositiveTest(int mockId)
        {
            //Given
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);

            _addedUserDtoIds.Add(addedEntityId);

            _repository.AddRoleToUser(addedEntityId, 1);
            _repository.AddRoleToUser(addedEntityId, 2);
            _repository.AddRoleToUser(addedEntityId, 3);

            _addedUserRoleIds.Add((addedEntityId, 1));
            _addedUserRoleIds.Add((addedEntityId, 2));
            _addedUserRoleIds.Add((addedEntityId, 3));

            //When
            _repository.DeleteRoleToUser(addedEntityId, 2);

            // Then
            dto.Id = addedEntityId;
            dto.Roles = new List<Role> { Role.Admin,Role.Teacher };
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(dto, actual);
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
            var dto = (UserDto)UserMockGetter.GetUserDtoMock(mockId).Clone();
            var addedEntityId = _repository.AddUser(dto);
            _repository.AddRoleToUser(addedEntityId, 1);
            dto.Roles = new List<Role> { Role.Admin };
            Assert.Greater(addedEntityId, 0);
            dto.Id = addedEntityId;

            _addedUserDtoIds.Add(addedEntityId);
            _addedUserRoleIds.Add((addedEntityId, 1));
            string newPassword = "fskljjsdljf";

            //When
            _repository.ChangeUserPassword(addedEntityId, dto.Password, newPassword);

            // Then
            dto.Password = newPassword;
            var actual = _repository.GetUserById(addedEntityId);
            Assert.AreEqual(dto, actual);
        }


        [TearDown]
        public void UserTestTearDown()
        {

            _addedUserRoleIds.ForEach(record =>
            {
                _repository.DeleteRoleToUser(record.Item1, record.Item2);
            });

            _addedUserDtoIds.ForEach(id =>
            {
                _repository.HardDeleteUser(id); 
            });

        }

    }
}

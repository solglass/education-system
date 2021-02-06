using NUnit.Framework;
using System;
using EducationSystem.Data.Models;
using EducationSystem.Data;
using System.Collections.Generic;
using System.Globalization;

namespace NUnitTestProject
{
    public class UserTests
    {
        private List<int> _userId;
        private List<int> _roleId;
        private List<int> _userRoleId;

        private UserRepository _uRepo;

        [SetUp]
        public void UserRepositoryTestsSetup()
        {
            _userId = new List<int>();
            _roleId = new List<int>();
            _userRoleId = new List<int>();
            _uRepo = new UserRepository();

        }


        [TestCase(1)]
        public void UserAddTests(int dtoMockNumber)
        {
            UserDto expected = GetMockUserAdd(dtoMockNumber);
            var added = _uRepo.AddUser(expected);
            _userId.Add(added);
            expected.Id = added;

            if (_userId.Count == 0) { Assert.Fail("User addition failed"); }
            else
            {
                UserDto actual = _uRepo.GetUserById(_userId[_userId.Count - 1]);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1)]
        public void UserDelete(int dtoMockNumber)
        {
            UserDto expected = GetMockUserAdd(dtoMockNumber);
            _userId.Add(_uRepo.AddUser(expected));
            if (_userId.Count == 0) { Assert.Fail("User addition failed"); }
            else
            {
                int newId = _userId[_userId.Count - 1];
                _uRepo.DeleteUser(newId);
                UserDto actual = _uRepo.GetUserById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(1)]
        public void UserUpdate(int dtoMockNumber)
        {
            UserDto expected = GetMockUserAdd(dtoMockNumber);
            _userId.Add(_uRepo.AddUser(expected));
            if (_userId.Count == 0) { Assert.Fail("User addition failed"); }
            else
            {
                int newId = _userId[_userId.Count - 1];
                expected.Id = newId;
                _uRepo.UpdateUser(expected);
                UserDto actual = _uRepo.GetUserById(newId);
                Assert.AreEqual(expected, actual);
            }
        }



        [TestCase(1)]
        public void RoleAddTests(int dtoMockNumber)
        {
            RoleDto expected = GetMockRoleAdd(dtoMockNumber);
            var added = _uRepo.AddRole(expected);
            _roleId.Add(added);
            expected.Id = added;

            if (_roleId.Count == 0)
            {
                Assert.Fail("Role addition failed");
            }
            else
            {
                RoleDto actual = _uRepo.GetRoleById(_roleId[_roleId.Count - 1]);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestCase(1)]
        public void RoleDelete(int dtoMockNumber)
        {
            RoleDto expected = GetMockRoleAdd(dtoMockNumber);
            _roleId.Add(_uRepo.AddRole(expected));
            if (_roleId.Count == 0) { Assert.Fail("Role addition failed"); }
            else
            {
                int newId = _roleId[_roleId.Count - 1];
                _uRepo.DeleteRole(newId);
                RoleDto actual = _uRepo.GetRoleById(newId);
                if (actual == null) { Assert.Pass(); }
                else Assert.Fail("Deletion went wrong");
            }
        }

        [TestCase(1)]
        public void RoleUpdate(int dtoMockNumber)
        {
            RoleDto expected = GetMockRoleAdd(dtoMockNumber);
            _roleId.Add(_uRepo.AddRole(expected));
            if (_roleId.Count == 0) 
            {
                Assert.Fail("Role addition failed"); 
            }
            else
            {
                int newId = _roleId[_roleId.Count - 1];
                expected.Id = newId;
                _uRepo.UpdateRole(expected);
                RoleDto actual = _uRepo.GetRoleById (newId);
                Assert.AreEqual(expected, actual);
            }
        }
        [TearDown]
        public void UserRepositiryTestsTearDown()
        {
            UserRepository uRepo = new UserRepository();
            foreach (int elem in _userRoleId)
            {
                uRepo.DeleteRoleToUser(elem);
            }
            foreach (int elem in _roleId)
            {
                uRepo.DeleteRole(elem);
            }
            foreach (int elem in _userId)
            {
                uRepo.DeleteUser(elem);
            }
        }

        private UserDto GetMockUserAdd(int n)
        {
            switch (n)
            {
                case 1:
                    UserDto userDto = new UserDto();
                    userDto.Email = "User@mail.ru";
                    userDto.FirstName = "Anton";
                    //userDto.RegistrationDate = DateTime.ParseExact("06.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    userDto.BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    //List<RoleDto> roles= new List<RoleDto>();
                    //roles.Add(GetMockRoleAdd(n));
                    //userDto.Roles =roles;
                    userDto.IsDeleted = false;
                    userDto.LastName = "Negodyaj";
                    userDto.Password = "1234567";
                    userDto.Phone = "9999999997";
                    userDto.UserPic = " 22";
                    userDto.Id = 1;
                    userDto.Login = "AN7";
                    return userDto;
                default:
                    throw new Exception();
            }
        }

        private RoleDto GetMockRoleAdd(int n)
        {
            switch (n)
            {
                case 1:
                    RoleDto roleDto = new RoleDto();
                    roleDto.Name = "Teacher5";
                    return roleDto;
                default:
                    throw new Exception();
            }
        }
    }
}
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class UserMockGetter
    {
        public static UserDto GetUserDtoMock(int mockId)
        {

            UserDto userDto = mockId switch
            {
                1 => new UserDto()
                {
                    Email = "ololosh@mail.ru",
                    FirstName = "Ololosh",
                    BirthDate = DateTime.ParseExact("06.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    LastName = "Horoshiy",
                    Password = "1234567",
                    Phone = "123123123",
                    UserPic = " 21",
                    Login = "ololosha",
                },

                2 => new UserDto()
                {
                    Email = "Use1r15@mail.ru",
                    FirstName = "Antonio",
                    BirthDate = DateTime.ParseExact("06.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    LastName = "Negodny",
                    Password = "1234567",
                    Phone = "9999999999",
                    UserPic = " 22",
                    Login = "AN7123",
                },

                3 => new UserDto()
                {
                    Email = "vasyarulit@mail.ru",
                    FirstName = "Vasek",
                    BirthDate = DateTime.ParseExact("07.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    LastName = "Pupkin",
                    Password = "1234567",
                    Phone = "999977777",
                    UserPic = " 23",
                    Login = "vasya",
                },
                _ => throw new NotImplementedException()
            };

            return userDto;
        }

    }
}
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

            switch (mockId)
            {
                case 1:
                    return new UserDto
                    {
                        Email = "Use1r14@mail.ru",
                        FirstName = "Anton",
                        BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        IsDeleted = false,
                        LastName = "Negodyaj",
                        Password = "1234567",
                        Phone = "9999999997",
                        UserPic = " 22",
                        Login = "AN712"
                    };
                case 2:
                    return new UserDto
                    {
                        Email = "Case2@mail.ru",
                        FirstName = "Anton",
                        BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        IsDeleted = false,
                        LastName = "Negodyaj",
                        Password = "1234567",
                        Phone = "45454677",
                        UserPic = "00",
                        Login = "Case2Login"
                    };
                case 3:
                    return new UserDto
                    {
                        Email = "Case333@mail.ru",
                        FirstName = "Anton",
                        BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        IsDeleted = false,
                        LastName = "Negodyaj",
                        Password = "1234567",
                        Phone = "030303033",
                        UserPic = "00",
                        Login = "Case333Login"
                    };
                case 4:
                    return new UserDto
                    {
                        Email = "Case4444@mail.ru",
                        FirstName = "Anton",
                        BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        IsDeleted = false,
                        LastName = "Negodyaj",
                        Password = "1234567",
                        Phone = "4448444444",
                        UserPic = "00",
                        Login = "Case4444Login"
                    };
                case 5:
                    return new UserDto
                    {
                        Email = "Case55555@mail.ru",
                        FirstName = "Anton",
                        BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        IsDeleted = false,
                        LastName = "Negodyaj",
                        Password = "1234567",
                        Phone = "5555595555",
                        UserPic = "00",
                        Login = "Case55555Login"
                    };

                default:

            return null;
            };
        }

    }
}
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
                _ => throw new NotImplementedException()
            };

            return userDto;
        }

    }
}
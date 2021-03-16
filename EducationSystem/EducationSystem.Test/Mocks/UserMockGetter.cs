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
                    Email = "Use1r14@mail.ru",
                    FirstName = "Anton",
                    BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    IsDeleted = false,
                    LastName = "Negodyaj",
                    Password = "1234567",
                    Phone = "9999999997",
                    UserPic = " 22",
                    Login = "AN712",
                },
                _ => throw new NotImplementedException()
            };

            return userDto;
        }

    }
}

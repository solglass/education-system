using EducationSystem.Data.Models;
using System;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class NotificationMockGetter
    {
        public static NotificationDto GetNotificationDtoMock(int id)
        {
            return id switch
            {
                1 => new NotificationDto()
                {
                    User = new UserDto(),
                    Author = new UserDto(),
                    Message = "Тобi Жопа",
                    Date = new DateTime(2021, 03, 30, 11, 30, 0).AddDays(-1),
                    IsRead = false
                },
                2 => new NotificationDto()
                {
                    User = new UserDto(),
                    Author = new UserDto(),
                    Message = "Тобi НЕ жопа",
                    Date = new DateTime(2021, 03, 30, 11, 30, 0).AddDays(-2),
                    IsRead = false
                },
                3 => new NotificationDto()
                {
                    User = new UserDto(),
                    Author = new UserDto(),
                    Message = "Текст сообщения",
                    Date = new DateTime(2021, 03, 30, 11, 30, 0).AddDays(+3),
                    IsRead = false
                },
                4 => new NotificationDto(),
                _ => null,
            };
        }
    }
}

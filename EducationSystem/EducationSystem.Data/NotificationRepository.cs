using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public int AddNotification(NotificationDto dto)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Notification_Add",
                new
                {
                    userId = dto.User.Id,
                    authorId = dto.Author.Id,
                    message = dto.Message,
                    date = dto.Date
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateNotification(NotificationDto dto)
        {
            var result = _connection
                .Execute("dbo.Notification_Update",
                new
                {
                    dto.Id,
                    dto.Message,
                    dto.Date
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int SetReadOrUnreadNotification(int id, bool isRead)
        {
            var result = _connection
                .Execute("dbo.Notification_SetSeenStatus",
                new
                {
                    id,
                    isRead
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public NotificationDto GetNotificationById(int id)
        {

            var result = _connection
                .Query<NotificationDto, UserDto, UserDto, NotificationDto>(
                    "dbo.Notification_SelectById",
                    (notification, user, author) =>
                    {
                        if (notification != null)
                        {
                            notification.User = user;
                            notification.Author = author;
                        }
                        return notification;
                    },
                    new
                    {
                        id
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return result;
        }

        public int DeleteNotification(int id)
        {
            var result = _connection
                .Execute("dbo.Notification_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public List<NotificationDto> GetNotificationsByUserId(int userId)
        {
            var result = _connection
                .Query<NotificationDto, UserDto, UserDto, NotificationDto>(
                    "dbo.Notification_SelectById",
                    (notification, user, author) =>
                    {
                        if (notification != null)
                        {
                            notification.User = user;
                            notification.Author = author;
                        }
                        return notification;
                    },
                    new
                    {
                        userId
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
    }
}

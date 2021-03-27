using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface INotificationService
    {
        int AddNotification(int userId, int authorId, NotificationDto notificationDto);
        int AddNotificationsForAllStaff(int authorId, NotificationDto notificationDto);
        int AddNotificationsForAllUsers(int authorId, NotificationDto notificationDto);
        int AddNotificationsForGroup(int groupId, int authorId, NotificationDto notificationDto);
        int AddNotificationsForStudents(int authorId, NotificationDto notificationDto);
        int AddNotificationsForTeachers(int authorId, NotificationDto notificationDto);
        int DeleteNotification(int id);
        NotificationDto GetNotificationById(int id);
        List<NotificationDto> GetNotificationsByUserId(int userId);
        int SetReadOrUnreadNotification(int id, bool isRead);
        int UpdateNotification(NotificationDto notificationDto);
    }
}
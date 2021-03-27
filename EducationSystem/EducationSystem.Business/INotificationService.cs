using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface INotificationService
    {
        int AddNotification(NotificationDto notificationDto);
        int AddNotificationsForAllStaff(NotificationDto notificationDto);
        int AddNotificationsForAllUsers(NotificationDto notificationDto);
        int AddNotificationsForGroup(int groupId, NotificationDto notificationDto);
        int AddNotificationsForStudents(NotificationDto notificationDto);
        int AddNotificationsForTeachers(NotificationDto notificationDto);
        int DeleteNotification(int id);
        NotificationDto GetNotificationById(int id);
        List<NotificationDto> GetNotificationsByUserId(int userId);
        int SetReadOrUnreadNotification(int id, bool isRead);
        int UpdateNotification(NotificationDto notificationDto);
    }
}
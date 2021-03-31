using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface INotificationRepository
    {
        int AddNotification(NotificationDto dto);
        int DeleteNotification(int id);
        NotificationDto GetNotificationById(int id);
        List<NotificationDto> GetNotificationsByUserId(int userId);
        int SetReadOrUnreadNotification(int id, bool isRead);
        int UpdateNotification(NotificationDto dto);
    }
}
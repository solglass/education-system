using EducationSystem.Core.Enums;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationSystem.Business
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRepository;
        private IUserRepository _userRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public int AddNotification(NotificationDto notificationDto) => _notificationRepository.AddNotification(notificationDto);
        public int UpdateNotification(NotificationDto notificationDto) => _notificationRepository.UpdateNotification(notificationDto);
        public int DeleteNotification(int id) => _notificationRepository.DeleteNotification(id);
        public NotificationDto GetNotificationById(int id) => _notificationRepository.GetNotificationById(id);
        public List<NotificationDto> GetNotificationsByUserId(int userId) => _notificationRepository.GetNotificationsByUserId(userId);
        public int SetReadOrUnreadNotification(int id, bool isRead) => _notificationRepository.SetReadOrUnreadNotification(id, isRead);
        public int AddNotificationsForStudents(NotificationDto notificationDto) => AddNotificationForRole(new Role[] { Role.Student }, notificationDto);
        public int AddNotificationsForTeachers(NotificationDto notificationDto) => AddNotificationForRole(new Role[] { Role.Teacher }, notificationDto);
        public int AddNotificationsForAllUsers(NotificationDto notificationDto) => AddNotificationForRole(new Role[] { Role.Admin,
        Role.Student,
        Role.Teacher,
        Role.Tutor,
        Role.Methodist,
        Role.Manager }, notificationDto);
        public int AddNotificationsForAllStaff(NotificationDto notificationDto) => AddNotificationForRole(new Role[] { Role.Admin,
        Role.Teacher,
        Role.Tutor,
        Role.Methodist,
        Role.Manager }, notificationDto);

        public int AddNotificationsForGroup(int groupId, NotificationDto notificationDto)
        {
            throw new NotImplementedException();
        }

        private int AddNotificationForRole(Role[] role, NotificationDto notificationDto)
        {
            var users = _userRepository.GetUsers();
            int i = 0;
            users.ForEach(user =>
            {
                if (user.Roles.Intersect(role).Count() != 0)
                {
                    notificationDto.User.Id = user.Id;
                    _notificationRepository.AddNotification(notificationDto);
                    i++;
                }
            });
            return i;
        }
    }
}

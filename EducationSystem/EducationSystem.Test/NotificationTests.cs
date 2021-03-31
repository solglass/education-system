﻿using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System.Collections.Generic;

namespace EducationSystem.Data.Tests
{
    public class NotificationTests : BaseTest
    {
        private IUserRepository _userRepo;
        private INotificationRepository _notificationRepo;

        private UserDto _userDtoMock;
        private UserDto _authorDtoMock;

        private List<int> _userIdList;
        private List<int> _notificationIdList; 

        [SetUp]
        public void SetUpTest()
        {
            _userRepo = new UserRepository(_options);
            _notificationRepo = new NotificationRepository(_options);

            _userIdList = new List<int>();
            _notificationIdList = new List<int>();

            _userDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(1).Clone();
            _userDtoMock.Id = _userRepo.AddUser(_userDtoMock);
            _userIdList.Add(_userDtoMock.Id);

            _authorDtoMock = (UserDto)UserMockGetter.GetUserDtoMock(2).Clone();
            _authorDtoMock.Id = _userRepo.AddUser(_authorDtoMock);
            _userIdList.Add(_authorDtoMock.Id);
        }
       
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddNotificationPositiveTest(int mockId)
        {
            // Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            Assert.Greater(addedNotificationId, 0);
            dto.Id = addedNotificationId;
            _notificationIdList.Add(addedNotificationId);

            // When
            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            // Then
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        public void UpdateNotificationTest(int mockId, int updateMockId)
        {
            // Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            _notificationIdList.Add(addedNotificationId);

            dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(updateMockId).Clone();
            dto.Id = addedNotificationId;
            var affectedRowsCount = _notificationRepo.UpdateNotification(dto);

            // When
            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1, true)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [TestCase(3, true)]
        public void SetReadOrUnreadNotificationPositiveTest(int mockId, bool isRead)
        {
            //Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            _notificationIdList.Add(addedNotificationId);
            dto.Id = addedNotificationId;
            dto.IsRead = isRead;

            // When
            var affectedRowsCount = _notificationRepo.SetReadOrUnreadNotification(addedNotificationId, isRead);

            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            // Then
            Assert.AreEqual(1, affectedRowsCount);
            Assert.AreEqual(dto, actual);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DeleteNotificationPositiveTest(int mockId)
        {
            //Given
            var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockId).Clone();
            dto.User = _userDtoMock;
            dto.Author = _authorDtoMock;
            var addedNotificationId = _notificationRepo.AddNotification(dto);
            _notificationIdList.Add(addedNotificationId);

            var deletedRows = _notificationRepo.DeleteNotification(addedNotificationId);

            Assert.Greater(deletedRows, 0);

            //When
            var actual = _notificationRepo.GetNotificationById(addedNotificationId);

            //Then
            Assert.IsNull(actual);
        }

        [TestCase(new int[] {1, 2, 3 }, new int[] {1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 }, new int[] {3, 1, 1 })]
        [TestCase(new int[] { }, new int[] { })]
        public void GetNotificationsByUserIdPositiveTest(int[] mockIds, int [] hwmockIds)
        {
            // Given
            var addedUserId = _userDtoMock.Id;

            var expected = new List<NotificationDto>();
            for (int i = 0; i < mockIds.Length; i++)
            {
                var dto = (NotificationDto)NotificationMockGetter.GetNotificationDtoMock(mockIds[i]).Clone();
                dto.User = _userDtoMock;
                dto.Author = _authorDtoMock;
                var addedNotificationId = _notificationRepo.AddNotification(dto);
                _notificationIdList.Add(addedNotificationId);
                dto.Id = addedNotificationId;
                expected.Add(dto);
            }

            // When
            var actual = _notificationRepo.GetNotificationsByUserId(addedUserId);

            // Then
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [TearDown]
        public void TestTearDown()
        {
            DeleteNotifications();
            DeleteUser();
        }

        private void DeleteNotifications()
        {
            foreach (var item in _notificationIdList)
            {
                _notificationRepo.DeleteNotification(item);
            }
        }

        private void DeleteUser()
        {
            foreach (var userId in _userIdList)
            {
                _userRepo.HardDeleteUser(userId);
            }
        }
    }
}

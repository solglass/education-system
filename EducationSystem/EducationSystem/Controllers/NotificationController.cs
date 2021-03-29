using AutoMapper;
using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.Business;
using EducationSystem.Core.CustomExceptions;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IUserService _userService;
        private INotificationService _notificationService;
        private IGroupService _groupService;

        public NotificationController(IMapper mapper,
                                      IUserService userService,
                                      INotificationService notificationService,
                                      IGroupService groupService)
        {
            _mapper = mapper;
            _userService = userService;
            _notificationService = notificationService;
            _groupService = groupService;
        }

        /// <summary>Get notification by id</summary>
        /// <param name="notificationId"> Notification Id which we want</param>
        /// <returns>Information about notification</returns>
        // https://localhost:44365/api/notification/88
        [ProducesResponseType(typeof(NotificationOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{notificationId}")]
        [Authorize]
        public ActionResult<NotificationOutputModel> GetNotification(int notificationId)
        {
            var userId = Convert.ToInt32(User.FindFirst("id").Value);
            var notificationDto = _notificationService.GetNotificationById(notificationId);
            if (notificationDto is null)
                return NotFound($"Notification with id {notificationId} is not found");

            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && notificationDto.User.Id != userId
                && notificationDto.Author.Id != userId)
                return Forbid($"User is not author or recepient");

            var outputModel = _mapper.Map<NotificationOutputModel>(notificationDto);
            return Ok(outputModel);
        }

        /// <summary>Get notifications by user id</summary>
        /// <param name="userId">For which user we are looking for notifications</param>
        /// <returns>Information about notifications</returns>
        // https://localhost:44365/api/notification/by-user/88
        [ProducesResponseType(typeof(List<NotificationOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("by-user/{userId}")]
        [Authorize]
        public ActionResult<List<NotificationOutputModel>> GetNotificationsByUserId(int userId)
        {

            var userDto = _userService.GetUserById(userId);
            if (userDto is null)
                return NotFound($"User with id {userId} is not found");

            var requsterGroups = this.SupplyUserGroupsList(_groupService);
            var userGroups = _groupService.GetGroupsByStudentId(userId);
            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && (User.IsInRole("Студент") && userId != Convert.ToInt32(User.FindFirst("id").Value))
                || (!User.IsInRole("Студент") && userGroups.Intersect(requsterGroups).Count() == 0))
                return Forbid($"User is not in group of requester");

            var notificationDtos = _notificationService.GetNotificationsByUserId(userId);
            var outputModel = _mapper.Map<List<NotificationOutputModel>>(notificationDtos);
            return Ok(outputModel);
        }

        /// <summary>Add notification to user</summary>
        /// <param name="userId">userId is recipient </param>
        /// <param name="notification">Message for Notification</param>
        /// <returns>Information about added notification</returns>
        // https://localhost:44365/api/notification/user/88
        [ProducesResponseType(typeof(NotificationOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("user/{userId}")]
        [Authorize(Roles = "Администратор, Менеджер, Преподаватель, Тьютор")]
        public ActionResult<NotificationOutputModel> AddNotification(int userId, [FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var userDto = _userService.GetUserById(userId);
            if (userDto is null)
                return NotFound($"User with id {userId} is not found");

            var requsterGroups = this.SupplyUserGroupsList(_groupService);
            var userGroups = _groupService.GetGroupsByStudentId(userId);
            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && userGroups.Intersect(requsterGroups).Count() == 0)
                return Forbid($"User is not in group of requester");

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            var addedNotificationId = _notificationService.AddNotification(userId, authorId, notificationDto);
            var outputModel = _mapper.Map<NotificationOutputModel>(_notificationService.GetNotificationById(addedNotificationId));
            return Ok(outputModel);
        }

        /// <summary>Add notification to all staff</summary>
        /// <param name="notification">Message for Notification</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/staff
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("staff")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddNotificationForAllStuff([FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            _notificationService.AddNotificationsForAllStaff(authorId, notificationDto);
            return NoContent();
        }

        /// <summary>Add notification to all users</summary>
        /// <param name="notification">Message for Notification</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/users
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("users")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddNotificationForAllUsers([FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            _notificationService.AddNotificationsForAllUsers(authorId, notificationDto);
            return NoContent();
        }

        /// <summary>Add notification to all students</summary>
        /// <param name="notification">Message for Notification</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/students
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("students")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddNotificationForAllStudents([FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            _notificationService.AddNotificationsForStudents(authorId, notificationDto);
            return NoContent();
        }

        /// <summary>Add notification to all teachers</summary>
        /// <param name="notification">Message for Notification</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/teachers
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("teachers")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult AddNotificationForAllTeachers([FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            _notificationService.AddNotificationsForTeachers(authorId, notificationDto);
            return NoContent();
        }

        /// <summary>Add notification to group</summary>
        /// <param name="groupId">Group which we want to notify</param>
        /// <param name="notification">Message for Notification</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/group/2
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("group/{groupId}")]
        [Authorize(Roles = "Администратор, Менеджер, Преподаватель, Тьютор")]
        public ActionResult AddNotificationForGroup(int groupId, [FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var groupDto = _groupService.GetGroupById(groupId);
            if (groupDto is null)
                return NotFound($"Group with id {groupId} is not found");

            var userGroup = this.SupplyUserGroupsList(_groupService);
            if (!User.IsInRole("Администратор") && !User.IsInRole("Менеджер") && !userGroup.Contains(groupId))
            {
                return Forbid($"User is not in group {groupId}");
            }

            var notificationDto = _mapper.Map<NotificationDto>(notification);
            var authorId = Convert.ToInt32(User.FindFirst("id").Value);
            _notificationService.AddNotificationsForGroup(groupId, authorId, notificationDto);
            return NoContent();
        }

        /// <summary>Delete notification</summary>
        /// <param name="notificationId">notification which we want to delete</param>
        /// <returns>nothing</returns>
        // https://localhost:44365/api/notification/2
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{notificationId}")]
        [Authorize(Roles = "Администратор, Менеджер, Преподаватель, Тьютор")]
        public ActionResult DeleteNotification(int notificationId)
        {
            var notificationDto = _notificationService.GetNotificationById(notificationId);
            if (notificationDto is null)
                return NotFound($"Notification with id {notificationId} is not found");

            var userId = Convert.ToInt32(User.FindFirst("id").Value);
            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && notificationDto.Author.Id != userId)
            {
                return Forbid($"User is not author of notification");
            }
            _notificationService.DeleteNotification(notificationId);
            return NoContent();
        }

        /// <summary>Update message notification by id</summary>
        /// <param name="notificationId"> Notification Id which we read</param>
        /// <returns>Information about notification</returns>
        // https://localhost:44365/api/notification/88/read
        [ProducesResponseType(typeof(NotificationOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{notificationId}")]
        [Authorize(Roles = "Администратор, Менеджер, Преподаватель, Тьютор")]
        public ActionResult<NotificationOutputModel> UpdateNotification(int notificationId, [FromBody] NotificationInputModel notification)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }

            var userId = Convert.ToInt32(User.FindFirst("id").Value);
            var notificationDto = _notificationService.GetNotificationById(notificationId);
            if (notificationDto is null)
                return NotFound($"Notification with id {notificationId} is not found");

            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && notificationDto.Author.Id != userId)
                return Forbid($"User is not author");

            var updateNotificationDto = _mapper.Map<NotificationDto>(notification);
            _notificationService.UpdateNotification(updateNotificationDto);
            var outputModel = _mapper.Map<NotificationOutputModel>(_notificationService.GetNotificationById(notificationId));
            return Ok(outputModel);
        }

        /// <summary>Read notification by id</summary>
        /// <param name="notificationId"> Notification Id which we read</param>
        /// <returns>Information about notification</returns>
        // https://localhost:44365/api/notification/88/read
        [ProducesResponseType(typeof(NotificationOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{notificationId}/read")]
        [Authorize]
        public ActionResult<NotificationOutputModel> ReadNotification(int notificationId)
        {
            return SetReadOrUnreadNotification(notificationId, true);
        }

        /// <summary>Unread notification by id</summary>
        /// <param name="notificationId"> Notification Id which we unread</param>
        /// <returns>Information about notification</returns>
        // https://localhost:44365/api/notification/88/unread
        [ProducesResponseType(typeof(NotificationOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{notificationId}/unread")]
        [Authorize]
        public ActionResult<NotificationOutputModel> UnReadNotification(int notificationId)
        {
            return SetReadOrUnreadNotification(notificationId, false);
        }

        private ActionResult<NotificationOutputModel> SetReadOrUnreadNotification(int notificationId, bool isRead)
        {
            var userId = Convert.ToInt32(User.FindFirst("id").Value);
            var notificationDto = _notificationService.GetNotificationById(notificationId);
            if (notificationDto is null)
                return NotFound($"Notification with id {notificationId} is not found");

            if (!User.IsInRole("Администратор")
                && !User.IsInRole("Менеджер")
                && notificationDto.User.Id != userId
                && notificationDto.Author.Id != userId)
                return Forbid($"User is not author or recepient");

            _notificationService.SetReadOrUnreadNotification(notificationId, isRead);

            var outputModel = _mapper.Map<NotificationOutputModel>(_notificationService.GetNotificationById(notificationId));
            return Ok(outputModel);
        }
    }
}

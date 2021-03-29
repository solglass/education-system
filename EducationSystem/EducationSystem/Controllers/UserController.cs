using System;
using System.Collections.Generic;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;
using Microsoft.AspNetCore.Http;
using EducationSystem.Core.CustomExceptions;

namespace EducationSystem.Controllers
{
    // https://localhost:44365/api/user/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IUserService _userService;
        private ILessonService _lessonService;
        private IGroupService _groupService;
        
        public UserController(IMapper mapper, IUserService userService, ILessonService lessonService, IGroupService groupService)
        {
            _mapper = mapper;
            _userService = userService;
            _lessonService = lessonService;
            _groupService = groupService;
        }

        // https://localhost:44365/api/user/register
        /// <summary>user registration</summary>
        /// <param name="inputModel">information about registered user</param>
        /// <returns>rReturn information about added user</returns>
        [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult<UserOutputModel> Register([FromBody] UserInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var userDto = _mapper.Map<UserDto>(inputModel);
            var id = _userService.AddUser(userDto);
            var user = _userService.GetUserById(id);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/change-password
        /// <summary>Changing password of user</summary>
        /// <param name="id">Id of user for whom we are changing the password</param>
        /// /// <param name="oldPassword">Old password of user</param>
        /// /// <param name="newPassword">New password of user</param>
        /// <returns>Status204NoContent response</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("change-password")]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            if (_userService.GetUserById(id) == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            _userService.ChangePassword(id, oldPassword,newPassword);
            return NoContent();
        }

        // https://localhost:44365/api/user
        /// <summary>Get info of all users</summary>
        /// <returns>List of all users, but not deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор")]
        public ActionResult<List<UserOutputModel>> GetUsers()
        {
            var users = _userService.GetUsers();
            var outputModels = _mapper.Map<List<UserOutputModel>>(users);
            return Ok(outputModels);
        }

        // https://localhost:44365/api/user/42
        /// <summary>Get info of user</summary>
        /// <param name="id">Id of user</param>
        /// <returns>Info of user</returns>
        [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор")]
        public ActionResult<UserOutputModel> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/passed-homework/by-group/42
        /// <summary>Get info of student who have submitted their homework</summary>
        /// <param name="groupId">Id of group in which students study</param>
        /// <returns>List of students</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("passed-homework/by-group/{groupId}")]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор")]
        public ActionResult<List<UserOutputModel>> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            if (_groupService.GetGroupById(groupId) == null)
            {
                return NotFound($"Group with id {groupId} is not found");
            }
            var users = _userService.GetPassedStudentsAttempt_SelectByGroupId(groupId);
            var outputModel = _mapper.Map<List<UserOutputModel>>(users);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/42
        /// <summary>Update information about user</summary>
        /// <param name="id">Id of user</param>
        /// /// <param name="inputModel">Nonupdated info about  user </param>
        /// <returns>Updated info about user</returns>
        [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult<UserOutputModel> UpdateUserInfo(int id,[FromBody] UserInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            if (_userService.GetUserById(id) == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            var userDto = _mapper.Map<UserDto>(inputModel);
            _userService.UpdateUser(id, userDto);
            var outputModel = _mapper.Map<UserOutputModel>(_userService.GetUserById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/42
        /// <summary>Change value of parametr "IsDeleted" to 1(Deleted)</summary>
        /// <param name="id">Id of user</param>
        /// <returns>Update user, which is deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputExtendedModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<UserOutputExtendedModel> DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            var outputModel = _mapper.Map<UserOutputExtendedModel>(user);      
            if (outputModel.IsDeleted == true)
            {
                return Forbid($"User with id {id} has already been deleted");
            }
            _userService.DeleteUser(id);
            outputModel = _mapper.Map<UserOutputExtendedModel>(_userService.GetUserById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/42/recovery
        /// <summary>Change value of parametr "IsDeleted" to 0(Not deleted)</summary>
        /// <param name="id">Id of user</param>
        /// <returns>Update user, which is not deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputExtendedModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<UserOutputExtendedModel> RecoverUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            var outputModel = _mapper.Map<UserOutputExtendedModel>(user);
            if (outputModel.IsDeleted == false)
            {
                return Forbid($"User with id {id} has not been deleted");
            }
            _userService.RecoverUser(id);
            outputModel = _mapper.Map<UserOutputExtendedModel>(_userService.GetUserById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/88/payment
        /// <summary>Add payment to student</summary>
        /// <param name="id">Id of student</param>
        /// /// <param name="payment">information about payment</param>
        /// <returns>Information about added payment</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("{userId}/payment")]
        [Authorize(Roles = "Администратор, Менеджер, Студент")]
        public ActionResult<PaymentOutputModel> AddPayment(int id, [FromBody] PaymentInputModel payment)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            _userService.AddPayment(id, paymentDto);
            var outputModel = _mapper.Map<PaymentOutputModel>(_userService.GetPaymentById(id));
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/by-period
        /// <summary>Get payments for period</summary>
        /// <param name="periodInput">information about period</param>
        /// <returns>List of payments for period</returns>
        [ProducesResponseType(typeof(List<PaymentOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("payment/by-period")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<List<PaymentOutputModel>> GetPaymentsByPeriod([FromBody] PeriodInputModel periodInput)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var payments = _userService.GetPaymentsByPeriod(Converters.StrToDateTimePeriod(periodInput.PeriodFrom), Converters.StrToDateTimePeriod(periodInput.PeriodTo));
            if (payments == null)
            {
                return NotFound($"Payments not found");
            }
            var outputModels = _mapper.Map<List<PaymentOutputModel>>(payments);
            return Ok(outputModels);
        }

        //https://localhost:44365/api/user/42/payment
        /// <summary>Get payments by student id</summary>
        /// <param name="id">Id of student</param>
        /// <returns>List of payments of student</returns>
        [ProducesResponseType(typeof(List<PaymentOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/payment")]
        [Authorize(Roles = "Администратор,Менеджер")]
        public ActionResult<List<PaymentOutputModel>> GetPaymentsByUserId(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            var payments = _userService.GetPaymentsByUserId(id);
            if (payments == null)
            {
                return NotFound($"Payments not found");
            }
            var outputModel = _mapper.Map<List<PaymentOutputModel>>(payments);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/32
        /// <summary>Get payment by paymentId</summary>
        /// <param name="id">Id of payment</param>
        /// <returns>List of attached materials to tag</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("payment/{id}")]
        [Authorize(Roles = "Администратор,Менеджер")]
        public ActionResult<PaymentOutputModel> GetPayment(int id)
        {
            var payment = _userService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound($"Payment with id {id} is not found");
            }
            var outputModel = _mapper.Map<PaymentOutputModel>(payment);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        /// <summary>Update information about payment</summary>
        /// <param name="id">Id of payment</param>
        /// <param name="paymentInputModel">Nonupdated info about user </param>
        /// <returns>Updated info about payment</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("payment/{id}")]
        [Authorize(Roles = "Администратор,Менеджер")]
        public ActionResult<PaymentOutputModel> UpdatePayment(int id, [FromBody] PaymentInputModel paymentInputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            if (_userService.GetPaymentById(id) == null)
            {
                return NotFound($"Payment with id {id} is not found");
            }
            var paymentDto = _mapper.Map<PaymentDto>(paymentInputModel);
            _userService.UpdatePayment(id, paymentDto);
            var outputModel = _mapper.Map<PaymentOutputModel>(_userService.GetPaymentById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/find-debt
        /// <summary>Get students who not paid in selected month</summary>
        /// <param name="month">month as selected period</param>
        /// <returns>List of students who not paid in selected month</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpGet("find-debt")]
        [Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<List<UserOutputModel>> GetStudentsNotPaidInMonth([FromBody] MonthInputModel month)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException(ModelState);
            }
            var students = _userService.GetStudentsNotPaidInMonth(Converters.StrToDateTimePeriod(month.Month));
            if (students == null)
            {
                return NotFound($"Students not found");
            }
            var outputModel = _mapper.Map<List<UserOutputModel>>(students);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        /// <summary>Hard delete payment</summary>
        /// <param name="id">Id of deleted payment</param>
        /// <returns>Status204NoContent response</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("payment/{id}")]
        [Authorize(Roles = "Администратор,Менеджер")]
        public ActionResult DeletePayment(int id)
        {
            _userService.DeletePayment(id);
            return NoContent();
        }

        // https://localhost:50221/user/42/attendances
        /// <summary>Get attandenced of user</summary>
        /// <param name="userId">Id of user</param>
        /// <returns>List of attandences of current user</returns>
        [ProducesResponseType(typeof(List<AttendanceOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}/attendances")]
        [Authorize(Roles = "Администратор, Преподаватель, Менеджер")]
        public ActionResult GetAttendancesByUserId(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound($"User with id {userId} is not found");
            }
            var attendanceDto = _lessonService.GetAttendancesByUserId(userId);
            if (attendanceDto == null)
            {
                return NotFound($"Attendance with userId {userId} is not found");
            }
            var outputModel = _mapper.Map<List<AttendanceOutputModel>>(attendanceDto);
            return Ok(outputModel);
        }
    }
}


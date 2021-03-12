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
        
        public UserController(IMapper mapper, IUserService userService, ILessonService lessonService)
        {
            _mapper = mapper;
            _userService = userService;
            _lessonService = lessonService;
        }

        // https://localhost:44365/api/user/register
        /// <summary>user registration</summary>
        /// <param name="inputModel">information about registered user</param>
        /// <returns>rReturn information about added user</returns>
        [ProducesResponseType(typeof(UserOutputModel), StatusCodes.Status200OK)]
        [HttpPost("register")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult<UserOutputModel> Register([FromBody] UserInputModel inputModel)
        {
            var userDto = _mapper.Map<UserDto>(inputModel);
            if (String.IsNullOrEmpty(inputModel.Password) && String.IsNullOrEmpty(inputModel.Login))
            {
                return Problem("Не заполнены поля Password и Login ");
            }
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
        [HttpPut("change-password")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
           if(_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.ChangePassword(id, oldPassword,newPassword);
            return NoContent();
        }

        // https://localhost:44365/api/user
        /// <summary>Get info of all users</summary>
        /// <returns>List of all users, but not deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
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
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult<UserOutputModel> GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/passed-homework/by-group/42
        /// <summary>Get info of student who have submitted their homework</summary>
        /// <param name="groupId">Id of group in which students study</param>
        /// <returns>List of students</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("passed-homework/by-group/{groupId}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult<List<UserOutputModel>> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
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
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult<UserOutputModel> UpdateUserInfo(int id,[FromBody] UserInputModel inputModel)
        {
            var userDto = _mapper.Map<UserDto>(inputModel);
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.UpdateUser(id, userDto);
            var outputModel = _mapper.Map<UserOutputModel>(_userService.GetUserById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/42
        /// <summary>Change value of parametr "IsDeleted" to 1(Deleted)</summary>
        /// <param name="id">Id of user</param>
        /// <returns>Update user, which is deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputExtendedModel>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<UserOutputExtendedModel> DeleteUser(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }

            var result = _userService.DeleteUser(id);
            if (result == 1)
            {
                var outputModel = _mapper.Map<UserOutputExtendedModel>(_userService.GetUserById(id));
                return Ok(outputModel);
            }

            return Problem($"Ошибка! Не удалось удалить пользователя #{id}!");
        }

        // https://localhost:44365/api/user/42/recovery
        /// <summary>Change value of parametr "IsDeleted" to 0(Not deleted)</summary>
        /// <param name="id">Id of user</param>
        /// <returns>Update user, which is not deleted</returns>
        [ProducesResponseType(typeof(List<UserOutputExtendedModel>), StatusCodes.Status200OK)]
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<UserOutputExtendedModel> RecoverUser(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }

            var result = _userService.RecoverUser(id);
            if (result == 1)
            {
                var outputModel = _mapper.Map<UserOutputExtendedModel>(_userService.GetUserById(id));
                return Ok(outputModel);
            }

            return Problem($"Ошибка! Не удалось восстановить пользователя #{id}!");
        }

        // https://localhost:44365/api/user/88/payment
        /// <summary>Add payment to student</summary>
        /// <param name="id">Id of student</param>
        /// /// <param name="payment">information about payment</param>
        /// <returns>Information about added payment</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [HttpPost("{userId}/payment")]
        [Authorize(Roles = "Админ, Менеджер, Студент")]
        public ActionResult<PaymentOutputModel> AddPayment(int id, [FromBody] PaymentInputModel payment)
        {
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
        [HttpGet("payment/by-period")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<List<PaymentOutputModel>> GetPaymentsByPeriod([FromBody] PeriodInputModel periodInput)
        {
            var payments = _userService.GetPaymentsByPeriod(Converters.StrToDateTimePeriod(periodInput.PeriodFrom), Converters.StrToDateTimePeriod(periodInput.PeriodTo));
            var outputModels = _mapper.Map<List<PaymentOutputModel>>(payments);
            return Ok(outputModels);
        }

        //https://localhost:44365/api/user/42/payment
        /// <summary>Get payments by student id</summary>
        /// <param name="id">Id of student</param>
        /// <returns>List of payments of student</returns>
        [ProducesResponseType(typeof(List<PaymentOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("{id}/payment")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult<List<PaymentOutputModel>> GetPaymentsByUserId(int id)
        {
            var payments = _userService.GetPaymentsByUserId(id);
            var outputModel = _mapper.Map<List<PaymentOutputModel>>(payments);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/32
        /// <summary>Get payment by paymentId</summary>
        /// <param name="id">Id of payment</param>
        /// <returns>List of attached materials to tag</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [HttpGet("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult<PaymentOutputModel> GetPayment(int id)
        {

            var payment = _userService.GetPaymentById(id);
            var outputModel = _mapper.Map<PaymentOutputModel>(payment);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        /// <summary>Update information about payment</summary>
        /// <param name="id">Id of payment</param>
        /// <param name="payment">Nonupdated info about user </param>
        /// <returns>Updated info about payment</returns>
        [ProducesResponseType(typeof(PaymentOutputModel), StatusCodes.Status200OK)]
        [HttpPut("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult<PaymentOutputModel> UpdatePayment(int id, [FromBody] PaymentInputModel payment)
        {
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            _userService.UpdatePayment(id, paymentDto);
            var outputModel = _mapper.Map<PaymentOutputModel>(_userService.GetPaymentById(id));
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/find-debt
        /// <summary>Get students who not paid in selected month</summary>
        /// <param name="month">month as selected period</param>
        /// <returns>List of students who not paid in selected month</returns>
        [ProducesResponseType(typeof(List<UserOutputModel>), StatusCodes.Status200OK)]
        [HttpGet("find-debt")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult<List<UserOutputModel>> GetStudentsNotPaidInMonth([FromBody] MonthInputModel month)
        {
            var students = _userService.GetStudentsNotPaidInMonth(Converters.StrToDateTimePeriod(month.Month));
            var outputModel = _mapper.Map<List<UserOutputModel>>(students);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        /// <summary>Hard delete payment</summary>
        /// <param name="id">Id of deleted payment</param>
        /// <returns>Status204NoContent response</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
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
        [HttpGet("{userId}/attendances")]
        [Authorize(Roles = "Админ, Преподаватель, Менеджер")]
        public ActionResult GetAttendancesByUserId(int userId)
        {
            var attendanceDto = _lessonService.GetAttendancesByUserId(userId);
            var outputModel = _mapper.Map<List<AttendanceOutputModel>>(attendanceDto);
            return Ok(outputModel);
        }
    }
}


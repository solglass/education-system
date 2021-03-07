using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.API.Mappers;
using EducationSystem.API.Models;
using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
using EducationSystem.Data;
using EducationSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using AutoMapper;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;

namespace EducationSystem.Controllers
{
    // https://localhost:44365/api/user/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
       
        private IPaymentRepository _prepo;
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IPaymentRepository paymentRepository, IUserService userService)

        {
            _prepo = paymentRepository;
            _mapper = mapper;
            _userService = userService;
        }

        // https://localhost:44365/api/user/register
        [HttpPost("register")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult Register([FromBody] UserInputModel inputModel)
        {
            var userDto = _mapper.Map<UserDto>(inputModel);
            if (String.IsNullOrEmpty(inputModel.Password) && String.IsNullOrEmpty(inputModel.Login))
            {
                return Problem("Не заполнены поля Password и Login ");
            }
            _userService.AddUser(userDto);
            return Ok("Пользователь зарегистрирован");
        }

        // https://localhost:44365/api/user/change-password
        [HttpPut("change-password")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
           if(_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.ChangePassword(id, oldPassword,newPassword);
            return Ok("Пароль обновлён");
        }

        // https://localhost:44365/api/user
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            var outputModels = _mapper.Map<List<UserOutputModel>>(users);
            return Ok(outputModels);
        }

        // https://localhost:44365/api/user/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/passed-homework/by-group/42
        [HttpGet("passed-homework/by-group/{groupId}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            var user = _userService.GetPassedStudentsAttempt_SelectByGroupId(groupId);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:44365/api/user/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент, Методист")]
        public ActionResult UpdateUserInfo(int id,[FromBody] UserInputModel inputModel)
        {
            var userDto = _mapper.Map<UserDto>(inputModel);
            userDto.Id = id;
            if (_userService.GetUserById(userDto.Id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.UpdateUser(userDto);
            return Ok("Обновлено");
        }

        // https://localhost:44365/api/user/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult DeleteUser(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }

            var result = _userService.DeleteUser(id);
            if (result == 1)
            {
                return Ok($"Пользователь #{id} удален!");
            }

            return Problem($"Ошибка! Не удалось удалить пользователя #{id}!");
        }

        // https://localhost:44365/api/user/42/recovery
        [HttpPut("{id}/recovery")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult RecoverUser(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }

            var result = _userService.RecoverUser(id);
            if (result == 1)
            {
                return Ok($"Пользователь #{id} восстановлен!");
            }

            return Problem($"Ошибка! Не удалось восстановить пользователя #{id}!");
        }

        // https://localhost:44365/api/user/88/payment
        [HttpPost("{userId}/payment")]
        [Authorize(Roles = "Админ, Менеджер, Студент")]
        public ActionResult AddPayment(int id, [FromBody] PaymentInputModel payment)
        {
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            paymentDto.Student.Id = id;
            _prepo.AddPayment(paymentDto);
            return Ok("Платеж добавлен");
        }

        //https://localhost:44365/api/user/payment/by-period
        [HttpGet("payment/by-period")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult GetPaymentsByPeriod([FromBody] PeriodInputModel periodInput)
        {                             
            return Ok(_userService
                .GetPaymentsByPeriod(
                Converters.StrToDateTimePeriod(periodInput.PeriodFrom), 
                Converters.StrToDateTimePeriod(periodInput.PeriodTo)
                ));
        }

        //https://localhost:44365/api/user/42/payment
        [HttpGet("{id}/payment")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult GetPaymentsByUserId(int id)
        {            
            return Ok(_userService.GetPaymentsByUserId(id));
        }

        //https://localhost:44365/api/user/payment/32
        [HttpGet("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public dynamic GetPayment(int id)
        {
            var payment = _prepo.GetPaymentById(id);
            var outputModel = _mapper.Map<PaymentOutputModel>(payment);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        [HttpPut("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult UpdatePayment(int id, [FromBody] PaymentInputModel payment)
        {
            var paymentDto = _mapper.Map<PaymentDto>(payment);
            paymentDto.Id = id;
            _prepo.UpdatePayment(paymentDto);
            return Ok("success");
        }

        // https://localhost:44365/api/user/find-debt
        [HttpGet("find-debt")]
        [Authorize(Roles = "Админ, Менеджер")]
        public ActionResult GetStudentsNotPaidInMonth([FromBody] MonthInputModel month)
        {
            var students = _prepo.GetStudentsNotPaidInMonth(Converters.StrToDateTimePeriod(month.Month));
            var outputModel = _mapper.Map<GroupOutputModel>(students);
            return Ok(outputModel);
        }

        //https://localhost:44365/api/user/payment/42
        [HttpDelete("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult DeletePayment(int id)
        {
            _prepo.DeletePayment(id);
            return Ok("Платеж удален");
        }
    }
}


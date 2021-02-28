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

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/user/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
       
        private PaymentRepository _prepo;
        private PaymentMapper _pMapper;
        private UserMapper _userMapper;
        private UserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)

        {
            _mapper = mapper;
            _userMapper = new UserMapper();
            _userService = new UserService();
        }

        // https://localhost:50221/api/user/register
        [HttpPost("register")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult Register([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            if (String.IsNullOrEmpty(inputModel.Password) && String.IsNullOrEmpty(inputModel.Login))
            {
                return Problem("Не заполнены поля Password и Login ");
            }
            _userService.AddUser(userDto);
            return Ok("пользователь зарегистрирован");
        }

        // https://localhost:50221/api/user/change-password
        [HttpPut("change-password")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
           if(_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.ChangePassword(id, oldPassword,newPassword);
            return Ok("Пороль обновлён");
        }

        // https://localhost:50221/api/user
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        // https://localhost:50221/api/user/42
        [HttpGet("{id}")]
        //[Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        // https://localhost:50221/api/user/passed-homework/group/42
        [HttpGet("/passed-homework/group/{groupId}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            var user = _userService.GetPassedStudentsAttempt_SelectByGroupId(groupId);
            return Ok(user);
        }

        // https://localhost:50221/api/user/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult UpdateUserInfo([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            if (_userService.GetUserById(userDto.Id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.UpdateUser(userDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/user/42
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
                return Ok($"Пользователь #{id} удален!");
            else
                return Problem($"Ошибка! Не удалось удалить пользователя #{id}!");
        }

        // https://localhost:50221/api/user/42/recovery
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
                return Ok($"Пользователь #{id} восстановлен!");
            else
                return Problem($"Ошибка! Не удалось восстановить пользователя #{id}!");
        }

        // https://localhost:50221/api/user/payment/payment/name
        [HttpPost("payment/name")]
        [Authorize(Roles = "Менеджер,Студент")]
        public ActionResult AddPayment([FromBody] PaymentInputModel payment)
        {
            var result = _pMapper.ToDto(payment);
            _prepo.AddPayment(result);
            return Ok("Платеж добавлен");
        }

        //https://localhost:50221/api/user/payment/payment
         [HttpGet("payment")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult GetPayments()
        {
            var payments = _prepo.GetPayments();
            return Ok(payments);
        }

        //https://localhost:50221/api/user/payment/payment/32
         [HttpGet("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public dynamic GetPayment(int id)
        {
            var payment = _prepo.GetPaymentById(id);
            return Ok(payment);
        }
        //https://localhost:50221/api/user/payment/payment/42
        [HttpPut("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult UpdatePayment(int id, [FromBody] PaymentDto payment)
        {
            _prepo.UpdatePayment(id,payment);
            return Ok("success");
        }
        // https://localhost:50221/api/group/student-group/02.2020
        [HttpGet("student-group/{period}")]
        [Authorize(Roles = "Админ, Менеджер, Преподаватель, Тьютор")]
        public ActionResult GetStudentsByIsPaidInPeriod(string period)
        {
            var group = _prepo.GetStudentsByIsPaidInPeriod(period);
            return Ok(group);
        }

        //https://localhost:50221/api/user/payment/payment/42
        [HttpDelete("payment/{id}")]
        [Authorize(Roles = "Админ,Менеджер")]
        public ActionResult DeletePayment(int id)
        {
            _prepo.DeletePayment(id);
            return Ok("success");
        }




    }
}


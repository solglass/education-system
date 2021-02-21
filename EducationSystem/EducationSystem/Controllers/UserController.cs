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
using EducationSystem.API.Utils;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/user/
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
       
        private PaymentRepository _prepo;
        private PaymentMapper _mapper;
        private UserMapper _userMapper;
        private RoleMapper _roleMapper;
        private UserService _userService;
        private Converters _converter;
        public UserController()

        {
            _userMapper = new UserMapper();
            _roleMapper = new RoleMapper();
            _userService = new UserService();
            _converter = new Converters();
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
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
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
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteUser(int id)
        {
            if (_userService.GetUserById(id) == null)
            {
                return Problem("Не найден пользователь");
            }
            _userService.DeleteUser(id);
            return Ok("Удалено");
        }

        //https://localhost:50221/api/role/
        [HttpPost]
        [Authorize(Roles = "Админ")]
        public ActionResult AddRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            _userService.AddRole(roleDto);
            return Ok("Роль добавлена");
        }

        // https://localhost:50221/api/role
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetURoles()
        {
            var roles = _userService.GetRoles();
            return Ok(roles);
        }

        // https://localhost:50221/api/role/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetRoleById(int id)
        {
            var user = _userService.GetRole(id);
            return Ok(user);
        }

        // https://localhost:50221/api/role/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult UpdateRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            if (_userService.GetRole(roleDto.Id) == null)
            {
                return Problem("Роль не найдена");
            }
            _userService.UpdateRole(roleDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/role/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteRole(int id)
        {
            if (_userService.GetRole(id) == null)
            {
                return Problem("Роль не найдена");
            }
            _userService.DeleteRole(id);
            return Ok("Удалено");
        }

        // https://localhost:50221/api/user/payment/payment/name
        [HttpPost("payment/name")]
        [Authorize(Roles = "Менеджер,Студент")]
        public ActionResult AddPayment([FromBody] PaymentInputModel payment)
        {
            var result = _mapper.ToDto(payment);    
            _prepo.AddPayment(result);
            return Ok("Платеж добавлен");
        }

        //https://localhost:50221/api/user/payment/payment
        [HttpGet("period")]
        //[Authorize(Roles = "Админ,Менеджер")]
        public ActionResult GetPayments(string periodFrom, string periodTo)
        {
            DateTime perFrom = Converters.StrToDateTime(periodFrom).Item2;
            DateTime perTo = Converters.StrToDateTime(periodTo).Item2;
            periodFrom = Converters.PeriodDateToStr(perFrom);
            periodTo = Converters.PeriodDateToStr(perTo);
            var payments = _userService.GetPaymentsByPeriod(periodFrom, periodTo);
            return Ok(payments);
        }

        //https://localhost:50221/api/user/payment/payment/32
         [HttpGet("payment/{id}")]
        //[Authorize(Roles = "Админ,Менеджер")]
        public dynamic GetPayment(int id)
        {
            var payment = _userService.GetPaymentById(id);
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


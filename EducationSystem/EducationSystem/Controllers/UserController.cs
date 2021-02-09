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

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/user/
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserRepository _repo;
        private PaymentRepository _prepo;
        private PaymentMapper _mapper;
        private UserMapper _userMapper;
        private RoleMapper _roleMapper;
        private UserService _userService;
        public UserController()

        {
            _repo = new UserRepository();
            _userMapper = new UserMapper();
            _roleMapper = new RoleMapper();
            _userService = new UserService();
        }

        // https://localhost:50221/api/user/register
        [HttpPost("register")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult Register([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            _repo.AddUser(userDto);
            return Ok("пользователь зарегистрирован");
        }

        // https://localhost:50221/api/user/change-password
        [HttpPut("change-password")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            _repo.ChangeUserPassword(id, oldPassword, newPassword);
            return Ok("Пороль обновлён");
        }

        // https://localhost:50221/api/user
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetUsers()
        {
            var users = _repo.GetUsers();
            return Ok(users);
        }

        // https://localhost:50221/api/user/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetUser(int id)
        {
            var user = _repo.GetUserById(id);
            return Ok(user);
        }

        // https://localhost:50221/api/user/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult UpdateUserInfo([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            _repo.UpdateUser(userDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/user/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteUser(int id)
        {
            _repo.DeleteUser(id);
            return Ok("Удалено");
        }

        //https://localhost:50221/api/role/
        [HttpPost]
        [Authorize(Roles = "Админ")]
        public ActionResult AddRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            _repo.AddRole(roleDto);
            return Ok("Роль добавлена");
        }

        // https://localhost:50221/api/role
        [HttpGet]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetURoles()
        {
            var roles = _repo.GetRoles();
            return Ok(roles);
        }

        // https://localhost:50221/api/role/42
        [HttpGet("{id}")]
        [Authorize(Roles = "Админ,Менеджер, Преподаватель, Тьютор, Студент")]
        public ActionResult GetRoleById(int id)
        {
            var user = _repo.GetRoleById(id);
            return Ok(user);
        }

        // https://localhost:50221/api/role/42
        [HttpPut("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult UpdateRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            _repo.UpdateRole(roleDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/role/42
        [HttpDelete("{id}")]
        [Authorize(Roles = "Админ")]
        public ActionResult DeleteRole(int id)
        {
            _repo.DeleteRole(id);
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


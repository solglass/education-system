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
using Microsoft.Extensions.Logging;

namespace EducationSystem.Controllers
{
    // https://localhost:50221/api/user/
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private UserRepository _repo;
        private UserMapper _userMapper;
        private RoleMapper _roleMapper;
        private UserService _userService;
        public UserController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new UserRepository();
            _userMapper = new UserMapper();
            _roleMapper = new RoleMapper();
            _userService = new UserService();
        }

        // https://localhost:50221/api/user/register
        [HttpPost("register")]
        public ActionResult Register([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            _repo.AddUser(userDto);
            return Ok("пользователь зарегистрирован");
        }

        // https://localhost:50221/api/user/change-password
        [HttpPut("change-password")]
        public ActionResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            _repo.ChangeUserPassword(id, oldPassword, newPassword);
            return Ok("Пороль обновлён");
        }

        // https://localhost:50221/api/user
        [HttpGet]
        public ActionResult GetUsers()
        {
            var users = _repo.GetUsers();
            return Ok(users);
        }

        // https://localhost:50221/api/user/42
        [HttpGet("{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _repo.GetUserById(id);
            return Ok(user);
        }

        // https://localhost:50221/api/user/42
        [HttpPut("{id}")]
        public ActionResult UpdateUserInfo([FromBody] UserInputModel inputModel)
        {
            UserDto userDto;
            userDto = _userMapper.ToDto(inputModel);
            _repo.UpdateUser(userDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/user/42
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _repo.DeleteUser(id);
            return Ok("Удалено");
        }

        //https://localhost:50221/api/role/
        [HttpPost]
        public ActionResult AddRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            _repo.AddRole(roleDto);
            return Ok("Роль добавлена");
        }

        // https://localhost:50221/api/role
        [HttpGet]
        public ActionResult GetURoles()
        {
            var roles = _repo.GetRoles();
            return Ok(roles);
        }

        // https://localhost:50221/api/role/42
        [HttpGet("{id}")]
        public ActionResult GetRoleById(int id)
        {
            var user = _repo.GetRoleById(id);
            return Ok(user);
        }

        // https://localhost:50221/api/role/42
        [HttpPut("{id}")]
        public ActionResult UpdateRole([FromBody] RoleInputModel inputModel)
        {
            RoleDto roleDto;
            roleDto = _roleMapper.ToDto(inputModel);
            _repo.UpdateRole(roleDto);
            return Ok("Обновлено");
        }

        // https://localhost:50221/api/role/42
        [HttpDelete("{id}")]
        public ActionResult DeleteRole(int id)
        {
            _repo.DeleteRole(id);
            return Ok("Удалено");
        }


        //


        /* // https://localhost:50221/api/user/payment/payment/name
         [HttpPost("payment/name")]
         public ActionResult AddPayment([FromBody] PaymentInputModel payment)
         {
             _repo.AddPayment();
             return Ok("Платеж добавлен");
         }

         // https://localhost:50221/api/user/payment/payment
         [HttpGet("payment")]
         public ActionResult GetPayments()
         {
             var payments = _repo.GetPayments();
             return Ok(payments);
         }

         // https://localhost:50221/api/user/payment/payment/32
         [HttpGet("payment/{id}")]
         public dynamic GetPayment(int id)
         {
             var payment = _repo.GetPaymentById(id);
             return Ok(payment);
         }

         // https://localhost:50221/api/user/payment/payment/42
         [HttpPut("payment/{id}")]
         public ActionResult UpdatePayment(int id, [FromBody] PaymentDto payment)
         {
             _repo.UpdatePayment(payment);
             return Ok("success");
         }

         // https://localhost:50221/api/user/payment/payment/42
         [HttpDelete("payment/{id}")]
         public ActionResult DeletePayment(int id)
         {
             _repo.DeletePayment(id);
             return Ok("success");
         }

         */


    }
}


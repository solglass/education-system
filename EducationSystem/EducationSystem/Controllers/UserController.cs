using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /*private readonly ILogger<WeatherForecastController> _logger;
        private UserRepository _repo;

        public UserController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new UserRepository();
        }

        // https://localhost:50221/api/user/register
        [HttpPost("register")]
        public ActionResult Register([FromBody] dynamic user)
        {
            _repo.AddUser();
            return Ok("пользователь зарегистрирован");
        }

        // https://localhost:50221/api/user/change-password
        [HttpPut("change-password")]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            // _repo.ChangePassword(oldPassword, newPassword)
            return Ok("success");
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
        public dynamic GetUser(int id)
        {
            var user = _repo.GetUserById(id);
            return Ok(user);
        }

        // https://localhost:50221/api/user/42
        [HttpPut("{id}")]
        public ActionResult UpdateUserInfo(int id, [FromBody] dynamic data)
        {
            _repo.UpdateUser();
            return Ok("success");
        }

        // https://localhost:50221/api/user/42
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _repo.DeleteUser();
            return Ok("success");
        }
        // https://localhost:50221/api/user/payment/payment/name
        [HttpPost("payment/name")]
        public ActionResult AddPayment(int contractNumber, decimal amount, DateTime date, string period, bool IsPaid, [FromBody] dynamic payment)
        {

            _repo.AddPayment(contractNumber, amount, date, period, IsPaid);
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


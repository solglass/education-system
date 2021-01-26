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
    // https://localhost:50221/api/payment/
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private UserRepository _repo;

        public PaymentController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _repo = new UserRepository();
        }

        // https://localhost:50221/api/payment/register
        [HttpPost("register")]
        public ActionResult Register(int contractNumber, decimal amount, DateTime date, string period, bool IsPaid,[FromBody] dynamic payment)
        {
            _repo.AddPayment(contractNumber, amount, date,period, IsPaid);
            return Ok("Платеж добавлен");
        }

        // https://localhost:50221/api/payment
        [HttpGet]
        public ActionResult GetPayments()
        {
            var payments = _repo.GetPayments();
            return Ok(payments);
        }

        // https://localhost:50221/api/payment/42
        [HttpGet("{id}")]
        public dynamic GetPayment(int id)
        {
            var payment = _repo.GetPaymentById(id);
            return Ok(payment);
        }

        // https://localhost:50221/api/payment/42
        [HttpPut("{id}")]
        public ActionResult UpdatePayment(int contractNumber, decimal amount, DateTime date, string period, bool IsPaid, [FromBody] dynamic data)
        {
            _repo.UpdatePayment(contractNumber,amount,date,period,IsPaid);
            return Ok("success");
        }

        // https://localhost:50221/api/payment/42
        [HttpDelete("{id}")]
        public ActionResult DeletePayment(int id)
        {
            _repo.DeletePayment(id);
            return Ok("success");
        }
    }
}

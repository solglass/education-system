using EducationSystem.API.Models.InputModels;
using EducationSystem.Business;
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
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationService _service;
        public AuthenticationController()
        {
            _service = new AuthenticationService();
        }
        [HttpPost]
        public ActionResult Login([FromBody] AuthenticationInputModel login)
        {
            //Hash function
            var user = _service.GetAuthentificatedUser(login.Login, login.Password);
            if( user != null)
            {
                var token = _service.GenerateToken(user);
                return Ok(token);
            }
            return Ok("");
        }
    }
}

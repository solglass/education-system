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
        private SecurityService _securityService;
        public AuthenticationController()
        {
            _service = new AuthenticationService();
            _securityService = new SecurityService();
        }
        [HttpPost]
        public ActionResult Authentificate([FromBody] AuthenticationInputModel login)
        {           
            var user = _service.GetAuthentificatedUser(login.Login);          
            if( user != null && _securityService.VerifyHashAndPassword(user.Password, login.Password))
            {
                var token = _service.GenerateToken(user);
                return Ok(token);
            }
            return Ok("");
        }
    }
}

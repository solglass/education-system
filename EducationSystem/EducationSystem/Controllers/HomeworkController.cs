using EducationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeworkController : ControllerBase
    {

        private readonly ILogger<HomeworkController> _logger;
        private HomeworkRepository _repo;

        public HomeworkController()
        {
            _repo = new HomeworkRepository();
        }

    }
}

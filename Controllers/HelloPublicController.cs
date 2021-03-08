using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloPublicController : ControllerBase
    {
        private HelloPrivateService _helloPrivateService;

        public HelloPublicController(HelloPrivateService helloPrivateService)
        {
            _helloPrivateService = helloPrivateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string response = await _helloPrivateService.HelloPrivate();

            response = "Hello from public - " + response;

            return Ok(response);
        }
    }
}

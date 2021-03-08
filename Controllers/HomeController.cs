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
    public class HomeController : ControllerBase
    {
        public HelloPublicService _helloPublicService;

        public HomeController(HelloPublicService helloPublicService)
        {
            _helloPublicService = helloPublicService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _helloPublicService.HelloPublic();

            return Ok(response.Result);
        }
    }
}

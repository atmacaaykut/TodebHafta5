using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("VerifyPassword")]
        public IActionResult VerifyPassword(string email, string password)
        {
            var response = _authService.VerifyPassword(email, password);
            return Ok(response);

        }

        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            var response = _authService.Login(email, password);
            return Ok(response);
        }
    }
}

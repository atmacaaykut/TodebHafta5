using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Abstract;
using DTO.User;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register(CreateUserRegisterRequest register)
        {
            var response=_userService.Register(register);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _userService.GetAll();
            return Ok(data);
        }

    }
}

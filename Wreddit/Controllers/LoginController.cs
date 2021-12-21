using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;
using Wreddit.Services.UserServices;

namespace Wreddit.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public LoginController(IRepositoryWrapper repository,
                               UserManager<User> userManager,
                               IUserService userService)
        {
            _repository = repository;
            _userManager = userManager;
            _userService = userService;
        }

     [HttpPost]
     public async Task<IActionResult> CheckUser([FromBody]LoginUserDTO user)
        {
            var token = await _userService.LoginUser(user);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }


    }
}

using Microsoft.AspNetCore.Authorization;
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
    [Route("signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public SignupController(IRepositoryWrapper repository,
                                UserManager<User> userManager,
                                IUserService userService)    
        {
            _repository = repository;
            _userManager = userManager;
            _userService = userService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateUser(User newUser)
        //{
        //    _repository.User.Create(newUser);
        //    await _repository.SaveAsync();
        //    return Ok(newUser);
        //}


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO newUser)
        {
            var exists = await _userManager.FindByEmailAsync(newUser.Email);

            if (exists != null)
            {
                return BadRequest("User already registered!");
            }

            var result = await _userService.RegisterUserAsync(newUser);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}

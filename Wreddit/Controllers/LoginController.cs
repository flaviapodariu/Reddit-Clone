using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Repositories;

namespace Wreddit.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public LoginController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CheckUser(User user)
        {
            Task<User> user2 = _repository.User.GetUserByEmail(user.Email);
            if (user2 == null)
            {
                 //_repository.SaveAsync();
                return Ok(user);
            }
            //await _repository.SaveAsync();
            return NotFound("Userul nu exista");
            //return Ok(user);
        }



    }
}

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
    [Route("signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public SignupController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            _repository.User.Create(newUser);
            await _repository.SaveAsync();
            return Ok(newUser);
        }
    }
}

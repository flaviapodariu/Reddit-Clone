using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;

namespace Wreddit.Controllers
{   
    [Authorize(Roles= "Admin")]
    [Route("admin-dashboard")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public AdminController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
           var users = await _repository.User.GetAllUsers();
            return Ok(users);
        }


    }
}

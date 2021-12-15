using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Entities;
using Wreddit.Repositories;

namespace Wreddit.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IGenericRepository<User> _repository;
        public LoginController(IGenericRepository<User> repository)
        {
            _repository = repository;
        }


    }
}

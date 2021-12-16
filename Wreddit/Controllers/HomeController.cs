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
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public HomeController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

    }
}

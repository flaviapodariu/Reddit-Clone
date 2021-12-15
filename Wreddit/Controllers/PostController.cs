using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;
using Wreddit.Entities;

namespace Wreddit.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IGenericRepository<Post> _repository;
        public PostController(IGenericRepository<Post> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById([FromQuery] int id)
        {
            var postToReturn = await _repository.GetByIdAsync(id);
            return Ok(postToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post newPost)
        {
            _repository.Create(newPost);
            await _repository.SaveAsync();
            return Ok(newPost);
        }

       // public ActionResult 
    }
}

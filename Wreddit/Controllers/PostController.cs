using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;
using Wreddit.Models.Entities;


namespace Wreddit.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public PostController(IRepositoryWrapper  repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("getPosts")]
        public IQueryable<Post> GetAllPosts()
        {
            IQueryable<Post> posts = _repository.Post.GetAll();
            return posts;
        }

        [HttpGet]
        //[Route("post/{id}")]
        public async Task<IActionResult> GetPostById([FromQuery] int id)
        {
            var postToReturn = await _repository.Post.GetByIdAsync(id);
            return Ok(postToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post newPost)
        {
            _repository.Post.Create(newPost);
            await _repository.SaveAsync();
            return Ok(newPost);
        }

       // public ActionResult 
    }
}

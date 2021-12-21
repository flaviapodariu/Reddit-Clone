using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

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
        public async Task<IActionResult> GetAllPosts()
        {
            List<Post> posts = await _repository.Post.GetAllPostsWithUsers(); // join on user table

            var postsToReturn = new List<PostDTO>();
            foreach(var post in posts)
            {
                var userToReturn = new UserDTO(post.User);  
                var postDto = new PostDTO(post);
                postDto.User = userToReturn;
                postsToReturn.Add(postDto);
            }
            return Ok(postsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _repository.Post.GetPostWithComments(id);   
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post newPost)
        {
            _repository.Post.Create(newPost);
            await _repository.SaveAsync();
            return Ok(newPost);
        }

    }
}

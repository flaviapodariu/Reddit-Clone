using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Wreddit.Services.UserServices;


namespace Wreddit.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        public PostController(IRepositoryWrapper  repository,
                              IUserService userService)
        {
            _repository = repository;
            _userService = userService;
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
            var post = await _repository.Post.GetPostWithUser(id);
            var postToReturn = new PostDTO(post);
            var userToReturn = new UserDTO(post.User);

            postToReturn.User = userToReturn;
            return Ok(postToReturn);
        }

        [HttpPost] 
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreatePost([FromBody]Post newPost)
        {
            _repository.Post.Create(newPost);
            await _repository.SaveAsync();
            return Ok(newPost);
        }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateVotes([FromBody]PostVoteDTO dto)
        {
            await _repository.Post.UpdatePostVotes(dto);
            await _repository.SaveAsync();
            return Ok(dto);
        }

        [HttpDelete]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeletePost([FromBody] DeleteDTO dto)
        {
            var postToDelete = await _repository.Post.GetByIdAsync(dto.Id);
            try
            {
                if (postToDelete.UserId == dto.UserId || _userService.ValidateAdminRole(dto.Token))
                {

                    _repository.PostVotes.DeleteById(dto.Id);

                    var comments = await _repository.Comment.GetCommentsFromPost(dto.Id);
                    foreach(var comm in comments)
                    {
                        _repository.CommentVotes.DeleteById(comm.Id);
                    }

                    _repository.Post.Delete(postToDelete);

                }
                    
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine($"Post does not exist or was not provided: {e}");
            }

            await _repository.SaveAsync();
            return Ok(dto);
        }
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("votes/{id}")]
        public async Task<IActionResult> GetPostVotesFromUser(int id)
        {
            List<PostVotes> votes = await _repository.User.GetUsersPostVotes(id);
            return Ok(votes);
        }
    }
}

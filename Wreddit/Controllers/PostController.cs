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

                List<Comment> comms = await _repository.Comment.GetCommentsFromPost(post.Id);
                postDto.NrComm = comms.Count();

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

            List<Comment> comms = await _repository.Comment.GetCommentsFromPost(id);
            postToReturn.NrComm = comms.Count();

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


        [HttpDelete]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeletePost([FromBody] DeleteDTO dto)
        {
            var postToDelete = await _repository.Post.GetByIdAsync(dto.Id);
            try
            {
                if (postToDelete.UserId == dto.UserId || _userService.ValidateAdminRole(dto.Token))
                {

                    await _repository.PostVotes.DeleteByPostId(dto.Id);

                    var comments = await _repository.Comment.GetCommentsFromPost(dto.Id);
                    foreach(var comm in comments)
                    {
                       await _repository.CommentVotes.DeleteByCommentId(comm.Id);
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

        [Route("/posts/user/{userId}")]
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetPostsByUser(int userId)
        {
           var posts = await _repository.Post.GetPostsByUser(userId);
           var postsToReturn = new List<PostDTO>();
           var user = await _repository.User.GetByIdAsync(userId);

            foreach (var post in posts)
            {
                var postDto = new PostDTO(post);

                List<Comment> comms = await _repository.Comment.GetCommentsFromPost(post.Id);
                postDto.NrComm = comms.Count();

                postDto.User = new UserDTO(user);
                postsToReturn.Add(postDto);
            }
            return Ok(postsToReturn);
        }
    }
}

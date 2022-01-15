using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;
using Wreddit.Services.UserServices;

namespace Wreddit.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;

        public CommentController(IRepositoryWrapper repository,
                                  IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles="User, Admin")]
        public async Task<IActionResult> CreateComment(Comment comm)
        {
            _repository.Comment.Create(comm);
            await _repository.SaveAsync();
            var commToReturn = new CommentDTO(comm);
            return Ok(commToReturn);
        }
        [HttpGet("{PostId}")]
        public async Task<IActionResult> GetCommentsFromPost(int PostId)
        {
            var comments = await _repository.Comment.GetCommentsFromPost(PostId);

            var commentsToReturn = new List<CommentDTO>();
            foreach (var comment in comments)
            {
                var userToReturn = new UserDTO(comment.User);
                var commentDto = new CommentDTO(comment);
                commentDto.User = userToReturn;
                commentsToReturn.Add(commentDto);
            }
            return Ok(commentsToReturn);
        }

        [HttpDelete]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteDTO dto)
        {
            var commToDelete = await _repository.Comment.GetByIdAsync(dto.Id);
            try
            {
                if (commToDelete.UserId == dto.UserId || _userService.ValidateAdminRole(dto.Token))
                {

                    _repository.CommentVotes.DeleteById(dto.Id);
                    _repository.Comment.Delete(commToDelete);

                }

            }
            catch (NullReferenceException e)
            {
                Console.WriteLine($"Post does not exist or was not provided: {e}");
            }

            await _repository.SaveAsync();
            return Ok();
        }
    }
    
}

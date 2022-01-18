using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;
using Wreddit.Services.UserServices;

namespace Wreddit.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;
        public UserController(IRepositoryWrapper repository,
                              IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpDelete]
        [Authorize(Roles="User, Admin")]
        public async Task<IActionResult> DeleteUserById([FromBody] DeleteDTO dto)
        {
            try
            {
                if (dto.Id == dto.UserId || _userService.ValidateAdminRole(dto.Token))
                {

                    await _repository.CommentVotes.DeleteByCommentByUserId(dto.Id);
                    await _repository.Comment.DeleteByUser(dto.Id);

                    await _repository.PostVotes.DeleteByPostByUserId(dto.Id);

                    var posts = await _repository.Post.GetPostsByUser(dto.Id);
                    foreach(var post in posts)
                    {
                        await _repository.CommentVotes.DeleteByPostId(post.Id);
                        await _repository.Comment.DeleteByPost(post.Id);
         
                    }

                    await _repository.Post.DeleteByUserId(dto.Id);

                    var userToDelete = await _repository.User.GetByIdAsync(dto.Id);
                    _repository.User.Delete(userToDelete);
                    await _repository.SaveAsync();
                    return Ok(userToDelete.UserName);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"User could not be deleted: {e}");
            }

           

            return Ok();
        }
    }
}

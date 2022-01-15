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
    [Route("comment-votes")]
    [ApiController]
    public class CommentVotesController : ControllerBase

    { 
            private readonly IRepositoryWrapper _repository;
            private readonly IUserService _userService;

            public CommentVotesController(IRepositoryWrapper repository,
                                   IUserService userService)
            {
                _repository = repository;
                _userService = userService;
            }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateVotes([FromBody] CommentVoteDTO dto)
        {
            await _repository.CommentVotes.UpdateCommentVotes(dto);
            await _repository.SaveAsync();
            return Ok(dto);
        }

    }
}

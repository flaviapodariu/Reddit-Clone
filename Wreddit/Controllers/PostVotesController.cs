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
    [Route("post-votes")]
    [ApiController]
    public class PostVotesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IUserService _userService;

        public PostVotesController(IRepositoryWrapper repository,
                               IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateVotes([FromBody] PostVoteDTO dto)
        {
            await _repository.PostVotes.UpdatePostVotes(dto);
            await _repository.SaveAsync();
            return Ok(dto);
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("{id}")]
        public async Task<IActionResult> GetPostVotesFromUser(int id)
        {
            List<PostVotes> votes = await _repository.PostVotes.GetUsersPostVotes(id);
            return Ok(votes);
        }
    }
}

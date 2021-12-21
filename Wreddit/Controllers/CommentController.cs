using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;

namespace Wreddit.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public CommentController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Comment comm)
        {
            _repository.Comment.Create(comm);
            await _repository.SaveAsync();
            return Ok(comm);
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
    }
}

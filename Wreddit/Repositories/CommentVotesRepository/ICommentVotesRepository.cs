using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

namespace Wreddit.Repositories
{
    public interface ICommentVotesRepository : IGenericRepository<CommentVotes>
    {
        void DeleteById(int PostId);
        Task<List<CommentVotes>> GetUsersCommentVotes(int id);
        Task<Comment> UpdateCommentVotes(CommentVoteDTO dto);
    }
}

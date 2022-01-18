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
        Task<List<CommentVotes>> DeleteByCommentId(int commentId);
        Task<List<CommentVotes>> DeleteByPostId(int postId);
        Task<List<CommentVotes>> DeleteByCommentByUserId(int userId);
       // Task<List<CommentVotes>> DeleteByPostByUserId(int userId);
        Task<List<CommentVotes>> GetUsersCommentVotes(int id);
        Task<Comment> UpdateCommentVotes(CommentVoteDTO dto);
    }
}

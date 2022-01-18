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
        Task<List<CommentVoteDTO>> GetUsersCommentVotes(int post_id, int user_id);
        Task<Comment> UpdateCommentVotes(CommentVoteDTO dto);
    }
}

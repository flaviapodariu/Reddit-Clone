using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

namespace Wreddit.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(WredditContext context) : base(context) { }

        public async Task<List<Comment>> GetCommentsFromPost(int PostId)
        {
            return await _context.Comments.Include(comment => comment.User).Where(comment => comment.PostId == PostId).ToListAsync();
        }

        public async Task<Comment> UpdateCommentVotes(CommentVoteDTO dto)
        {
            var comment = await _context.Comments.FindAsync(dto.CommentId);
            var userVote = await _context.CommentVotes.FindAsync(dto.CommentId, dto.UserId);
            if (userVote != null && userVote.VoteType == 1) //if user already voted 
            {
                if (dto.VoteType.ToLower() == "downvote")  //if vote is diff from the one in db
                {
                    comment.Downvotes += 1;
                    userVote.VoteType = -1;
                }
                else _context.CommentVotes.Remove(userVote);

                comment.Upvotes -= 1;    // take the original vote back

            }
            else
            if (userVote != null)
            {
                if (dto.VoteType.ToLower() == "upvote")
                {
                    comment.Upvotes += 1;
                    userVote.VoteType = 1;
                }
                else _context.CommentVotes.Remove(userVote); //if user upvoted and clicks on upvote again vote becomes null

                comment.Downvotes -= 1;

            }
            else
            if (dto.VoteType.ToLower() == "upvote")  //if user has never voted before
            {
                comment.Upvotes += 1;
                var newUserVote = new CommentVotes(dto.CommentId, dto.UserId, 1); // 1 = upvote
                _context.CommentVotes.Add(newUserVote);
            }
            else
            {
                comment.Downvotes += 1;
                var newUserVote = new CommentVotes(dto.CommentId, dto.UserId, -1); //-1 = downvote
                _context.CommentVotes.Add(newUserVote);
            }

            await _context.SaveChangesAsync();
            return comment;

        }
    
    }
}

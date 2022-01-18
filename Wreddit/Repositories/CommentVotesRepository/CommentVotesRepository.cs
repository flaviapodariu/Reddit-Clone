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
    public class CommentVotesRepository: GenericRepository<CommentVotes>, ICommentVotesRepository
    {
        public CommentVotesRepository(WredditContext context) : base(context) { }

        public async Task<List<CommentVotes>> DeleteByCommentId(int commentId)
        {
            var votesToDelete = await _context.CommentVotes.ToListAsync();
            votesToDelete.RemoveAll(c => c.CommentId.Equals(commentId));
            return votesToDelete;
        }

        public async Task<List<CommentVotes>> DeleteByPostId(int postId)
        {
            var votesToDelete = await _context.CommentVotes.Include(c => c.Comment)
                                              .Where(v => v.Comment.PostId == postId).ToListAsync();

            _context.CommentVotes.RemoveRange(votesToDelete.Where(v => v.Comment.PostId == postId));
            return votesToDelete;
        }

        public async Task<List<CommentVotes>> DeleteByCommentByUserId(int userId)
        {
            var votesToDelete = await _context.CommentVotes.Include(c => c.Comment).ToListAsync();
            _context.CommentVotes.RemoveRange(votesToDelete.Where(c => c.CommentId == c.Comment.Id && c.Comment.UserId == userId));
            return votesToDelete;
        }
        public async Task<List<CommentVoteDTO>> GetUsersCommentVotes(int post_id, int user_id) // returns the comments the user voted on 
        {
            var votes = _context.CommentVotes.Join(_context.Comments,
                                                       commentVote => commentVote.CommentId, // join between comment and comment votes
                                                       comment => comment.Id,
                                                       (commentVote, comment) => new     
                                                       {
                                                           commentVote.CommentId,
                                                           comment.PostId,
                                                           commentVote.UserId,
                                                           commentVote.VoteType
                                                       });
            List<CommentVoteDTO> commentVotesToReturn = votes.Where(v => v.PostId == post_id && v.UserId == user_id) // only take the votes for the specific post and user
                .Select(v => new CommentVotes(v.CommentId, v.UserId, v.VoteType))
                .AsEnumerable()
                .Select(v => new CommentVoteDTO(v))
                .ToList();
            return commentVotesToReturn;
        }
        public async Task<Comment> UpdateCommentVotes(CommentVoteDTO dto)
        {
            var comment = await _context.Comments.FindAsync(dto.CommentId);
            var userVote = await _context.CommentVotes.FindAsync(dto.CommentId, dto.UserId);
            if (userVote != null && userVote.VoteType == 1) //if user already voted 
            {
                if (dto.VoteType == -1)  //if vote is diff from the one in db
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
                if (dto.VoteType == 1)
                {
                    comment.Upvotes += 1;
                    userVote.VoteType = 1;
                }
                else _context.CommentVotes.Remove(userVote); //if user upvoted and clicks on upvote again vote becomes null

                comment.Downvotes -= 1;

            }
            else
            if (dto.VoteType == 1)  //if user has never voted before
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

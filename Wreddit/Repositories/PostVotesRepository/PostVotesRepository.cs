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
    public class PostVotesRepository: GenericRepository<PostVotes>, IPostVotesRepository
    {
        public PostVotesRepository(WredditContext context) : base(context) { }

        public async Task<List<PostVotes>> DeleteByPostId(int postId)
        {
            var votesToDelete =  await _context.PostVotes.ToListAsync();  
            votesToDelete.RemoveAll(p => p.PostId.Equals(postId));
            return votesToDelete;
        }
        public async Task<List<PostVotes>> GetUsersPostVotes(int id) // returns the posts the user voted on 
        {
            return await _context.PostVotes.Where(vote => vote.UserId == id).ToListAsync();
        }
        public async Task<Post> UpdatePostVotes(PostVoteDTO dto)
        {
            var post = await _context.Posts.FindAsync(dto.PostId);
            var userVote = await _context.PostVotes.FindAsync(dto.PostId, dto.UserId);
            if (userVote != null && userVote.VoteType == 1) //if user already voted 
            {
                if (dto.VoteType == -1)  //if vote is diff from the one in db
                {
                    post.Downvotes += 1;
                    userVote.VoteType = -1;
                }
                else _context.PostVotes.Remove(userVote);

                post.Upvotes -= 1;    // take the original vote back

            }
            else
            if (userVote != null)
            {
                if (dto.VoteType == 1)
                {
                    post.Upvotes += 1;
                    userVote.VoteType = 1;
                }
                else _context.PostVotes.Remove(userVote); //if user upvoted and clicks on upvote again vote becomes null

                post.Downvotes -= 1;

            }
            else
            if (dto.VoteType == 1)  //if user has never voted before
            {
                post.Upvotes += 1;
                var newUserVote = new PostVotes(dto.PostId, dto.UserId, 1); // 1 = upvote
                _context.PostVotes.Add(newUserVote);
            }
            else
            {
                post.Downvotes += 1;
                var newUserVote = new PostVotes(dto.PostId, dto.UserId, -1); //-1 = downvote
                _context.PostVotes.Add(newUserVote);
            }

            await _context.SaveChangesAsync();
            return post;

        }
        public async Task<List<PostVotes>> DeleteByPostByUserId(int userId)
        {
            var votesToDelete = await _context.PostVotes.Include(c => c.Post).ToListAsync();
            _context.PostVotes.RemoveRange(votesToDelete.Where(c => c.PostId == c.Post.Id && c.Post.UserId == userId));
            return votesToDelete;
        }
    }

         
}

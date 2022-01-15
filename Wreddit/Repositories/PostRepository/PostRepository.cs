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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(WredditContext context) : base(context) { }

        public async Task<List<Post>> GetAllPostsWithUsers()
        {
            return await _context.Posts.Include(post => post.User).ToListAsync();
        }

        public async Task<List<Post>> GetAllPostsWithComments()
        {
            return await _context.Posts.Include(post => post.Comments).ToListAsync();
        }


        public async Task<Post> GetPostWithComments(int id)
        {
            return await _context.Posts.Include(post => post.Comments).Where(post => post.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Post> GetPostWithUser(int id)
        {
            return await _context.Posts.Include(post => post.User).Where(post => post.Id == id).FirstOrDefaultAsync();
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
                else  _context.PostVotes.Remove(userVote); //if user upvoted and clicks on upvote again vote becomes null

                post.Downvotes -= 1;
               
            }
            else
            if (dto.VoteType  == 1)  //if user has never voted before
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
    }
}
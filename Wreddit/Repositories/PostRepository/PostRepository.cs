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

        public async Task<List<Post>> GetPostsByUser(int userId)
        {
            return await _context.Posts.Where(post => post.UserId == userId).ToListAsync();
        }

        public async Task<List<Post>> DeleteByUserId(int userId)
        {
            var postsToDelete = await _context.Posts.Include(p => p.PostVotes).Where(c => c.UserId == userId).ToListAsync();
            _context.Posts.RemoveRange(postsToDelete.Where(c => c.UserId == userId));
            return postsToDelete;
        }
    }
}
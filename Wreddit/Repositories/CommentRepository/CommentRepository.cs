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

        public async Task<List<Comment>> GetCommentsByUser(int UserId)
        {
            return await _context.Comments.Where(comment => comment.UserId == UserId).ToListAsync();
        }

        public async Task<List<Comment>> DeleteByUser(int UserId)
        {
            var commsToDelete = await _context.Comments.Where(c => c.UserId == UserId).ToListAsync();
            _context.Comments.RemoveRange(commsToDelete.Where(c => c.UserId == UserId));
            return commsToDelete;
        }
        public async Task<List<Comment>> DeleteByPost(int PostId)
        {
            var commsToDelete = await _context.Comments.Where(c => c.PostId == PostId).ToListAsync();
            _context.Comments.RemoveRange(commsToDelete.Where(c => c.PostId == PostId));
            return commsToDelete;
        }

    }
}

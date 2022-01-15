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
    
    }
}

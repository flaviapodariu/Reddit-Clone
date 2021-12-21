using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

namespace Wreddit.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(WredditContext context) : base(context) { }

        public async Task<List<PostDTO>> GetAllPostsWithAuthors()
        {
       
            return await _context.Posts.Select(p => new PostDTO(p, new string(p.User.UserName))).ToListAsync();
        }
    }
}
     
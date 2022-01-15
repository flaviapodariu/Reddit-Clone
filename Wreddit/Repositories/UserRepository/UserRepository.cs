using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WredditContext context) : base(context) { }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetByIdWithRoles(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<List<PostVotes>> GetUsersPostVotes(int id) // returns the posts the user voted on 
        {
            return await _context.PostVotes.Where(vote => vote.UserId == id).ToListAsync();
        }

        public async Task<List<CommentVotes>> GetUsersCommentVotes(int id) // returns the comments the user voted on 
        {
            return await _context.CommentVotes.Where(vote => vote.UserId == id).ToListAsync();

        }
    }
}


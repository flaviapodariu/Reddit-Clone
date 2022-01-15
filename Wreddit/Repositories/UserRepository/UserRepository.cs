using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WredditContext context) : base(context) { }
        public async Task<List<UsersInfoDTO>> GetAllUsers()
        {
            var userRoles = _context.UserRoles.Join(_context.Users,
                                                           ur => ur.UserId,
                                                           u => u.Id,
                                                           (ur, u) => new
                                                           {
                                                               UserName = u.UserName,
                                                               Email = u.Email,
                                                               Role = ur.RoleId
                                                           });

            var admins = userRoles.Where(a => a.Role.Equals(1)).Select(a => new { a.UserName, a.Email })
                                  .AsEnumerable().Select(a => new Tuple<string, string>(a.UserName, a.Email)).ToList();

            var users = userRoles.Where(u => u.Role.Equals(2)).Select(u => new {u.UserName, u.Email})
                                 .AsEnumerable().Select(u => new Tuple<string, string>(u.UserName, u.Email)).ToList();

            var groups = await userRoles.GroupBy(role => role.Role).Select(all => new UsersInfoDTO(all.Key, all.Count())).ToListAsync();
            foreach(var group in groups)
            {
                if (group.Role == 1)                

                  group.UserData = admins;               
                else
                  group.UserData = users;
                
            }

            return groups;
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

    
    }
}


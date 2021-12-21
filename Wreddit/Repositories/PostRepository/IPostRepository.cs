using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
   public interface IPostRepository: IGenericRepository<Post>
    {
        Task<List<Post>> GetAllPostsWithUsers();
        Task<List<Post>> GetAllPostsWithComments();
        Task<Post> GetPostWithComments(int id);
        Task<Post> GetPostWithUser(int id);

    }
}

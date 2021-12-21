using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
   public interface IPostRepository: IGenericRepository<Post>
    {
        public Task<List<PostDTO>> GetAllPostsWithAuthors();
    }
}

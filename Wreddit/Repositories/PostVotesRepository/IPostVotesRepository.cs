using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

namespace Wreddit.Repositories
{
    public interface IPostVotesRepository: IGenericRepository<PostVotes>
    {
        void DeleteById(int PostId);
        Task<List<PostVotes>> GetUsersPostVotes(int id);
        Task<Post> UpdatePostVotes(PostVoteDTO dto);
    }
}

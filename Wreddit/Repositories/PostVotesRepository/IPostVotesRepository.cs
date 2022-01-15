using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;

namespace Wreddit.Repositories
{
    public interface IPostVotesRepository: IGenericRepository<PostVotes>
    {
        void DeleteById(int PostId);
    }
}

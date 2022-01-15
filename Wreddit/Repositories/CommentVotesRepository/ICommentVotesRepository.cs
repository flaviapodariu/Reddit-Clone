using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;

namespace Wreddit.Repositories
{
    public interface ICommentVotesRepository : IGenericRepository<CommentVotes>
    {
        void DeleteById(int PostId);
    }
}

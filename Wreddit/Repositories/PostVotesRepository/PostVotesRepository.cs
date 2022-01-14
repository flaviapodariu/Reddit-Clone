using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;

namespace Wreddit.Repositories
{
    public class PostVotesRepository: GenericRepository<PostVotes>, IPostVotesRepository
    {
        public PostVotesRepository(WredditContext context) : base(context) { }

        public void DeleteById(int postId)
        {
            var votesToDelete =  _context.PostVotes.ToList();  //nu ma lasa cu async
            votesToDelete.RemoveAll(p => p.PostId.Equals(postId));
        }

    }
}

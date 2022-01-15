using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Models.Entities;

namespace Wreddit.Repositories
{
    public class CommentVotesRepository: GenericRepository<CommentVotes>, ICommentVotesRepository
    {
        public CommentVotesRepository(WredditContext context) : base(context) { }

        public void DeleteById(int commentId)
        {
            var votesToDelete = _context.CommentVotes.ToList();  //nu ma lasa cu async
            votesToDelete.RemoveAll(c => c.CommentId.Equals(commentId));
        }
    }
}

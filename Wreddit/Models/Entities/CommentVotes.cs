using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Wreddit.Models.Entities
{
    public class CommentVotes
    {
        public virtual int? CommentId { get; set; }
        public virtual int? UserId { get; set; }
        public int? VoteType { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }

    }
}

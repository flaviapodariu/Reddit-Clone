using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Wreddit.Models.Entities
{
    public class CommentVotes
    {
        public CommentVotes() : base() { }
        public CommentVotes(int _CommentId, int _UserId, int _VoteType)
        {
            this.CommentId = _CommentId;
            this.UserId = _UserId;
            this.VoteType = _VoteType;
        }
        public virtual int? CommentId { get; set; }
        public virtual int? UserId { get; set; }
        public int? VoteType { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }

    }
}

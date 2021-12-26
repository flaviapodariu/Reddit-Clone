using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Wreddit.Models.Entities
{
    public class PostVotes
    {
        public PostVotes() : base() { }
        public PostVotes(int _PostId, int _UserId, int _VoteType)
        {
            this.PostId = _PostId;
            this.UserId = _UserId;
            this.VoteType = _VoteType;
        }
        public virtual int? PostId { get; set; }
        public virtual int? UserId { get; set; }
        public int? VoteType { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}

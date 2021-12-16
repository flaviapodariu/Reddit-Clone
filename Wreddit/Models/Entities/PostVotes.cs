using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Wreddit.Models.Entities
{
    public class PostVotes
    {
        public virtual int? PostId { get; set; }
        public virtual int? UserId { get; set; }
        public int? VoteType { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;

namespace Wreddit.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
#nullable enable
        public virtual ICollection<CommentVotes>? CommentsVotes { get; set; }
#nullable disable
    }
}

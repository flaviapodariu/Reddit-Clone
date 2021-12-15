using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public virtual ICollection<Comments>? Comments { get; set; }
    }
}

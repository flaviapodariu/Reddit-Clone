using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Wreddit.Models.Entities
{
    public class User: IdentityUser<int>
    {
        public User() : base() { }
#nullable enable
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<PostVotes>? PostVotes { get; set; }
        public ICollection<CommentVotes>? CommentsVotes { get; set; }
#nullable disable
        public ICollection<UserRole> UserRoles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
#nullable enable
        public List <Comment>? Comments { get; set; }

        public List<PostVotes>? PostVotes { get; set; }
        public PostDTO(Post post, string u)
        {
            this.Id = post.Id;
            this.UserId = post.UserId;
            this.Upvotes = post.Upvotes;
            this.Downvotes = post.Downvotes;
            this.Title = post.Title;
            this.Text = post.Text;
            this.UserName = u;
            this.Comments = new List<Comment>();
            this.PostVotes = new List<PostVotes>();
        }
        public PostDTO() { }
    }

    
}

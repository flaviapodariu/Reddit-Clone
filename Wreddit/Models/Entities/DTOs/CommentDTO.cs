using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public UserDTO User { get; set; }

        public CommentDTO(Comment comment)
        {
            this.Id = comment.Id;
            this.ParentId = comment.ParentId;
            this.UserId = comment.UserId;
            this.Content = comment.Content;
            this.PostId = comment.PostId;
            this.Upvotes = comment.Upvotes;
            this.Downvotes = comment.Downvotes;
        }
        public CommentDTO() { }
    }
}

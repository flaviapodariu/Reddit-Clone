﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class CommentVoteDTO
    {
        public CommentVoteDTO() { }


        public CommentVoteDTO(CommentVotes vote)
        {
            CommentId = vote.CommentId;
            UserId = vote.UserId;
            VoteType = vote.VoteType;
        }

        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int VoteType { get; set; }

        
    }
}

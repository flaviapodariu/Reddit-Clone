﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class VoteDTO
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string VoteType { get; set; }
    }

}
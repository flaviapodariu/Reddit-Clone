﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public UserDTO(User user)
        {
            this.Id = user.Id;
            this.UserName = user.UserName;
            this.Email = user.Email;
        }
    }
}

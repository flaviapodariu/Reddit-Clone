using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Entities
{
    public class User: IdentityUser<int>
    {
        public User() : base() { }
        public int SettingsId { get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}

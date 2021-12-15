using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Entities
{
    public class Role: IdentityRole<int>
    {
        public Role(): base() { }
        public ICollection<UserRole> UserRoles{ get; set; }
    }
}

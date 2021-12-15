using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Entities
{
    public class Settings
    {
        public int Id { get; set; }
        public bool Notifications { get; set; }
        public User User { get; set; }
    }
}


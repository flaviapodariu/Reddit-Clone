using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class UsersInfoDTO
    {
        public List<Tuple<int, string, string>> UserData { get; set; }  //id + username + email
        public int Role { get; set; }  // 1 - Admin,  2 - User

        public int Count { get; set; }

        public UsersInfoDTO(): base() { }
        public UsersInfoDTO(int role, int count)
        {
            this.UserData = new List<Tuple<int, string, string>>();
            this.Role = role;
            this.Count = count;
        }
    }
}
 
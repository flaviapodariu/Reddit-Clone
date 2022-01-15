using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Models.Entities.DTOs
{
    public class DeleteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Models.Entities;
using Wreddit.Models.Entities.DTOs;

namespace Wreddit.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetCommentsFromPost(int PostId);
        Task<List<Comment>> GetCommentsByUser(int UserId);
        Task<List<Comment>> DeleteByUser(int UserId);
        Task<List<Comment>> DeleteByPost(int PostId);
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Repositories;
using Wreddit.Services.UserServices;

namespace Wreddit.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        ISessionTokenRepository SessionToken { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
    }
}

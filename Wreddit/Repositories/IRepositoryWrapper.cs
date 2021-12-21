﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wreddit.Repositories
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
    }
}

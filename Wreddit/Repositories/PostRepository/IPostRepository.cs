﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Entities;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
   public interface IPostRepository: IGenericRepository<Post>
    {

    }
}
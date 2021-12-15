using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;
using Wreddit.Repositories;

namespace Wreddit.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly WredditContext _context;
        private IUserRepository _user;
        private IPostRepository _post;
     
        public RepositoryWrapper(WredditContext context)
        {
            _context = context;
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                    _user = new UserRepository(_context);
                return _user;
            }
        }
        public IPostRepository Post
        {
            get
            {
                if (_post == null)
                    _post = new PostRepository(_context);
                return _post;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

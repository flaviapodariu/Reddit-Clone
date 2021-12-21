using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wreddit.Data;

namespace Wreddit.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly WredditContext _context;
        private IUserRepository _user;
        private IPostRepository _post;
        private ISessionTokenRepository _sessionToken;
        private ICommentRepository _comment;
     
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

        public ISessionTokenRepository SessionToken
        {
            get
            {
                if (_sessionToken == null)
                    _sessionToken = new SessionTokenRepository(_context);
                return _sessionToken;
            }
        }
        public ICommentRepository Comment
        {
            get
            {
                if (_comment == null)
                    _comment = new CommentRepository(_context);
                return _comment;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

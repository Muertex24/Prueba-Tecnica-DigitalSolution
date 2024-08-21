using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.domain.Entities;
using core.domain.Interfaces.Repositories;
using infrastructure.data.Contexts;

namespace infrastructure.data.Repositories
{
    public class PostRepository : InterfacePostRepository<Post, string>
    {
        private readonly SocialNetworkContext _context;

        public PostRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public Post post(Post entity)
        {
            _context.Posts.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<Post> PostAsync(Post entity)
        {
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Post GetById(int id) 
        {
            return _context.Posts.Find(id);
        }

        public async Task<Post> GetByIdAsync(int id) 
        {
            return await _context.Posts.FindAsync(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public async Task<IEnumerable<Post>> GetAllAsync() 
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorsAsync(IEnumerable<string> authorUsernames)
        {
            return await _context.Posts
                                 .Where(p => authorUsernames.Contains(p.AuthorUsername))
                                 .ToListAsync();
        }

    }
}

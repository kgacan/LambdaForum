using LambdaForum.Data;
using LambdaForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambadForum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(x=>x.Posts);
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            return _context.ApplicationUsers;
        }

        public Forum GetById(int id)
        {
            return _context.Forums.Where(x => x.Id == id)
                .Include(y => y.Posts).ThenInclude(z => z.User)
                .Include(a => a.Posts).ThenInclude(b => b.PostReplies).ThenInclude(c => c.User)
                .FirstOrDefault();
        }

        public Task UpdateForumDescription(int id, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int id, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}

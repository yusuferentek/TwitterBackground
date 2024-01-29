using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Database;
using TwitterApi.Infrastructure.Entities.Mentions;
using TwitterApi.Infrastructure.Repositories.Base;

namespace TwitterApi.Infrastructure.Repositories.MentionRepos
{
    public interface IMentionRepository : IRepository<Mention>
    {
        Mention GetById(int id);
        Mention Add(Mention mention);
        void Delete(Mention mention);
        IQueryable<Mention> GetAll();
        IQueryable<Mention> GetMentionsForFollowedUsers(int userId);
    }

    public class MentionRepository : IMentionRepository
    {
        private readonly DBContext _context;
        public MentionRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Mention Add(Mention mention)
        {
            return _context.Mentions.Add(mention).Entity;
        }

        public void Delete(Mention mention)
        {
            _context.Remove(mention);
        }

        public IQueryable<Mention> GetAll()
        {
            return _context.Mentions;
        }

        public Mention GetById(int id)
        {
            return _context.Mentions.Where(x => x.Id == id).FirstOrDefault() ?? new Mention { };
        }

        public IQueryable<Mention> GetMentionsForFollowedUsers(int userId)
        {
            var user = _context.Users
                .Include(u => u.Following)
                .ThenInclude(f => f.Mentions)
                .FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                var mentions = user.Following
                    .SelectMany(f => f.Mentions)
                    .OrderByDescending(m => m.CDate);

                return mentions.AsQueryable();
            }

            return Enumerable.Empty<Mention>().AsQueryable();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApi.Infrastructure.Database;
using TwitterApi.Infrastructure.Entities.Mentions;
using TwitterApi.Infrastructure.Entities.Users;
using TwitterApi.Infrastructure.Repositories.Base;
using Utils.Extensions;

namespace TwitterApi.Infrastructure.Repositories.UserRepos
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsernameAndPass(string email, string password);
        User GetUserById(int UserId);
        User Add(User user);
        void Delete(User user);
        IQueryable<User> GetAll();
        Task FollowUser(int followerId, int followingId);
        Task UnfollowUser(int followerId, int followingId);

    }

    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;

        public UserRepository(DBContext context)
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

        public User Add(User user)
        {
            return _context.Users.Add(user).Entity;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetUserByUsernameAndPass(string username, string password)
        {
            var pass = (username + password).ToPass();
            return _context.Users.FirstOrDefault(x => x.Username == username && x.Password == pass);
        }

        public User GetUserById(int UserId)
        {
            return _context.Users.Where(x => x.Id == UserId).FirstOrDefault();
        }
        public async Task FollowUser(int followerId, int followingId)
        {
            var follower = await _context.Users.FindAsync(followerId);
            var following = await _context.Users.FindAsync(followingId);

            if (follower != null && following != null)
            {
                follower.Following.Add(following);
                following.Followers.Add(follower);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UnfollowUser(int followerId, int followingId)
        {
            var follower = await _context.Users.FindAsync(followerId);
            var following = await _context.Users.FindAsync(followingId);

            if (follower != null && following != null)
            {
                follower.Following.Remove(following);
                following.Followers.Remove(follower);
                await _context.SaveChangesAsync();
            }

        }


    }
}

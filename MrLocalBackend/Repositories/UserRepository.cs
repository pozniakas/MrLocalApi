using Microsoft.EntityFrameworkCore;
using MrLocalBackend.Repositories.Interfaces;
using MrLocalDb;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MrLocalDbContext _context;
        public UserRepository(MrLocalDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(string username, string password)
        {
            var updatedAt = DateTime.UtcNow;
            var createdAt = DateTime.UtcNow;
            var id = Guid.NewGuid().ToString();
            var user = new User(id, username, password, createdAt, updatedAt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> FindOne(string username)
        {
            var result = await _context.Users.SingleOrDefaultAsync(b => b.Username == username);

            return result;
        }
    }
}

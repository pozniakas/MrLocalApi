using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByUsername(string username);
        public Task<User> GetUserById(string id);
        public Task<User> CreateUser(string username, string password);
    }
}

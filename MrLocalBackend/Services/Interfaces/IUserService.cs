using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUser(string username);
        public Task<User> CreateUser(string username, string password);
    }
}

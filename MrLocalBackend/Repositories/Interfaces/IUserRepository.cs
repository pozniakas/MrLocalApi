using MrLocalDb.Entities;
using System.Threading.Tasks;

namespace MrLocalBackend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> Create(string username, string password);
        public Task<User> FindOne(string username, string password);
    }
}

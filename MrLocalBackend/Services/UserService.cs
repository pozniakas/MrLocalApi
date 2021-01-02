using MrLocalBackend.Repositories.Interfaces;
using MrLocalBackend.Services.Interfaces;
using MrLocalDb.Entities;
using System;
using System.Threading.Tasks;

namespace MrLocalBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly Lazy<IValidateData> _validateData = null;

        public UserService(IUserRepository userRepository, Lazy<IValidateData> validateData)
        {
            _validateData = validateData;
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(string username, string password)
        {
            await _validateData.Value.ValidateUsername(username);
            var createdUser = await _userRepository.Create(username, password);

            return createdUser;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _userRepository.FindOne(username);

            return user;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userRepository.FindOne(id);

            return user;
        }
    }
}

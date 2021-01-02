using System.Threading.Tasks;

namespace MrLocalBackend.Authentication.Interfaces
{
    public interface IJwdAuthenticationManager
    {
        public Task<string> AuthenticateAsync(string username, string password);
    }
}

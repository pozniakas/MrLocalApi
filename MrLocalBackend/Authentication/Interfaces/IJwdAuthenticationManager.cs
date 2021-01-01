namespace MrLocalBackend.Authentication.Interfaces
{
    public interface IJwdAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}

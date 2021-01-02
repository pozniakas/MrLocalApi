using Microsoft.IdentityModel.Tokens;
using MrLocalBackend.Authentication.Interfaces;
using MrLocalBackend.Services.Interfaces;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MrLocalBackend.Authentication
{
    public class JwdAuthenticationManager : IJwdAuthenticationManager
    {
        private readonly string _Key;
        private readonly IUserService _userService;


        public JwdAuthenticationManager(IUserService userService)
        {
            _Key = ConfigurationManager.AppSettings.Get("SECRET_KEY");
            _userService = userService;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userService.GetUserByUsername(username);
            if (user == null || user.Password != password)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

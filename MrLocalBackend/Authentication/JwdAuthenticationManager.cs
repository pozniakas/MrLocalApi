﻿using Microsoft.IdentityModel.Tokens;
using MrLocalBackend.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MrLocalBackend.Authentication
{
    public class JwdAuthenticationManager : IJwdAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { { "test1", "password1"},{ "test2", "password2"} };
        private readonly string _Key;
        public JwdAuthenticationManager(string key)
        {
            _Key = key;
        }

        public string Authenticate(string username, string password)
        {
            if (false)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username) //i tokena dedam userid ne username
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

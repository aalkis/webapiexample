﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi
{
    public class TokenGenerator
    {
        public string GenerateToken()
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Yavuzyavuzyavuz1."));
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Member"));
            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", audience: "http://localhost",
               claims: null, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}

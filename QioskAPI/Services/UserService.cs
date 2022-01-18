using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using QioskAPI.Interfaces;
using QioskAPI.Data;
using QioskAPI.Helpers;
using QioskAPI.Models;

namespace QioskAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly QioskContext _quioskContext;
        public UserService(IOptions<AppSettings> appSettings, QioskContext qioskContext)
        {
            _appSettings = appSettings.Value;
            _quioskContext = qioskContext;
        }
        public User Authenticate(string email, string password)
        {
            var user = _quioskContext.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserID.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("isActive", user.IsActive.ToString()),
                    new Claim("isAdmin", user.IsAdmin.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Password = null;
            return user;
        }
    }
}

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
        private readonly QioskContext _context;
        public UserService(IOptions<AppSettings> appSettings, QioskContext qioskContext)
        {
            _appSettings = appSettings.Value;
            _context = qioskContext;
        }
        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
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
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(u=>u.Company).ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(u=>u.Company).FirstOrDefaultAsync(u=>u.UserID == id);

         
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u=>u.Company).FirstOrDefaultAsync(u=>u.Email.ToLower() == email);

         
        }
        public async Task PutUser(int id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task PostUser(User user)
        {
            
            var UE =await UserExistsByMailEnUpdateAsync(user);
            if (!UE)
            {
                user.CompanyID = user.CompanyID == 0 ? user.Company.CompanyID : user.CompanyID;
                var c = await _context.Companies.FindAsync(user.CompanyID);
                if (c != null)
                { user.Company = c; }
                if (user.CompanyID == 0)
                {
                    var company = await _context.Companies.FirstOrDefaultAsync(u => user.Company.Name == u.Name);
                    if (company != null)
                    {
                        user.CompanyID = company.CompanyID;
                        user.Company = company;
                    }
                }
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteUser(int id)
        {
            var user =await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public bool UserExistsAsync(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
        public async Task<bool> UserExistsByMailEnUpdateAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower());
            if (existingUser != null)
            {
                existingUser.CompanyID = user.CompanyID;
                existingUser.Company = user.Company;
                existingUser.Password = user.Password;
                existingUser.IsActive = true;
                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        } 
        public async Task<bool> UserExistsByMailAsync(string email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (existingUser != null)
            {
                return true;
            }
            return false;
        }

    }
}

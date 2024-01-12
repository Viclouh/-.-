using API.Models;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Security.Cryptography;


namespace API.Services
{
    public class AuthService
    {
        private Database.Context _dbContext;
        private PasswordHasher<UserAuthData> _passwordHasher = new PasswordHasher<UserAuthData>();

        public AuthService(Database.Context dbContext)
        {
            _dbContext = dbContext;
        }

        public User? checkPassword(string username, string password)
        {
            UserAuthData? user = _dbContext.UserAuthData
                .Include(p => p.User).OrderBy(p => p.Id).LastOrDefault(p => p.UserName == username);
            if (user == null)
            {
                return null;
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
            {
                return user.User;
            }
            
            return null;
        }

        public User? CreateUser(UserAuthData user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _dbContext.UserAuthData.Add(user);
            _dbContext.SaveChanges();
            return user.User;
        }
    }
}

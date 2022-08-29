using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Services;

namespace WebAPI.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IAuthenticationService _service;
        public UserRepository(DataContext context, AuthenticationService service) : base(context)
        {
            _service = service;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
             return await _context.Users.Where(a => a.EmailAddress == email).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserByEmailAndHashedPassword(string email, string hash)
        {
            return await _context.Users.Where(a => a.EmailAddress == email &&
            a.HashedPassword == hash).FirstOrDefaultAsync();
        }
        public async Task<Token?> GetTokenForUser(UserLoginDTO user)
        {
            return await _service.Authenticate(user);
        }

        public async Task<User> GetRegisteredUser(UserRegisterDTO user)
        {
            return await _service.Register(user);
        }
    }
}

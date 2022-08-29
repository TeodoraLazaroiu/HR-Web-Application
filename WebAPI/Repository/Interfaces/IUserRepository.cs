using WebAPI.Models;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByEmailAndHashedPassword(string email, string hash);
        Task<Token?> GetTokenForUser(UserLoginDTO user);
        Task<User> GetRegisteredUser(UserRegisterDTO user);
    }
}